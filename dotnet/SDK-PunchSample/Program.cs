using Ede.Uofx.OpenApi.Sdk.NetStd;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Base;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Bpm;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Eip;
using Ede.Uofx.OpenApi.Sdk.NetStd.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();

// 設定 UOFX 站台網址
UofxService.UofxServerUrl = builder.Configuration["UofxServiceSettings:UofxServiceUrl"];
// 設定公司代號
UofxService.CorpCode = builder.Configuration["UofxServiceSettings:CorpCode"];
// 設定金鑰
UofxService.Key = builder.Configuration["UofxServiceSettings:Key"];

try
{
    // 取得單人出勤刷卡紀錄
    var userResult = await UofxService.EIP.Punch.GetUserPunch(new UserPunchApiRequestModel
	{
        // 使用者資訊
        User = UserModel.Create(UserType.Account, "USER_ACCOUNT"),
        // 查詢日期區間
        DateRange = new SearchDateRange
        {
			Since = DateTimeOffset.Now.AddDays(-7),
            Until = DateTimeOffset.Now
        },
        // 查詢類型 (0:全部, 1:出勤, 2:外出)
        QueryPunchHistoryType = 0,
        // 時區 Id (ex: "Taipei Standard Time")
        TimeZoneId = "Taipei Standard Time"
    });

	Console.WriteLine(UofxService.Json.Convert(userResult));

    // 取得部門出勤刷卡紀錄
    var deptResult = await UofxService.EIP.Punch.GetDeptUserPunch(new DeptUserPunchApiRequestModel
    {
        // 部門代碼
        DeptCode = "DEPT_CODE",
        // 是否包含子部門
        IncludeSubDept = true,
        // 查詢日期區間
        DateRange = new SearchDateRange
        {
            Since = DateTimeOffset.Now.AddDays(-7),
            Until = DateTimeOffset.Now
        },
        // 查詢類型 (0:全部, 1:出勤, 2:外出)
        QueryPunchHistoryType = 0,
        // 時區 Id (ex: "Taipei Standard Time")
        TimeZoneId = "Taipei Standard Time"
    });

    Console.WriteLine(UofxService.Json.Convert(deptResult));
}
catch (Exception e)
{
	//將 exception 轉換成較容易判斷的 model
	var model = UofxService.Error.ConvertToModel(e);
	//將 model 轉成 json 格式印出
	Console.WriteLine(UofxService.Json.Convert(model));
}



