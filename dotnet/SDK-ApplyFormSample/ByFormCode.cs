using Ede.Uofx.PubApi.Sdk.NetStd.Helpers;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace SDK_FirstSample
{
    public static class ByFormCode
    {
        /// <summary>
        /// 使用表單代碼起單
        /// </summary>
        public static async Task<object> GenFormContentAsync(string formCode)
        {
            // ---------- 要上傳的檔案應該要先上傳，再取得檔案物件 ----------

            //上傳檔案
            //var fileView = await UofxService.File.FileUpload(@"D:\sample\sample.pdf");

            //轉換成檔案物件
            //var fileItem = new FileModel(fileView.Id, fileView.FileName);

            //------------------------------------------------------------


            //申請的表單代號
            var formHelper = new FormHelper(formCode);

            //申請者帳號、申請者部門代號
            formHelper.ApplyAccount("USER_ACCOUNT", "USER_DEPT_CODE");

            //客製資訊
            var customDataObj = new
            {
                Timestamp = DateTimeOffset.Now,
                PurchaseID = 1
            };

            //要 CallBack 的 Url
            formHelper.Callback("https://myuofx.com.tw/api/sdk/callback", JsonConvert.SerializeObject(customDataObj));

            //夾帶附件
            //formHelper.AttachFiles(new List<FileModel>() { fileItem });

            //填入表單欄位值
            // 採購單號
            formHelper.FieldAdd("PurchaseID", FieldHelper.Base.Text("1"));
            // 供應商編號
            formHelper.FieldAdd("SupplierID", FieldHelper.Base.Text("3"));
            // 採購日期
            formHelper.FieldAdd("PurchaseDate", FieldHelper.Base.Date(DateTimeOffset.Parse("2025/3/14")));
            // 採購類型
            formHelper.FieldAdd("PurchaseType", FieldHelper.Base.SingleSelection(new SelectionValueModel("一般採購")));
            // 採購明細
            formHelper.FieldAdd("PurchaseDetail", FieldHelper.Base.Grid(new List<RowModel>
            {
                new RowModel()
                    .Column("PurchaseDetailID", "1")
                    .Column("ProductID", "1")
                    .Column("Quantity", 10)
                    .Column("UnitPrice", 130),

                new RowModel()
                    .Column("PurchaseDetailID", "2")
                    .Column("ProductID", "2")
                    .Column("Quantity", 25)
                    .Column("UnitPrice", 110),

                new RowModel()
                    .Column("PurchaseDetailID", "3")
                    .Column("ProductID", "4")
                    .Column("Quantity", 13)
                    .Column("UnitPrice", 95)
            }));

            //產生表單物件
            var doc = formHelper.Complete();

            return doc;
        }
    }
}
