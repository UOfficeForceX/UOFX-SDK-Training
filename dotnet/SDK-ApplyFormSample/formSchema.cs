//----------------------
// <auto-generated>
//    請勿修改此檔案內容，以免產生不可預期的錯誤。
//    產生日期:2024-10-14 05:59:16。
//    類別:SDK外部起單, 表單:採購單-25R1, 版本:1
//     Generated using the NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------


namespace Ede.Uofx.FormSchema.UofxFormSchema
{
    #pragma warning disable // Disable all warnings

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.2.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class FileItem
    {
        /// <summary>
        /// Id
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("FileName", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string FileName { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.2.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class UofxFormSchemaFields
    {
        /// <summary>
        /// 採購單號
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C002", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(255, MinimumLength = 1)]
        public string C002 { get; set; }

        /// <summary>
        /// 申請人
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C003", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Collections.Generic.ICollection<AccountItem> C003 { get; set; } = new System.Collections.ObjectModel.Collection<AccountItem>();

        /// <summary>
        /// 供應商名稱
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C023", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(255, MinimumLength = 1)]
        public string C023 { get; set; }

        /// <summary>
        /// 採購日期
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C006", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTimeOffset C006 { get; set; }

        /// <summary>
        /// 採購類型，單選項目: 一般採購、緊急採購
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C008", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string C008 { get; set; }

        /// <summary>
        /// 交貨地點
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C007", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string C007 { get; set; }

        /// <summary>
        /// 請購明細
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C012", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<C012Row> C012 { get; set; }

        /// <summary>
        /// 請購單位需求說明
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C013", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string C013 { get; set; }

        /// <summary>
        /// 採購單位說明
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C015", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string C015 { get; set; }

        /// <summary>
        /// 附件上傳
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C022", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<FileItem> C022 { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.2.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class AccountItem
    {
        /// <summary>
        /// 申請者原公司代碼(如為其他公司兼職才需要填)
        /// </summary>
        [Newtonsoft.Json.JsonProperty("CorpCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CorpCode { get; set; }

        /// <summary>
        /// 部門代碼，此人員所屬的部門
        /// </summary>
        [Newtonsoft.Json.JsonProperty("DeptCode", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 人員帳號
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Account", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Account { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.2.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class C012Row
    {
        /// <summary>
        /// 料號
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C016", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string C016 { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C017", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string C017 { get; set; }

        /// <summary>
        /// 規格
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C018", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string C018 { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C019", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string C019 { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C020", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.Range(1, 99999999)]
        public int? C020 { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C021", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string C021 { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.2.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class UofxFormSchema
    {
        /// <summary>
        /// 請勿修改
        /// </summary>
        [Newtonsoft.Json.JsonProperty("FormId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid FormId { get; set; } = new System.Guid("4a00b6b8-b42e-463e-7128-08dcec123d57");

        /// <summary>
        /// 請勿修改
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ScriptId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid ScriptId { get; set; } = new System.Guid("96adc8af-45cb-4148-8742-08dcec123d54");

        /// <summary>
        /// 人員帳號
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Account", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Account { get; set; }

        /// <summary>
        /// 申請者原公司代碼(如為其他公司兼職才需要填)
        /// </summary>
        [Newtonsoft.Json.JsonProperty("CorpCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CorpCode { get; set; }

        /// <summary>
        /// 部門代碼，此人員所屬的部門
        /// </summary>
        [Newtonsoft.Json.JsonProperty("DeptCode", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 起單完成要回覆的 api url
        /// </summary>
        [Newtonsoft.Json.JsonProperty("CallBackUrl", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CallBackUrl { get; set; }

        /// <summary>
        /// 使用者自訂資料，會在 CallBack 時回傳
        /// </summary>
        [Newtonsoft.Json.JsonProperty("CustomData", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CustomData { get; set; }

        /// <summary>
        /// 急件
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Urgent", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Urgent { get; set; }

        /// <summary>
        /// 備註說明
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Opinion", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Opinion { get; set; }

        /// <summary>
        /// 附件。請先呼叫檔案上傳 (UofxService.File.FileUpload) 取得 id 和 name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("AttachFiles", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<FileItem> AttachFiles { get; set; }

        /// <summary>
        /// 表單欄位
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Fields", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public UofxFormSchemaFields Fields { get; set; } = new UofxFormSchemaFields();


    }
}