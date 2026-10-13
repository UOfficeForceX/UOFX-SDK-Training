using Ede.Uofx.OpenApi.Sdk.NetStd;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Base;
using Ede.Uofx.OpenApi.Sdk.NetStd.Service;
using SDK_FirstSample.Models;

namespace SDK_FirstSample.Services;

public sealed class EmployeeSyncService
{
    private readonly SyncMessageLogger _messages;

    public EmployeeSyncService(SyncMessageLogger messages)
    {
        _messages = messages;
    }

    public async Task SyncAsync(
        List<HrEmplModel> sourceEmployees,
        List<HrJobTitleModel> jobTitles,
        List<HrJobFunctionModel> jobFunctions)
    {
        // 先同步職稱職務，再同步人員與部門主管
        await SyncJobTitlesAndFunctions(jobTitles, jobFunctions);
        var employees = ConvertToEmployeeModels(sourceEmployees);
        await SyncEmployees(employees);
        await SyncSuperiors(sourceEmployees);
    }

    private async Task SyncJobTitlesAndFunctions(
        List<HrJobTitleModel> hrJobTitles,
        List<HrJobFunctionModel> hrJobFunctions)
    {
        // 取得 UOFX 職稱職務代號，用於判斷要更新或新增
        var uofJobTitles = await UofxService.BASE.JobTitle.Get();
        var uofJobFunctions = await UofxService.BASE.JobFunc.Get();
        var uofJobTitleCodes = uofJobTitles.Select(jobTitle => jobTitle.Code).ToHashSet();
        var uofJobFunctionCodes = uofJobFunctions.Select(jobFunction => jobFunction.Code).ToHashSet();

        foreach (var jobTitle in hrJobTitles)
        {
            try
            {
                if (uofJobTitleCodes.Contains(jobTitle.JobTitleCode))
                {
                    await UofxService.BASE.JobTitle.UpdateStatus(new JobTitleUpdateStatusModel
                    {
                        Code = jobTitle.JobTitleCode,
                        Active = true
                    });
                    await UofxService.BASE.JobTitle.Update(new JobTitleUpdateModel
                    {
                        OriginalCode = jobTitle.JobTitleCode,
                        Code = jobTitle.JobTitleCode,
                        Title = jobTitle.JobTitleName
                    });
                    _messages.AddSuccess($"已更新 {jobTitle.JobTitleCode} 職稱");
                }
                else
                {
                    await UofxService.BASE.JobTitle.Create(new JobTitleViewModel
                    {
                        Rank = 15,
                        Seq = 1,
                        Code = jobTitle.JobTitleCode,
                        Title = jobTitle.JobTitleName,
                        Active = true
                    });
                    _messages.AddSuccess($"已新增 {jobTitle.JobTitleCode} 職稱");
                }
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法同步 {jobTitle.JobTitleCode} 職稱，error: {exception.Message}");
            }
        }

        foreach (var jobFunction in hrJobFunctions)
        {
            try
            {
                if (uofJobFunctionCodes.Contains(jobFunction.JobFunctionCode))
                {
                    await UofxService.BASE.JobFunc.UpdateStatus(new JobFuncUpdateStatusModel
                    {
                        Code = jobFunction.JobFunctionCode,
                        Active = true
                    });
                    await UofxService.BASE.JobFunc.Update(new JobFuncUpdateModel
                    {
                        OriginalCode = jobFunction.JobFunctionCode,
                        Code = jobFunction.JobFunctionCode,
                        JobFunc = jobFunction.JobFunctionName
                    });
                    _messages.AddSuccess($"已更新 {jobFunction.JobFunctionCode} 職務");
                }
                else
                {
                    await UofxService.BASE.JobFunc.Create(new JobFuncCreateModel
                    {
                        CategoryName = jobFunction.JobFunctionName,
                        Code = jobFunction.JobFunctionCode,
                        JobFunc = jobFunction.JobFunctionName,
                        Active = true
                    });
                    _messages.AddSuccess($"已新增 {jobFunction.JobFunctionCode} 職務");
                }
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法同步 {jobFunction.JobFunctionCode} 職務，error: {exception.Message}");
            }
        }
    }

