using Ede.Uofx.OpenApi.Sdk.NetStd;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Base;
using Ede.Uofx.OpenApi.Sdk.NetStd.Service;

namespace SDK_FirstSample.Services;

public sealed class ResetService
{
    private readonly SyncMessageLogger _messages;

    public ResetService(SyncMessageLogger messages)
    {
        _messages = messages;
    }

    public async Task ResetAsync()
    {
        // 依序刪除範例建立的人員、部門、部門層級、職稱與職務
        await DeleteEmployees(["costanza", "natty", "julina", "catharina", "nita", "cary", "gelya", "carmelina", "raviv", "chev", "ezmeralda", "rina", "dory", "conway", "rebekkah", "kath", "betteanne", "antons"]);
        await DeleteDepartments(["BD", "RD", "T", "HR", "S"]);
        await DeleteDepartmentLevels(["lev1", "lev2", "lev3"]);
        await DeleteJobTitles(["PM", "TA", "SA", "UX", "QA", "BA", "BE", "IS", "FE"]);
        await DeleteJobFunctions(["SEC", "UI", "DEV", "SUP", "BE", "PM"]);
        await DeleteJobFunctionCategories(["資訊安全管理", "使用者介面設計", "軟體開發", "技術支援", "後端開發", "專案管理"]);
    }

    private async Task DeleteEmployees(List<string> accounts)
    {
        // 刪除範例人員
        foreach (var account in accounts)
        {
            try
            {
                await UofxService.BASE.OrgEmpl.Delete(UserModel.Create(UserType.Account, account));
                _messages.AddSuccess($"已刪除 {account} 人員");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法刪除 {account} 人員，error: {exception.Message}");
            }
        }
    }

    private async Task DeleteDepartments(List<string> codes)
    {
        // 刪除範例部門
        foreach (var code in codes)
        {
            try
            {
                await UofxService.BASE.Department.Remove(new DepartmentRemoveModel { Code = code });
                _messages.AddSuccess($"已刪除 {code} 部門");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法刪除 {code} 部門，error: {exception.Message}");
            }
        }
    }

    private async Task DeleteDepartmentLevels(List<string> codes)
    {
        // 刪除範例部門層級
        foreach (var code in codes)
        {
            try
            {
                await UofxService.BASE.DeptLevel.Delete(new DeptLevelDeleteModel { Code = code });
                _messages.AddSuccess($"已刪除 {code} 部門層級");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法刪除 {code} 部門層級，error: {exception.Message}");
            }
        }
    }

    private async Task DeleteJobTitles(List<string> codes)
    {
        // 刪除範例職稱
        foreach (var code in codes)
        {
            try
            {
                await UofxService.BASE.JobTitle.Delete(new JobTitleDeleteModel { Code = code });
                _messages.AddSuccess($"已刪除 {code} 職稱");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法刪除 {code} 職稱，error: {exception.Message}");
            }
        }
    }

    private async Task DeleteJobFunctions(List<string> codes)
    {
        // 刪除範例職務
        foreach (var code in codes)
        {
            try
            {
                await UofxService.BASE.JobFunc.Delete(new JobFuncDeleteModel { Code = code });
                _messages.AddSuccess($"已刪除 {code} 職務");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法刪除 {code} 職務，error: {exception.Message}");
            }
        }
    }

    private async Task DeleteJobFunctionCategories(List<string> categories)
    {
        // 刪除範例職務類別
        foreach (var category in categories)
        {
            try
            {
                await UofxService.BASE.JobFuncCategory.Delete(new JobFuncCategoryDeleteModel { Category = category });
                _messages.AddSuccess($"已刪除 {category} 職務類別");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法刪除 {category} 職務類別，error: {exception.Message}");
            }
        }
    }
}