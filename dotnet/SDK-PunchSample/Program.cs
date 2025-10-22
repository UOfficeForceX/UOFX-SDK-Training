using Ede.Uofx.PubApi.Sdk.NetStd;
using Ede.Uofx.PubApi.Sdk.NetStd.Models.Eip;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;
using System;
using System.Reflection.Emit;

//設定金鑰
UofxService.Key = "xxx";

//設定 UOF X 站台網址
UofxService.UofxServerUrl = "https://myuofx.com.tw";

try
{
	//請求打卡紀錄的物件
	var punchModel = new UserPunchApiRequestModel
	{
		//公司代碼
		CorpCode = "ede",
		//起訖時間
		StartDate = DateTimeOffset.Now.AddDays(-1),
		EndDate = DateTimeOffset.Now,
		//打卡類型類型(0:全部,1:出勤,2:外出)
		QueryPunchHistoryType = 0,
		//查詢結果的時區
		TimeZoneId = "Taipei Standard Time",

		//人員資料類型(0:Account, 1:EmployeeNo)
		UserType = 0,
		//人員識別碼(依UserType填入對應值)
		UserCode = "Justin"
	};

	//取得打卡紀錄
	var result = await UofxService.EIP.Punch.GetUserPunch(punchModel);
	Console.WriteLine(UofxService.Json.Convert(result));

	//請求部門人員打卡紀錄的物件
	var deptPunchModel = new DeptUserPunchApiRequestModel
	{
		//公司代碼
		CorpCode = "ede",
		//起訖時間
		StartDate = DateTimeOffset.Now.AddDays(-1),
		EndDate = DateTimeOffset.Now,
		//打卡類型類型(0:全部,1:出勤,2:外出)
		QueryPunchHistoryType = 0,
		//查詢結果的時區
		TimeZoneId = "Taipei Standard Time",

		//部門代號
		DeptCode = "dept1",
		//是否包含子部門
		ContainChild = false
	};

	//取得部門人員打卡紀錄
	var deptResult = await UofxService.EIP.Punch.GetDeptUserPunch(deptPunchModel);
	Console.WriteLine(UofxService.Json.Convert(deptResult));
}
catch (Exception e)
{
	//將 exception 轉換成較容易判斷的 model
	var model = UofxService.Error.ConvertToModel(e);
	//將 model 轉成 json 格式印出
	Console.WriteLine(UofxService.Json.Convert(model));
}



