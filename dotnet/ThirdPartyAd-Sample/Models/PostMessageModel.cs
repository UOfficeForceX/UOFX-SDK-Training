namespace Ede.Uofx.ThirdPartyAd.Sample.Models
{
    public class PostMessageModel
    {
        /// <summary>
        /// 類型，應為 TPAD_Login
        /// </summary>
        public string Type { get; } = "TPAD_Login";

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 登入成功後的 token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 狀態碼: 成功 200，失敗 400
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 登入失敗時的錯誤訊息
        /// </summary>
        public string[] Errors { get; set; }

        /// <summary>
        /// 登入頁帶入的資訊
        /// </summary>
        public InfoModel Info { get; set; }
    }
}
