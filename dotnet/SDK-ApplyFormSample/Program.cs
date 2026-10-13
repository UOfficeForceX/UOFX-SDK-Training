using Ede.Uofx.OpenApi.Sdk.NetStd.Service;
using SDK_ErpApplyFormSample;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();

// 設定 UOFX 站台網址
UofxService.UofxServerUrl = builder.Configuration["UofxServiceSettings:UofxServiceUrl"];
// 設定公司代號
UofxService.CorpCode = builder.Configuration["UofxServiceSettings:CorpCode"];
// 設定金鑰
UofxService.Key = builder.Configuration["UofxServiceSettings:Key"];

// 使用 起單檔案 產生內容
//var doc = await ByFormSchema.GenFormContentAsync();

// 使用 表單代號 產生內容
var doc = await ByFormCode.GenFormContentAsync("Purchase");

try
{
    // 呼叫站台進行起單
    var traceId = await UofxService.BPM.Apply.ApplyForm(doc);
    Console.WriteLine($"Trace Id: {traceId}");
}
catch (Exception e)
{
    // 將 exception 轉換成較容易判斷的 model
    var model = UofxService.Error.ConvertToModel(e);
    // 將 model 轉成 json 格式印出
    Console.WriteLine(UofxService.Json.Convert(model));
}

// 註冊 Swagger 服務，用於生成 API 文件
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// 如果是開發環境，啟用 Swagger UI 方便測試 API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
