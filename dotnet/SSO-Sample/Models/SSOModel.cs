namespace SSO_Sample.Models
{
    public class SSOModel
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CorpCode { get; set; }

        /// <summary>
        /// UOF X 設定的 SSO 名稱
        /// </summary>
        public string SSOName { get; set;}

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
