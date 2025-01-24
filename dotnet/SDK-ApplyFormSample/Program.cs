using Ede.Uofx.FormSchema.UofxFormSchema;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;

//設定金鑰
UofxService.Key = "xxx";

//設定 UOF X 站台網址
UofxService.UofxServerUrl = "https://myuofx.com.tw";

//建立 外部起單物件
var doc = new Ede.Uofx.FormSchema.UofxFormSchema.UofxFormSchema()
{
    //申請者帳號
    Account = "Justin",
    //申請者部門代號
    DeptCode = "RD",
};

//建立 表單欄位物件，填寫表單欄位資料
doc.Fields = new Ede.Uofx.FormSchema.UofxFormSchema.UofxFormSchemaFields()
{
    // 採購單號
    C002 = "PO20241014",
    // 申請人
    C003 = new List<AccountItem>
    {
        new AccountItem { DeptCode = "RD", Account = "Justin" }
    },
    // 供應商名稱
    C023 = "OO工業有限公司",
    // 採購日期
    C006 = DateTimeOffset.Parse("2024/10/14"),
    // 採購類型
    C008 = "緊急採購",
    // 交貨地點
    C007 = "高雄市前鎮區復興四路2號2樓之2",
    // 請購明細
    C012 = new List<C012Row>
    {
        new C012Row
        {
            C016 = "P-1001",
            C017 = "高速列印紙",
            C018 = "A4 / 80gsm / 500張",
            C019 = "箱",
            C020 = 10,
            C021 = "急件，需於本週五前交貨"
        },
        new C012Row
        {
            C016 = "P-2003",
            C017 = "工業級螺絲刀組",
            C018 = "5支組 / 含十字與一字",
            C019 = "組",
            C020 = 5,
            C021 = "需提供保固兩年"
        },
        new C012Row
        {
            C016 = "P-3010",
            C017 = "無線滑鼠",
            C018 = "2.4G無線 / 含電池",
            C019 = "個",
            C020 = 20,
            C021 = "需提供產品說明書"
        }
    },
    // 請購單位需求說明
    C013 = "本次申請旨在補充辦公室耗材與常用工具，確保業務進行順暢。列印紙需求為緊急採購，請協助加速處理。",
    // 採購單位說明
    C015 = "滑鼠、螺絲刀組已向供應商確認庫存充足，預計三天內可交貨。列印紙的交貨時程已交代供應商儘量配合需求時限。"
};

try
{
	//呼叫站台進行起單
	var traceId = await UofxService.BPM.ApplyForm(doc);

	Console.WriteLine($"Trace Id: {traceId}");
}
catch (Exception e)
{
	//將 exception 轉換成較容易判斷的 model
	var model = UofxService.Error.ConvertToModel(e);
	//將 model 轉成 json 格式印出
	Console.WriteLine(UofxService.Json.Convert(model));
}



