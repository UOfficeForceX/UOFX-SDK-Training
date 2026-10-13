namespace SDK_FirstSample.Models;

public class HrDeptModel
{
    public string DeptName { get; set; } = string.Empty;
    public string DeptCode { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsActive { get; set; }
    public bool HasSubDepartments { get; set; }
    public string ParentDeptCode { get; set; } = string.Empty;
    public string DeptDescription { get; set; } = string.Empty;
    public List<HrDeptModel> SubDepts { get; set; } = [];
}

public class HrJobTitleModel
{
    public string JobTitleCode { get; set; } = string.Empty;
    public string JobTitleName { get; set; } = string.Empty;
}

public class HrJobFunctionModel
{
    public string JobFunctionCode { get; set; } = string.Empty;
    public string JobFunctionName { get; set; } = string.Empty;
}

public class HrEmplModel
{
    public string Account { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string EmployeeID { get; set; } = string.Empty;
    public DateTime? ExpiredTime { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string IDNumber { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string CardTitle { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public DateTime? ResignationDate { get; set; }
    public string PrimaryEmail { get; set; } = string.Empty;
    public string? SecondaryEmail { get; set; }
    public bool AccountLocked { get; set; }
    public bool AccountActive { get; set; }
    public string JobTitleID { get; set; } = string.Empty;
    public List<string>? JobFunctionIDs { get; set; }
    public string DeptID { get; set; } = string.Empty;
    public bool PrimaryDepartment { get; set; }
    public bool IsSupervisor { get; set; }
}