    private static List<EmplModel> ConvertToEmployeeModels(List<HrEmplModel> sourceEmployees)
    {
        // 同一帳號可能有多筆部門資料，依帳號合併為一筆 UOFX 人員資料
        return sourceEmployees
            .GroupBy(employee => employee.Account)
            .Select(group => new EmplModel
            {
                Account = group.Key,
                Name = group.First().Name,
                Gender = group.First().Gender == "male" ? "0" : (group.First().Gender == "female" ? "1" : "2"),
                EnglishName = group.First().EnglishName,
                EmployeeNumber = group.First().EmployeeID,
                ExpiredTime = group.First().ExpiredTime,
                ResignationDate = group.First().ResignationDate,
                IdCardNumber = group.First().IDNumber,
                BirthDate = new DateTime(group.First().BirthDate.Year + 1911, group.First().BirthDate.Month, group.First().BirthDate.Day),
                PhoneNumber = group.First().PhoneNumber,
                BusinessCard = group.First().CardTitle,
                HireDate = group.First().HireDate,
                Email = group.First().PrimaryEmail,
                EmailEx = group.First().SecondaryEmail,
                Active = group.First().AccountActive,
                Locked = group.First().AccountLocked,
                Depts = group.Select(employee => new EmpCreateOfDeptItemRequestModel
                {
                    // 部門、職稱及職務均使用對應代號
                    Code = employee.DeptID,
                    IsMainDept = employee.PrimaryDepartment,
                    JobTitleCode = employee.JobTitleID,
                    JobFuncs = employee.JobFunctionIDs
                }).ToList()
            })
            .ToList();
    }

    private async Task SyncEmployees(List<EmplModel> hrEmployees)
    {
        // 依 UOFX 是否已有此帳號，分類為更新或新增；過期的帳號另列停用
        var employeesToUpdate = new List<EmplModel>();
        var employeesToAdd = new List<EmplModel>();
        var employeesToDeactivate = new List<EmplModel>();

        foreach (var employee in hrEmployees)
        {
            var existingEmployee = await UofxService.BASE.OrgEmpl.GetProfile(UserModel.Create(UserType.Account, employee.Account));
            if (existingEmployee != null && existingEmployee.Account != null)
            {
                employeesToUpdate.Add(employee);
            }
            else
            {
                employeesToAdd.Add(employee);
            }

            if (employee.ExpiredTime != null && DateTime.Now > employee.ExpiredTime)
            {
                employeesToDeactivate.Add(employee);
            }
        }

        await UpdateEmployees(employeesToUpdate);
        await CreateEmployees(employeesToAdd);
        await DeactivateEmployees(employeesToDeactivate);
    }

    private async Task UpdateEmployees(List<EmplModel> employees)
    {
        foreach (var employee in employees)
        {
            try
            {
                // 更新帳號啟用與鎖定狀態
                await UofxService.BASE.OrgEmpl.UpdateAcctStatus(new EmplUpdateAcctStatusRequestModel
                {
                    User = UserModel.Create(UserType.Account, employee.Account),
                    Active = employee.Active
                });
                await UofxService.BASE.OrgEmpl.UpdateAcctLocked(new EmplUpdateAcctLockedRequestModel
                {
                    User = UserModel.Create(UserType.Account, employee.Account),
                    Locked = employee.Locked
                });
                // 更新人員基本資料
                await UofxService.BASE.OrgEmpl.Update(new EmplUpdateProfileRequestModel
                {
                    User = UserModel.Create(UserType.Account, employee.Account),
                    Account = employee.Account,
                    Name = employee.Name,
                    EmployeeNumber = employee.EmployeeNumber,
                    Gender = employee.Gender,
                    EnglishName = employee.EnglishName,
                    IdCardNumber = employee.IdCardNumber,
                    BirthDate = employee.BirthDate,
                    PhoneNumber = employee.PhoneNumber,
                    BusinessCard = employee.BusinessCard,
                    HireDate = employee.HireDate,
                    Email = employee.Email,
                    EmailEx = employee.EmailEx,
                    LoginValidation = new List<LoginValidationItemModel>()
                    {
                        new LoginValidationItemModel
                        {
                            Type = LoginValidationType.Uof,
                            Enabled = true
                        }
                    }
                });
                // 更新帳號過期時間與離職日期
                await UofxService.BASE.OrgEmpl.UpdateAcctExpiredTime(new EmplUpdateAcctExpiredTimeRequestModel
                {
                    User = UserModel.Create(UserType.Account, employee.Account),
                    ExpiredTime = employee.ExpiredTime
                });
                await UofxService.BASE.OrgEmpl.UpdateEmplResignationDate(new EmplUpdateResignationDateRequestModel
                {
                    User = UserModel.Create(UserType.Account, employee.Account),
                    ResignationDate = employee.ResignationDate
                });
                // 更新人員所屬部門、職稱與職務
                await UofxService.BASE.OrgEmpl.UpdateEmplDept(new EmplUpdateDeptRequestModel
                {
                    User = UserModel.Create(UserType.Account, employee.Account),
                    Depts = employee.Depts.Select(department => new DeptRequestModel
                    {
                        Code = department.Code,
                        IsMainDept = department.IsMainDept,
                        JobTitleCode = department.JobTitleCode,
                        JobFuncs = department.JobFuncs
                    }).ToList()
                });
                _messages.AddSuccess($"已更新 {employee.Account} 人員");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法更新 {employee.Account} 人員，error: {exception.Message}");
            }
        }
    }

