using Ede.Uofx.FormSchema.UofxFormSchema;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;
using SDK_FirstSample;
using SDK_FirstSample.Service;

// 設定金鑰
UofxService.Key = "xxx";

// 設定 UOF X 站台網址
UofxService.UofxServerUrl = "https://myuofx.com.tw/";

// 使用 表單專屬檔案 產生內容
//var doc = await ByFormSchema.GenFormContentAsync();

// 使用 表單代號 產生內容
var doc = await ByFormCode.GenFormContentAsync("Purchase");

try
{ 
    // 呼叫站台進行起單
    var traceId = await UofxService.BPM.ApplyForm(doc);
    Console.WriteLine($"Trace Id: {traceId}");
}
catch (Exception e)
{
    // 將 exception 轉換成較容易判斷的 model
    var model = UofxService.Error.ConvertToModel(e);
    // 將 model 轉成 json 格式印出
    Console.WriteLine(UofxService.Json.Convert(model));
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ERPService>();
builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();

var app = builder.Build();
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
