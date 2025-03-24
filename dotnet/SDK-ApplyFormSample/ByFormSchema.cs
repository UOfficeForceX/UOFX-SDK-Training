using Ede.Uofx.FormSchema.UofxFormSchema;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;
using Newtonsoft.Json;

namespace SDK_FirstSample
{
    public static class ByFormSchema
    {
        /// <summary>
        /// 使用表單專屬檔案起單
        /// </summary>
        public static async Task<object> GenFormContentAsync()
        {
            // ---------- 要上傳的檔案應該要先上傳，再取得檔案物件 ----------

            //上傳檔案
            //var fileView = await UofxService.File.FileUpload(@"D:\sample\sample.pdf");

            //轉換成檔案物件
            //var fileItem = new Ede.Uofx.FormSchema.UofxFormSchema.FileItem()
            //{
            //    Id = fileView.Id,
            //    FileName = fileView.FileName
            //};

            //------------------------------------------------------------

            //客製資訊
            var customDataObj = new
            {
                Timestamp = DateTimeOffset.Now,
                PurchaseID = 1
            };

            //建立 外部起單物件
            var doc = new Ede.Uofx.FormSchema.UofxFormSchema.UofxFormSchema()
            {
                //申請者帳號
                Account = "USER_ACCOUNT",
                //申請者部門代號
                DeptCode = "USER_DEPT_CODE",
                //要 CallBack 的 Url
                CallBackUrl = "https://myuofx.com.tw/api/sdk/callback",
                //客製資訊: 填入起單時間
                CustomData = JsonConvert.SerializeObject(customDataObj),
                //將檔案物件入附件欄位
                //AttachFiles = new List<Ede.Uofx.FormSchema.UofxFormSchema.FileItem>()
                //{
                //    fileItem
                //}
            };

            //建立 表單欄位物件，填寫表單欄位資料
            doc.Fields = new Ede.Uofx.FormSchema.UofxFormSchema.UofxFormSchemaFields()
            {
                // 採購單號
                PurchaseID = "1",
                // 供應商編號
                SupplierID = "3",
                // 採購日期
                PurchaseDate = DateTimeOffset.Parse("2025/3/14"),
                // 採購類型
                PurchaseType = "一般採購",
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

            return doc;
        }
    }
}
