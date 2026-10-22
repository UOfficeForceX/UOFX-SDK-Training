using Ede.Uofx.FormSchema.UofxFormSchema;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Bpm;
using Ede.Uofx.OpenApi.Sdk.NetStd.Service;
using System.Text.Json;

namespace SDK_ErpApplyFormSample
{
    public static class ByFormSchema
    {
        /// <summary>
        /// 使用表單專屬檔案起單
        /// </summary>
        public static async Task<FormApplyRequestModel> GenFormContentAsync()
        {
            // ---------- 要上傳的檔案應該要先上傳，再取得檔案物件 ----------

            //上傳檔案
            //var fileView = await UofxService.File.FileUpload(@"D:\sample\sample.pdf");

            //------------------------------------------------------------

            //客製資訊
            var customDataObj = new
            {
                Timestamp = DateTimeOffset.Now,
                PurchaseID = "1"
            };

            //建立 外部起單物件
            var doc = new UofxFormSchema()
            {
                //申請者帳號
                Account = "USER_ACCOUNT",
                //申請者部門代號
                DeptCode = "USER_DEPT_CODE",
                //要 CallBack 的 Url
                //若要呼叫本專案的 CallBack API（SDKController.cs）
                //請設定為 "http://[本機IP]:62206/api/sdk/callback" （Port 依據 launchSettings.json 設定）
                CallBackUrl = "https://myuofx.com.tw/api/sdk/callback",
                //客製資訊: 填入起單時間
                CustomData = JsonSerializer.Serialize(customDataObj),
                //將檔案物件入附件欄位
                //AttachFiles = new List<Ede.Uofx.FormSchema.UofxFormSchema.FileItem>()
                //{
                //    fileItem
                //}
            };

            //建立 表單欄位物件，填寫表單欄位資料
            doc.Fields = new UofxFormSchemaFields()
            {
                // 採購單號
                PurchaseID = "1",
                // 供應商編號
                SupplierID = "3",
                // 採購日期
                PurchaseDate = DateTimeOffset.Parse("2025/3/14"),
                // 採購類型
                PurchaseType = new SelectionValue
                {
                    Name = "一般採購",
                },
                // 採購明細
                PurchaseDetail = new List<PurchaseDetailRow>
                {
                    new PurchaseDetailRow
                    {
                        PurchaseDetailID = "1",
                        ProductID = "1",
                        Quantity = 10,
                        UnitPrice = 130,
                    },
                    new PurchaseDetailRow
                    {
                        PurchaseDetailID = "2",
                        ProductID = "2",
                        Quantity = 25,
                        UnitPrice = 110,
                    },
                    new PurchaseDetailRow
                    {
                        PurchaseDetailID = "3",
                        ProductID = "4",
                        Quantity = 13,
                        UnitPrice = 95,
                    }
                }
            };

            // 轉換為外部起單模型
            var result = await UofxService.BPM.Convert.SchemaToApplyModel(doc);
            
            return result;
        }
    }
}