    private async Task CreateEmployees(List<EmplModel> employees)
    {
        foreach (var employee in employees)
        {
            try
            {
                // 新增 HR 中尚未存在於 UOFX 的人員
                await UofxService.BASE.OrgEmpl.CreateEmpl(new EmpCreateProfileRequestModel
                {
                    Account = employee.Account,
                    Name = employee.Name,
                    EnglishName = employee.EnglishName,
                    EmployeeNumber = employee.EmployeeNumber,
                    ExpiredTime = employee.ExpiredTime,
                    Gender = employee.Gender,
                    IdCardNumber = employee.IdCardNumber,
                    BirthDate = employee.BirthDate,
                    PhoneNumber = employee.PhoneNumber,
                    BusinessCard = employee.BusinessCard,
                    HireDate = employee.HireDate,
                    Email = employee.Email,
                    EmailEx = employee.EmailEx,
                    Depts = employee.Depts,
                    LoginValidation = new List<LoginValidationItemModel>()
                    {
                        new LoginValidationItemModel
                        {
                            Type = LoginValidationType.Uof,
                            Enabled = true
                        }
                    }
                });
                _messages.AddSuccess($"已新增 {employee.Account} 人員");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法新增 {employee.Account} 人員，error: {exception.Message}");
            }
        }
    }

    private async Task DeactivateEmployees(List<EmplModel> employees)
    {
        foreach (var employee in employees)
        {
            try
            {
                // 停用已過期的人員帳號
                await UofxService.BASE.OrgEmpl.UpdateAcctStatus(new EmplUpdateAcctStatusRequestModel
                {
                    User = UserModel.Create(UserType.Account, employee.Account),
                    Active = false
                });
                _messages.AddSuccess($"已停用 {employee.Account} 人員");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法停用 {employee.Account} 人員，error: {exception.Message}");
            }
        }
    }

    private async Task SyncSuperiors(List<HrEmplModel> sourceEmployees)
    {
        // 依 HR 主管資料設定 UOFX 部門主管
        var superiors = sourceEmployees
            .Where(employee => employee.IsSupervisor)
            .Select(employee => new SuperiorModel
            {
                Account = employee.Account,
                DeptCode = employee.DeptID
            })
            .ToList();

        foreach (var superior in superiors)
        {
            try
            {
                await UofxService.BASE.Department.Manager.Set(new DeptSetManagerModel
                {
                    Code = superior.DeptCode,
                    Manager = UserModel.Create(UserType.Account, superior.Account)
                });
                _messages.AddSuccess($"已設定 {superior.Account} 為 {superior.DeptCode} 部門主管");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法設定 {superior.Account} 為 {superior.DeptCode} 部門主管，error: {exception.Message}");
            }
        }
    }
}