using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Base;
using Ede.Uofx.OpenApi.Sdk.NetStd.Service;
using SDK_FirstSample.Models;

namespace SDK_FirstSample.Services;

public sealed class DepartmentSyncService
{
    private readonly SyncMessageLogger _messages;

    public DepartmentSyncService(SyncMessageLogger messages)
    {
        _messages = messages;
    }

    public async Task SyncAsync(List<HrDeptModel> sourceDepartments)
    {
        // 將 HR 部門整理成階層，再轉換為 UOFX 部門層級與部門資料
        var hierarchy = BuildHierarchy(sourceDepartments);
        var (levels, departments) = BuildDepartmentLevels(hierarchy, 1);

        await SyncDepartmentLevels(levels);
        await SyncDepartments(departments);
    }

    private static List<HrDeptModel> BuildHierarchy(List<HrDeptModel> departments)
    {
        // 將子部門掛到父部門，並取得最上層部門
        foreach (var department in departments.Where(department => department.HasSubDepartments))
        {
            department.SubDepts = departments
                .Where(child => child.ParentDeptCode == department.DeptCode)
                .ToList();
        }

        return departments
            .Where(department => string.IsNullOrEmpty(department.ParentDeptCode))
            .ToList();
    }

    private static (List<DeptLevelViewModel> Levels, List<DeptModel> Departments) BuildDepartmentLevels(
        List<HrDeptModel> hierarchy,
        int level)
    {
        // 建立目前層級的部門層級資料與 UOFX 部門資料
        var levels = new List<DeptLevelViewModel>();
        var departments = new List<DeptModel>();

        foreach (var department in hierarchy)
        {
            if (levels.All(item => item.Seq != level))
            {
                levels.Add(new DeptLevelViewModel
                {
                    Code = $"lev{level}",
                    Name = $"Level{level}",
                    Seq = level,
                    Active = true
                });
            }

            departments.Add(new DeptModel
            {
                Code = department.DeptCode,
                Name = department.DeptName,
                Active = department.IsActive,
                IncludeSubDept = department.HasSubDepartments,
                ParentCode = department.ParentDeptCode,
                Description = department.DeptDescription,
                DeptLevelCode = $"lev{level}",
                Seq = department.Order - 1
            });

            if (department.SubDepts.Count > 0)
            {
                var (childLevels, childDepartments) = BuildDepartmentLevels(department.SubDepts, level + 1);
                levels.AddRange(childLevels);
                departments.AddRange(childDepartments);
            }
        }

        return (levels, departments);
    }

    private async Task SyncDepartmentLevels(List<DeptLevelViewModel> hrDepartmentLevels)
    {
        // 比對 HR 與 UOFX 部門層級，分別更新已存在項目與新增缺少項目
        var uofDepartmentLevels = await UofxService.BASE.DeptLevel.Get();
        var uofLevelCodes = uofDepartmentLevels.Select(level => level.Code).ToHashSet();
        var levelsToUpdate = hrDepartmentLevels.Where(level => uofLevelCodes.Contains(level.Code)).ToList();
        var levelsToAdd = hrDepartmentLevels.Where(level => !uofLevelCodes.Contains(level.Code)).ToList();

        await UpdateDepartmentLevels(levelsToUpdate);
        await CreateDepartmentLevels(levelsToAdd);
    }

    private async Task UpdateDepartmentLevels(List<DeptLevelViewModel> levels)
    {
        foreach (var level in levels)
        {
            try
            {
                // 更新部門層級狀態與資料
                await UofxService.BASE.DeptLevel.UpdateStatus(new DeptLevelUpdateStatusModel
                {
                    Code = level.Code,
                    Active = true
                });
                await UofxService.BASE.DeptLevel.Update(new DeptLevelUpdateModel
                {
                    OriginalCode = level.Code,
                    Code = level.Code,
                    Name = level.Name
                });
                _messages.AddSuccess($"已更新 {level.Code} 部門層級");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法更新 {level.Code} 部門層級，error: {exception.Message}");
            }
        }
    }

