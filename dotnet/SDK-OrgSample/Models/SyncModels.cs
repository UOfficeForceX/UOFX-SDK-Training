using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Base;

namespace SDK_FirstSample.Models;

public class DeptModel
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
    public bool IncludeSubDept { get; set; }
    public string? ParentCode { get; set; }
    public string? Description { get; set; }
    public string DeptLevelCode { get; set; } = string.Empty;
    public int Seq { get; set; }
    public List<DeptModel> SubDepts { get; set; } = [];
}

public class SuperiorModel
{
    public string DeptCode { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
}

public class EmplModel
{
    public string Account { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string EmployeeNumber { get; set; } = string.Empty;
    public DateTime? ExpiredTime { get; set; }
    public DateTime? ResignationDate { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string IdCardNumber { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string BusinessCard { get; set; } = string.Empty;
    public DateTime? HireDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? EmailEx { get; set; }
    public bool Locked { get; set; }
    public bool Active { get; set; }
    public List<EmpCreateOfDeptItemRequestModel> Depts { get; set; } = [];
}
