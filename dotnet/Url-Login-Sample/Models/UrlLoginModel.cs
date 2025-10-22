namespace Url_Login_Sample.Models
{
    public class UrlLoginModel
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CorpCode { get; set; }

        /// <summary>
        /// UOF X 設定的URL登入名稱
        /// </summary>
        public string UrlLoginName { get; set;}

        /// <summary>
        /// 用於 Callback 驗證身分的 Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 進入 UOF X 後要前往的頁面
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 時間戳記
        /// </summary>
        public long Timestamp { get; set; }
    }
}
