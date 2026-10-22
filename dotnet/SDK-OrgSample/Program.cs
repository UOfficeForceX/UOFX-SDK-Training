using Ede.Uofx.OpenApi.Sdk.NetStd.Service;
using SDK_FirstSample.Samples;
using SDK_FirstSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();

// 設定 UOFX 站台網址
UofxService.UofxServerUrl = builder.Configuration["UofxServiceSettings:UofxServiceUrl"];
// 設定公司代號
UofxService.CorpCode = builder.Configuration["UofxServiceSettings:CorpCode"];
// 設定金鑰
UofxService.Key = builder.Configuration["UofxServiceSettings:Key"];

var messages = new SyncMessageLogger();
var departmentSyncService = new DepartmentSyncService(messages);
var employeeSyncService = new EmployeeSyncService(messages);

try
{
    // 同步 HR 部門與人員資料
    await departmentSyncService.SyncAsync(HrSampleData.Departments);
    await employeeSyncService.SyncAsync(
        HrSampleData.Employees,
        HrSampleData.JobTitles,
        HrSampleData.JobFunctions);

    // 更新使用者權限
    await UofxService.BASE.Permission.UpdateUserPermission();

    // 重製範例資料時取消註解
    //await new ResetService(messages).ResetAsync();

    // 列印錯誤與成功訊息
    messages.Print();
}
catch (Exception exception)
{
    // 將 exception 轉換成較容易判斷的 model，再轉成 JSON 輸出
    var model = UofxService.Error.ConvertToModel(exception);
    Console.WriteLine(UofxService.Json.Convert(model));
}
