using Ede.Uofx.ThirdPartyAd.Sample.Helper;
using Ede.Uofx.ThirdPartyAd.Sample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using Ede.Uofx.ThirdPartyAd.Sample.JsonPolicy;
using System.Reflection;

namespace Ede.Uofx.ThirdPartyAd.Sample.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Hash Key (UOF X 中設定的也要一樣)
        /// </summary>
        private readonly string _HashKey = "201N0tz3ArwcRohRoKxm8TMBdH6gT5SHJQGFWDgczRM=";

        private static readonly Dictionary<string, string> _SidMap = [];

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Login();
        }

        /// <summary>
        /// 登入頁
        /// </summary>
        [HttpGet("uofx/login")]
        public IActionResult Login()
        {
            // 從網址中接收 info 參數
            if (!Request.Query.TryGetValue("info", out var info))
            {
                // 如果沒有 info 參數，返回錯誤
                ViewBag.ErrorMessage = "info is required";
            }

            // 保存 info model 到 TempData，後續登入成功後須回傳
            TempData["InfoModel"] = info.ToString();

            return View("index");
        }
      
        /// <summary>
        /// 按下登入按鈕後的處理
        /// </summary>
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // 在這裡處理登入邏輯，例如驗證帳號和密碼
            if (!string.IsNullOrWhiteSpace(username)) 
            {
                //------------------------------------------------------
                //---            在這實作帳號密碼驗證的邏輯              ---
                //------------------------------------------------------
                //if (username != "testaccount")
                //{
                //    ViewBag.ErrorMessage = "帳號或密碼錯誤";
                //    return View("index");
                //}
                //------------------------------------------------------

                //------------------------------------------------------
                //---             這裡應隨實際需求動態調整              ---
                //------------------------------------------------------

                // 產生要登入的一次性帳號識別碼並放入 Token 中， callback 時用來識別身分 
                // (請依自己需求調整，但不建議把真實帳號放入 Token 中，因為 token 內容是公開的)
                var sid = GetSid(username);

                // 產生短時效 Token (可改成使用自己製作的 token)
                var accessToken = TokenHelper.GenToken(sid, DateTimeOffset.Now.AddMinutes(5));

                //------------------------------------------------------

                // 從 TempData 取得 info model
                var modelJson = TempData["InfoModel"] as string;
                byte[] bytes = Convert.FromBase64String(modelJson);
                var infoString = Encoding.UTF8.GetString(bytes);
                var infoModel = JsonSerializer.Deserialize<InfoModel>(infoString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                // 製作 PostMessage Model
                var model = new PostMessageModel
                {
                    Info = infoModel,
                    Token = accessToken,
                    Message = "TPAD login success.",
                    StatusCode = 200
                };

                // 將 model 序列化成 JSON 字串
                var result = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = new LowercaseFirstLetterPolicy(), // 將屬性名稱第一個字母轉為小寫
                    WriteIndented = true
                });
                return View("LoginSuccess", result);
            }
            else
            {
                // 驗證失敗，返回登入頁面並顯示錯誤訊息
                ViewBag.ErrorMessage = "帳號或密碼錯誤";
                return View("index");
            }
        }

        /// <summary>
        /// 檢驗 token 並回傳 account key
        /// </summary>
        [HttpGet("uofx/accountkey")]
        public IActionResult Accountkey()
        {
            // 從網址中接收 token 參數 (UOFX 會以 query-string 't' 傳遞過來)
            if (!Request.Query.TryGetValue("t", out var accessToken))
                throw new Exception("Token not found");

            // 驗證 Token，並從 Token 中取得 sid
            if (!TokenHelper.VirtyfyAndGetData(accessToken, out var sid))
                throw new Exception("Invalid Token");

            // 根據 sid 取得帳號
            var accountKey = GetAccountBySid(sid);

            // 產生要回傳的 model
            var result = new CallbackResponseModel()
            {
                AccountKey = accountKey,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() //務必放當下時間，會依此時間來判斷是否過期
            };

            // 使用 AES 加密 model
            /*
                加密演算法：AES（Advanced Encryption Standard）
                填充模式：PKCS7
                加密模式：CBC（Cipher Block Chaining）
                反饋大小：128 位元
             */
            byte[] aeskeyBytes = HashHelper.SHA256ToBytes(_HashKey);
            var aesKey = aeskeyBytes;   // SecretKey 的 SHA256 Hash
            var aesIv = aeskeyBytes.Skip(16).ToArray(); // SecretKey 的 SHA256 Hash 後 16 bytes
            var encodeResult = AesHelper.EncodeData(JsonSerializer.Serialize(result), aesKey, aesIv);

            return Ok(encodeResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 產生一次性帳號識別碼
        /// </summary>
        private string GetSid(string account)
        {
            //這只是 sample code，實務上應該更嚴謹的規畫
            var id = Guid.NewGuid().ToString();
            _SidMap.Add(id, account);
            return id;
        }

        /// <summary>
        /// 根據一次性帳號識別碼取得帳號
        /// </summary>
        private string GetAccountBySid(string sid)
        {
            //這只是 sample code，實務上應該更嚴謹的規畫
            if (_SidMap.TryGetValue(sid, out string account))
            {
                return account;
            }

            throw new Exception("Invalid Sid");
        }
    }
}