    private async Task CreateDepartmentLevels(List<DeptLevelViewModel> levels)
    {
        foreach (var level in levels)
        {
            try
            {
                // 新增 HR 中尚未存在於 UOFX 的部門層級
                await UofxService.BASE.DeptLevel.Create(new DeptLevelCreateModel
                {
                    Code = level.Code,
                    Name = level.Name,
                    Active = level.Active ?? true
                });
                _messages.AddSuccess($"已新增 {level.Code} 部門層級");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法新增 {level.Code} 部門層級，error: {exception.Message}");
            }
        }
    }

    private async Task SyncDepartments(List<DeptModel> hrDepartments)
    {
        // 比對 HR 與 UOFX 部門，分成更新、新增及停用清單
        var uofDepartments = await UofxService.BASE.Department.Get(new DepartmentGetModel { IncludeSubDept = true });
        var hrDepartmentCodes = hrDepartments.Select(department => department.Code).ToHashSet();
        var uofDepartmentCodes = uofDepartments.Select(department => department.Code).ToHashSet();

        var departmentsToUpdate = hrDepartments.Where(department => uofDepartmentCodes.Contains(department.Code)).ToList();
        var departmentsToAdd = hrDepartments.Where(department => !uofDepartmentCodes.Contains(department.Code)).ToList();
        var departmentsToDeactivate = uofDepartments
            .Where(department => !hrDepartmentCodes.Contains(department.Code))
            .Select(department => new DeptModel
            {
                Code = department.Code,
                Active = department.Active,
                DeptLevelCode = department.DeptLevelCode
            })
            .ToList();

        await UpdateDepartments(departmentsToUpdate);
        await CreateDepartments(departmentsToAdd);
        await DeactivateDepartments(departmentsToDeactivate);
    }

    private async Task UpdateDepartments(List<DeptModel> departments)
    {
        foreach (var department in departments)
        {
            try
            {
                // 更新部門狀態與基本資料
                await UofxService.BASE.Department.UpdateState(new DepartmentUpdateStatusModel
                {
                    Code = department.Code,
                    Active = department.Active
                });
                await UofxService.BASE.Department.Update(new DepartmentUpdateModel
                {
                    OriginalCode = department.Code,
                    Code = department.Code,
                    Name = department.Name,
                    DeptLevelCode = department.DeptLevelCode,
                    Description = department.Description
                });
                _messages.AddSuccess($"已更新 {department.Code} 部門");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法更新 {department.Code} 部門，error: {exception.Message}");
            }
        }
    }

    private async Task CreateDepartments(List<DeptModel> departments)
    {
        foreach (var department in departments)
        {
            try
            {
                // 新增 HR 中尚未存在於 UOFX 的部門
                await UofxService.BASE.Department.Create(new DepartmentCreateModel
                {
                    ParentCode = department.ParentCode,
                    Code = department.Code,
                    DeptLevelCode = department.DeptLevelCode,
                    Name = department.Name,
                    Active = department.Active,
                    Description = department.Description
                });
                _messages.AddSuccess($"已新增 {department.Code} 部門");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法新增 {department.Code} 部門，error: {exception.Message}");
            }
        }
    }

    private async Task DeactivateDepartments(List<DeptModel> departments)
    {
        foreach (var department in departments)
        {
            try
            {
                // 停用 HR 已不存在的 UOFX 部門
                await UofxService.BASE.Department.UpdateState(new DepartmentUpdateStatusModel
                {
                    Code = department.Code,
                    Active = false
                });
                _messages.AddSuccess($"已停用 {department.Code} 部門");
            }
            catch (Exception exception)
            {
                _messages.AddError($"無法停用 {department.Code} 部門，error: {exception.Message}");
            }
        }
    }
}