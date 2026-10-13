using Ede.Uofx.ThirdPartyAd.Sample.Helper;
using Ede.Uofx.ThirdPartyAd.Sample.JsonPolicy;
using Ede.Uofx.ThirdPartyAd.Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;

namespace Ede.Uofx.ThirdPartyAd.Sample.Controllers
{
    public class GoogleController : Controller
    {
        /// <summary>
        /// Hash Key (UOF X 中設定的也要一樣)
        /// </summary>
        private readonly string _HashKey = "201N0tz3ArwcRohRoKxm8TMBdH6gT5SHJQGFWDgczRM=";

        private static readonly Dictionary<string, string> _SidMap = [];

        private readonly ILogger<GoogleController> _logger;
        private readonly GoogleAuthInfo _googleAuthInfo;
        private readonly HttpClient _http;

        public GoogleController(ILogger<GoogleController> logger, IOptionsSnapshot<GoogleAuthInfo> googleAuthOptions, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _googleAuthInfo = googleAuthOptions.Value;
            _http = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return GoogleLogin();
        }

        /// <summary>
        /// 導向 Google 授權頁面
        /// </summary>
        [HttpGet("uofx/google-login")]
        public IActionResult GoogleLogin()
        {
            // 從網址中接收 info 參數
            if (!Request.Query.TryGetValue("info", out var info))
            {
                // 如果沒有 info 參數，返回錯誤
                ViewBag.ErrorMessage = "info is required";
            }

            // 保存 info model 到 TempData，後續登入成功後須回傳
            TempData["InfoModel"] = info.ToString();

            // 產生 State 參數的 Sid，並產生短時效 Token
            var stateSid = GetSid("state");
            var stateToken = TokenHelper.GenToken(stateSid, DateTimeOffset.Now.AddMinutes(5));

            // 取得 Google 授權頁面 URL
            var authUrl = GetGoogleAuthUrl(stateToken);
            // 導向 Google 授權頁面
            return Redirect(authUrl);
        }

        /// <summary>
        /// 授權成功後的回傳處理
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet("uofx/oauth-callback")]
        public async Task<IActionResult> OAuthCallback(string code, string state)
        {
            // 從網址中接收 code 參數 (Google 授權成功後會帶回這個 code)
            if (string.IsNullOrEmpty(code))
                throw new Exception("Code not found");

            // 從網址中接收 state 參數 (Google 授權成功後會帶回這個 state)
            if (string.IsNullOrEmpty(state))
                throw new Exception("State not found");

            // 驗證 state 參數，確保是從 Google 授權頁面回來的
            if (!TokenHelper.VirtyfyAndGetData(state, out var stateSid))
                throw new Exception("Invalid Token");

            // 設定要傳遞給 Google 的資料，包含 code、client_id、client_secret、redirect_uri 和 grant_type
            var postData = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _googleAuthInfo.ClientId },
                { "client_secret", _googleAuthInfo.ClientSecret },
                { "redirect_uri", _googleAuthInfo.RedirectUri },
                { "grant_type", "authorization_code" }
            };

            // 將 postData 轉換為 FormUrlEncodedContent(HTTP POST 請求的格式)
            var formContent = new FormUrlEncodedContent(postData);

            // 向 Google 取得 access_token，並將回應的 JSON 字串反序列化後，取得 access_token 欄位值
            var tokenResponse = await _http.PostAsync("https://oauth2.googleapis.com/token", formContent);
            var tokenJson = await tokenResponse.Content.ReadAsStringAsync();
            var tokenData = JsonSerializer.Deserialize<JsonElement>(tokenJson);
            var googleAccessToken = tokenData.GetProperty("access_token").GetString();

            // 使用 access_token 向 Google 取得使用者資訊，並取得 email 欄位值
            var userInfoResponse = await _http.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo?access_token=" + googleAccessToken);
            var userJson = await userInfoResponse.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<JsonElement>(userJson);
            var email = user.GetProperty("email").GetString();

            // 產生 Google 帳號的 Sid，並產生短時效 Token
            var sid = GetSid(email);
            var uofxAccessToken = TokenHelper.GenToken(sid, DateTimeOffset.Now.AddMinutes(5));

            // 從 TempData 取得 info model，並將其轉換為 InfoModel 物件
            var modelJson = TempData["InfoModel"] as string;
            byte[] bytes = Convert.FromBase64String(modelJson);
            var infoString = Encoding.UTF8.GetString(bytes);
            var infoModel = JsonSerializer.Deserialize<InfoModel>(infoString, new JsonSerializerOptions
            {
                // 預期 JSON 屬性是小寫開頭的駝峰式命名，以及不區分大小寫，對 JSON 屬性名稱容錯處理比較寬鬆
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });

            // 製作 PostMessage Model，包含 info、token、message 和狀態碼
            var postMessage = new PostMessageModel
            {
                Info = infoModel,
                Token = uofxAccessToken,
                Message = "TPAD login success via Google.",
                StatusCode = 200
            };

            // 將 postMessage 序列化成 JSON 字串
            var result = JsonSerializer.Serialize(postMessage, new JsonSerializerOptions
            {
                // 將屬性名稱第一個字母轉為小寫
                PropertyNamingPolicy = new LowercaseFirstLetterPolicy(),
                WriteIndented = true
            });

            // 將結果傳回給 UOF X 的前端頁面
            return View("LoginSuccess", result);
        }

        /// <summary>
        /// 驗證 token 並回傳 account key
        /// </summary>
        [HttpGet("uofx/google-callback")]
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
            // 將加密後的結果傳回
            return Ok(encodeResult);
        }

        /// <summary>
        /// 取得 Google 授權頁面 URL
        /// </summary>
        /// <returns></returns>
        private string GetGoogleAuthUrl(string stateToken)
        {
            var clientId = _googleAuthInfo.ClientId;
            var redirectUri = _googleAuthInfo.RedirectUri;
            var scope = _googleAuthInfo.Scope;
            var state = stateToken;
            var prompt = _googleAuthInfo.Prompt;

            var authUrl = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={clientId}&redirect_uri={redirectUri}&response_type=code&scope={scope}&state={state}&prompt={prompt}";

            return authUrl;
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
