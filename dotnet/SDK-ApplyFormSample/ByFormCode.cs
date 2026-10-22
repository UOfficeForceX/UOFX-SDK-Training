using Ede.Uofx.OpenApi.Sdk.NetStd.Helpers;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Base;
using Ede.Uofx.OpenApi.Sdk.NetStd.Service;
using Ede.Uofx.OpenApi.Sdk.NetStd;
using System.Text.Json;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Bpm;

namespace SDK_ErpApplyFormSample
{
    public static class ByFormCode
    {
        /// <summary>
        /// 使用表單代碼起單
        /// </summary>
        public static async Task<FormApplyRequestModel> GenFormContentAsync(string formCode)
        {
            // 可先取得表單設計的欄位資訊
            //var fieldInfo = await UofxService.BPM.Form.GetFieldInfo(formCode);
            //Console.WriteLine(UofxService.Json.Convert(fieldInfo));

            // ---------- 要上傳的檔案應該要先上傳，再取得檔案物件 ----------

            //上傳檔案
            //var fileView = await UofxService.File.FileUpload(@"D:\sample\sample.pdf");

            //------------------------------------------------------------

            //申請的表單代號
            var formHelper = new FormHelper(formCode);

            //申請者帳號、申請者部門代號
            formHelper.ApplyAccount(UserModel.Create(UserType.Account, "USER_ACCOUNT"), "USER_DEPT_CODE");

            //客製資訊
            var customDataObj = new
            {
                Timestamp = DateTimeOffset.Now,
                PurchaseID = "1"
            };

            //要 CallBack 的 Url
            //若要呼叫本專案的 CallBack API（SDKController.cs）
            //請設定為 "http://[本機IP]:62206/api/sdk/callback" （Port 依據 launchSettings.json 設定）
            formHelper.Callback("https://myuofx.com.tw/api/sdk/callback", JsonSerializer.Serialize(customDataObj));

            //夾帶附件
            //formHelper.AttachFiles(new List<FileViewModel>() { fileView });

            //填入表單欄位值
            // 採購單號
            formHelper.FieldAdd("PurchaseID", FieldHelper.Base.Text("1"));
            // 供應商編號
            formHelper.FieldAdd("SupplierID", FieldHelper.Base.Text("3"));
            // 採購日期
            formHelper.FieldAdd("PurchaseDate", FieldHelper.Base.Date(DateTime.Parse("2025/3/14")));
            // 採購類型
            formHelper.FieldAdd("PurchaseType", FieldHelper.Base.SingleSelection(new SelectionValueModel("一般採購")));
            // 採購明細
            formHelper.FieldAdd("PurchaseDetail", FieldHelper.Base.Grid(new List<RowModel>
            {
                new RowModel()
                    .Column("PurchaseDetailID", FieldHelper.Base.Text("1"))
                    .Column("ProductID", FieldHelper.Base.Text("1"))
                    .Column("Quantity", FieldHelper.Base.Number(10))
                    .Column("UnitPrice", FieldHelper.Base.Number(130)),

                new RowModel()
                    .Column("PurchaseDetailID", FieldHelper.Base.Text("2"))
                    .Column("ProductID", FieldHelper.Base.Text("2"))
                    .Column("Quantity", FieldHelper.Base.Number(25))
                    .Column("UnitPrice", FieldHelper.Base.Number(110)),

                new RowModel()
                    .Column("PurchaseDetailID", FieldHelper.Base.Text("3"))
                    .Column("ProductID", FieldHelper.Base.Text("4"))
                    .Column("Quantity", FieldHelper.Base.Number(13))
                    .Column("UnitPrice", FieldHelper.Base.Number(95))
            }));

            //產生表單物件
            var doc = formHelper.Complete();

            return doc;
        }
    }
}
