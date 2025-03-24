﻿using Microsoft.AspNetCore.Mvc;
using SSO_Sample.Helper;
using SSO_Sample.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SSO_Sample.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Hash Key (UOF X 中設定的也要一樣)
        /// </summary>
        private readonly string _HashKey = "201N0tz3ArwcRohRoKxm8TMBdH6gT5SHJQGFWDgczRM=";

        private static readonly Dictionary<string, string> _SidMap = [];

        public HomeController(ILogger<HomeController> logger)
        {
        }

        public IActionResult Index()
        {
            HomeViewModel defaultValue = new HomeViewModel();
            defaultValue.targetUrl = "https://myuofx.com.tw";
            defaultValue.corpCode = "ede";
            defaultValue.ssoName = "Test SSO";
            defaultValue.account = "justin";
            return View(defaultValue);
        }

        /// <summary>
        /// 點擊按鈕後前往 UOF X
        /// </summary>
        [HttpPost]
        public ActionResult ButtonClick(HomeViewModel body)
        {
            //------------------------------------------------------
            //---             裡應隨實際需求動態調整                ---
            //------------------------------------------------------

            var uofxUrl = body.targetUrl ?? "https://myuofx.com.tw";  //<== 要登入的站台網址
            var uofxAccount = body.account ?? "justin";         //<== 要登入的使用者帳號
            var uofxCorpCode = body.corpCode ?? "ede";          //<== 登入的公司代號
            var uofxSSOName = body.ssoName ?? "Test SSO";     //<==  UOF X 設定的 SSO 名稱

            // 產生要登入的一次性帳號識別碼並放入 Token 中， callback 時用來識別身分 
            // (請依自己需求調整，但不建議把真實帳號放入 Token 中，因為 token 內容是公開的)
            var sid = GetSid(uofxAccount);

            // 產生短時效 Token (可改成使用自己製作的 token)
            var accessToken = TokenHelper.GenToken(sid, DateTimeOffset.Now.AddMinutes(5));

            // 進入 UOF X 後要前往的頁面
            var target = "";

            //------------------------------------------------------

            // 產生 SSO Model 並使用 Base64 編碼
            var ssoModel = new SSOModel()
            {
                CorpCode = uofxCorpCode,
                SSOName = uofxSSOName,
                Token = accessToken,
                Target = target,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };

            var ssoJsonString = JsonSerializer.Serialize(ssoModel);
            var ssoBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(ssoJsonString));

            // 產生 Hash
            /*
                哈希演算法：SHA-256
                金鑰哈希演算法：HMAC（Hash-based Message Authentication Code）
                編碼：使用 UTF-8 編碼將輸入字串和金鑰轉換為位元組
                輸出格式：將哈希結果轉換為十六進制字串
             */
            var hashString = HashHelper.HMACSHA256(ssoBase64, _HashKey);

            // 要開啟 UOF X 的 URL
            var fullUrl = new Uri(new Uri(uofxUrl), $"/UniversalLink/sso?p={Uri.EscapeDataString(ssoBase64)}&h={Uri.EscapeDataString(hashString)}").ToString();
            return Redirect(fullUrl);
        }

        /// <summary>
        /// UOF X 登入時的 Callback 驗證 API
        /// </summary>
        [HttpGet("/sso/callback")]
        public ActionResult Callback()
        {
            // 從網址中接收 token 參數 (UOFX 會以 query-string 't' 傳遞過來)
            if (!Request.Query.TryGetValue("t", out var accessToken))
                throw new Exception("Token not found");

            // 驗證 Token，並從 Token 中取得 sid
            if (!TokenHelper.VirtyfyAndGetData(accessToken, out var sid))
                throw new Exception("Invalid Token");

            // 根據 sid 取得帳號
            var account = GetAccountBySid(sid);

            // 產生要回傳的 model
            var result = new CallbackResponseModel()
            {
                AccountKey = account,
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
            var aesKey = aeskeyBytes;   // HashKey 的 SHA256 Hash
            var aesIv = aeskeyBytes.Skip(16).ToArray(); // HashKey 的 SHA256 Hash 後 16 bytes
            var encodeResult = AesHelper.EncodeData(JsonSerializer.Serialize(result), aesKey, aesIv);

            return Ok(encodeResult);
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
