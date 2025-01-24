using Ede.Uofx.PubApi.Sdk.NetStd.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            var fileView = await UofxService.File.FileUpload(@"D:\sample\sample.pdf");

            //轉換成檔案物件
            var fileItem = new Ede.Uofx.FormSchema.UofxFormSchema.FileItem()
            {
                Id = fileView.Id,
                FileName = fileView.FileName
            };

            //------------------------------------------------------------


            //建立 外部起單物件
            var doc = new Ede.Uofx.FormSchema.UofxFormSchema.UofxFormSchema()
            {
                //申請者帳號
                Account = "Justin",
                //申請者部門代號
                DeptCode = "RD",
                ////要 CallBack 的 Url
                //CallBackUrl = "https://hr-system.com.tw/uofx-sdk/callback",
                ////客製資訊: 填入起單時間
                //CustomData = $"{DateTimeOffset.Now}",
                //將檔案物件入附件欄位
                AttachFiles = new List<Ede.Uofx.FormSchema.UofxFormSchema.FileItem>()
                {
                    fileItem
                }
            };

            //建立 表單欄位物件，填寫表單欄位資料
            doc.Fields = new Ede.Uofx.FormSchema.UofxFormSchema.UofxFormSchemaFields()
            {
                C002 = "測試",
            };

            return doc;
        }
    }
}
