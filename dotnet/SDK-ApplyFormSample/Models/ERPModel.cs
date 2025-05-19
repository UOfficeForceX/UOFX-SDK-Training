namespace SDK_FirstSample.Models
{
    public class PurchaseModel
    {
        // 採購單號
        public decimal PurchaseID { get; set; }
        // 供應商編號
        public decimal SupplierID { get; set; }
        // 狀態（待審核/已核准/已作廢/已拒絕）
        public string Status { get; set; }
        // 採購日期
        public DateTimeOffset PurchaseDate { get; set; }
        // 採購類型（一般採購/緊急採購）
        public string PurchaseType { get; set; }
        // 採購明細
        public List<PurchaseDetailModel> PurchaseDetails { get; set; }
    }

    public class PurchaseDetailModel
    {
        // 採購明細編號
        public decimal PurchaseDetailID { get; set; }
        // 採購單號
        public decimal PurchaseID { get; set; }
        // 產品ID
        public decimal ProductID { get; set; }
        // 數量
        public decimal Quantity { get; set; }
        // 單價
        public decimal UnitPrice { get; set; }
        // 小計（計算欄位：數量 * 單價）
        public decimal Subtotal { get; set; }
    }

    public class UpdatePurchaseStatusModel
    {
        // 採購單號
        public decimal PurchaseID { get; set; }
        // 狀態（待審核/已核准/已作廢/已拒絕）
        public string Status { get; set; }
    }

    public class UpdateBpmModel
    {
        // 採購單號
        public decimal PurchaseID { get; set; }
        // 簽核單號
        public string BpmID { get; set; }
    }
}
