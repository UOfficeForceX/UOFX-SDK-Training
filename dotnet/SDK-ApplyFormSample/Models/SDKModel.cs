namespace SDK_FirstSample.Models
{
    public class CallBackModel
    {
        //追蹤代號，對應呼叫起單時拿到的 traceid
        public string TraceId { get; set; }
        //起單結果: Success、Failure
        public CallbackType Type { get; set; }
        //成功的資訊
        public FormApplyResponseModel UofxData { get; set; }
        //客製資訊
        public string CustomData { get; set; }
        //失敗的訊息
        public string ErrorMsg { get; set; }
    }

    public enum CallbackType
    {
        Success,
        Failure
    }

    public class FormApplyResponseModel
    {
        //表單名稱
        public string FormName { get; set; }
        //表單編號
        public string FormSn { get; set; }
    }
}
