namespace Ede.Uofx.ThirdPartyAd.Sample.Models
{
    /// <summary>
    /// Google驗證資訊
    /// </summary>
    public class GoogleAuthInfo
    {
        /// <summary>
        /// Google OAuth 應用程式的 Client ID，通常在 Google Cloud Console 建立 OAuth 憑證時取得
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// 與 Client ID 搭配使用的密鑰，用於交換 access token 時驗證身份，請勿在前端或公開位置暴露
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// 使用者完成授權後，Google 將會導向至的 URL，需與 Google Cloud Console 中設定的 redirect URI 完全一致
        /// </summary>
        public string RedirectUri { get; set; } = string.Empty;

        /// <summary>
        /// 要請求使用者授權的權限範圍（scope），例如 email、profile 等，可用空格分隔多個 scope
        /// </summary>
        public string Scope { get; set; } = string.Empty;

        /// <summary>
        /// 自訂的狀態碼，可用於在授權流程中傳遞自定資訊，例如防止 CSRF 或附帶參數，通常會加密 infoModel
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// 控制 Google 授權頁的顯示，例如 none、consent、select_account、login 等
        /// </summary>
        public string Prompt { get; set; } = string.Empty;
    }
}