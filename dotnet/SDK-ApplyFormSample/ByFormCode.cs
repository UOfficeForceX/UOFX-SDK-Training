using Ede.Uofx.PubApi.Sdk.NetStd.Helpers;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;

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
            var fileView = await UofxService.File.FileUpload(@"D:\sample\sample.pdf");

            //轉換成檔案物件
            var fileItem = new FileModel(fileView.Id, fileView.FileName);

            //------------------------------------------------------------
           

            //申請的表單代號
            var formHelper = new FormHelper(formCode);

            //申請者帳號、申請者部門代號
            formHelper.ApplyAccount("Justin", "PRX");

            //夾帶附件
            formHelper.AttachFiles(new List<FileModel>() { fileItem });

            //填入表單欄位值
            formHelper.FieldAdd("C002", FieldHelper.Base.Text("測試"));

            //產生表單物件
            var doc = formHelper.Complete();

            return doc;
        }
    }
}
