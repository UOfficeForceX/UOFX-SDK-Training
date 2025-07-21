using Microsoft.AspNetCore.Mvc;

namespace Url_Login_Sample.Models
{
    /// <summary>
    /// 首頁欄位資訊
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// 目標站台的 Host
        /// </summary>
        public string targetUrl { get; set; }
        /// <summary>
        /// 目標公司別
        /// </summary>
        public string corpCode { get; set; }
        /// <summary>
        /// 目標 SSO 名稱
        /// </summary>
        public string urlLoginName { get; set; }
        /// <summary>
        /// 要登入的帳號
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 可以選擇的魔法連結類型
        /// </summary>
        public Dictionary<string, string> magicLinkOptions { get; set; }
        /// <summary>
        /// 已選擇的魔法連結類型
        /// </summary>
        public string selectMagicLinkType { get; set; }
        /// <summary>
        /// 魔法連結所須提供的資訊
        /// </summary>
        public string maginLinkPayload { get; set; }
    }

    /// <summary>
    /// 魔法連結
    /// </summary>
    public class MagicLinkModel
    {
        /// <summary>
        /// 模組
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 額外須提供的資訊
        /// </summary>
        public object Payload { get; set; }
    }

    /// <summary>
    /// 魔法連結 - 模組類型
    /// </summary>
    public class ModuleType
    {
        public static readonly string Bpm = "Bpm";
    }

    /// <summary>
    /// 魔法連結 - BPM 模組可用操作類型
    /// </summary>
    public class BpmActionType
    {
        /// <summary>
        /// 表單申請
        /// </summary>
        public static readonly string Apply = "Apply";
        /// <summary>
        /// 表單簽核
        /// </summary>
        public static readonly string Sign = "Sign";
    }
}
