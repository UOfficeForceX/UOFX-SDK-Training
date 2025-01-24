using Ede.Uofx.PubApi.Sdk.NetStd;
using Ede.Uofx.PubApi.Sdk.NetStd.Models.Base;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;

//設定金鑰
UofxService.Key = "xxx";

//設定 UOF X 站台網址
UofxService.UofxServerUrl = "https://myuofx.com.tw";

// 公司代號
string _corpCode = "corpCode";

// 儲存成功訊息
List<string> successMsg = new List<string> { "====================================================同步成功====================================================" };
// 儲存錯誤訊息
List<string> errorMsg = new List<string> { "====================================================同步失敗====================================================" };

// 列印錯誤成功訊息
void ConsoleList(List<string> list)
{
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
};

// HR 系統部門資料
List<HrDeptModel> originHrDepts = new List<HrDeptModel>
{
    new HrDeptModel
    {
        DeptName = "Research and Development",
        DeptCode = "RD",
        Order = 2,
        IsActive = true,
        HasSubDepartments = false,
        ParentDeptCode = "HR",
        DeptDescription = "Contamination NEC"
    },
    new HrDeptModel
    {
        DeptName = "Business Development",
        DeptCode = "BD",
        Order = 1,
        IsActive = true,
        HasSubDepartments = false,
        ParentDeptCode = "HR",
        DeptDescription = "Adenovirus infect NOS"
    },
    new HrDeptModel
    {
        DeptName = "Training",
        DeptCode = "T",
        Order = 3,
        IsActive = true,
        HasSubDepartments = false,
        ParentDeptCode = "HR",
        DeptDescription = "TB peritonitis-histo dx"
    },
    new HrDeptModel
    {
        DeptName = "Human Resources",
        DeptCode = "HR",
        Order = 1,
        IsActive = true,
        HasSubDepartments = true,
        ParentDeptCode = "S",
        DeptDescription = "Spon abor w pel inf-unsp"
    },
    new HrDeptModel
    {
        DeptName = "Services",
        DeptCode = "S",
        Order = 1,
        IsActive = true,
        HasSubDepartments = true,
        ParentDeptCode = "",
        DeptDescription = "Oth coll stn obj-per NEC"
    }
};
// HR 系統人員資料
List<HrEmplModel> originHrEmpls = new List<HrEmplModel>
{
    new HrEmplModel{
        Account = "costanza",
        Name = "松源",
        EnglishName = "costanza Posvner",
        EmployeeID = "A3897",
        ExpiredTime = null,
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(85, 7, 25),
        PhoneNumber = "0946323278",
        CardTitle = "Human Resources Assistant II",
        HireDate = new DateTime(2013, 12, 21),
        ResignationDate = new DateTime(2027, 10, 18),
        PrimaryEmail = "cposvner0@jalbum.net",
        SecondaryEmail = "cposvner0@phoca.cz",
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "UX",
        JobFunctions = null,
        DeptCode = "T",
        PrimaryDepartment = true,
        IsSupervisor = true
    },
    new HrEmplModel{
        Account = "natty",
        Name = "丰逸",
        EnglishName = "natty Yankin",
        EmployeeID = "A9618",
        ExpiredTime = null,
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(85, 1, 14),
        PhoneNumber = "0934756998",
        CardTitle = "Pharmacist",
        HireDate = new DateTime(2020, 3, 2),
        ResignationDate = null,
        PrimaryEmail = "nyankin1@wiley.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "PM",
        JobFunctions = new List<string> { "資訊安全管理" },
        DeptCode = "HR",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel{
        Account = "julina",
        Name = "思宇",
        EnglishName = "julina Andryushchenko",
        EmployeeID = "A8329",
        ExpiredTime = new DateTime(2023, 2, 10),
        Gender = "other",
        IDNumber = "A123456789",
        BirthDate = new DateTime(87, 4, 10),
        PhoneNumber = "0960836336",
        CardTitle = "Budget/Accounting Analyst III",
        HireDate = new DateTime(2024, 2, 7),
        ResignationDate = null,
        PrimaryEmail = "jandryushchenko2@amazon.co.uk",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "TA",
        JobFunctions = null,
        DeptCode = "RD",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel{
        Account = "catharina",
        Name = "琪煜",
        EnglishName = "catharina Edmonson",
        EmployeeID = "A6879",
        ExpiredTime = null,
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(90, 7, 18),
        PhoneNumber = "0995946581",
        CardTitle = "Actuary",
        HireDate = new DateTime(2012, 4, 15),
        ResignationDate = null,
        PrimaryEmail = "cedmonson3@free.fr",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "TA",
        JobFunctions = null,
        DeptCode = "BD",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel{
        Account = "nita",
        Name = "梓焓",
        EnglishName = "nita Lowre",
        EmployeeID = "A4523",
        ExpiredTime = new DateTime(2025, 9, 2),
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(63, 7, 18),
        PhoneNumber = "0995537127",
        CardTitle = "Compensation Analyst",
        HireDate = new DateTime(2011, 5, 17),
        ResignationDate = null,
        PrimaryEmail = "nlowre4@privacy.gov.au",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "SA",
        JobFunctions = new List<string> { "使用者介面設計" },
        DeptCode = "T",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel{
        Account = "cary",
        Name = "可馨",
        EnglishName = "cary Ralton",
        EmployeeID = "A4550",
        ExpiredTime = null,
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(62, 1, 26),
        PhoneNumber = "0969873340",
        CardTitle = "Dental Hygienist",
        HireDate = new DateTime(2008, 11, 24),
        ResignationDate = null,
        PrimaryEmail = "cralton5@admin.ch",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "QA",
        JobFunctions = null,
        DeptCode = "BD",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel{
        Account = "gelya",
        Name = "培安",
        EnglishName = "gelya Louder",
        EmployeeID = "A8117",
        ExpiredTime = null,
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(74, 3, 20),
        PhoneNumber = "0971386307",
        CardTitle = "Compensation Analyst",
        HireDate = new DateTime(2013, 7, 16),
        ResignationDate = null,
        PrimaryEmail = "glouder6@homestead.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "PM",
        JobFunctions = null,
        DeptCode = "HR",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "nita",
        Name = "梓焓",
        EnglishName = "nita Lowre",
        EmployeeID = "A4523",
        ExpiredTime = new DateTime(2025, 9, 2),
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(63, 7, 18),
        PhoneNumber = "0995537127",
        CardTitle = "Compensation Analyst",
        HireDate = new DateTime(2011, 5, 17),
        ResignationDate = null,
        PrimaryEmail = "nlowre4@privacy.gov.au",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "TA",
        JobFunctions = null,
        DeptCode = "HR",
        PrimaryDepartment = false,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "costanza",
        Name = "松源",
        EnglishName = "costanza Posvner",
        EmployeeID = "A3897",
        ExpiredTime = null,
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(85, 7, 25),
        PhoneNumber = "0946323278",
        CardTitle = "Human Resources Assistant II",
        HireDate = new DateTime(2013, 12, 21),
        ResignationDate = new DateTime(2027, 10, 18),
        PrimaryEmail = "cposvner0@jalbum.net",
        SecondaryEmail = "cposvner0@phoca.cz",
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "UX",
        JobFunctions = new List<string> { "資訊安全管理" },
        DeptCode = "RD",
        PrimaryDepartment = false,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "carmelina",
        Name = "漫妮",
        EnglishName = "carmelina Beadle",
        EmployeeID = "A3583",
        ExpiredTime = null,
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(85, 10, 8),
        PhoneNumber = "0911960804",
        CardTitle = "Accounting Assistant I",
        HireDate = new DateTime(2011, 3, 26),
        ResignationDate = new DateTime(2027, 9, 25),
        PrimaryEmail = "cbeadle9@go.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "TA",
        JobFunctions = new List<string> { "軟體開發" },
        DeptCode = "HR",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "raviv",
        Name = "澤瀚",
        EnglishName = "raviv Atcherley",
        EmployeeID = "A8007",
        ExpiredTime = null,
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(98, 4, 20),
        PhoneNumber = "0976843491",
        CardTitle = "Tax Accountant",
        HireDate = new DateTime(2018, 4, 21),
        ResignationDate = null,
        PrimaryEmail = "ratcherleya@yellowpages.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "TA",
        JobFunctions = null,
        DeptCode = "HR",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "chev",
        Name = "雅靜",
        EnglishName = "chev Rowell",
        EmployeeID = "A287",
        ExpiredTime = null,
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(88, 5, 2),
        PhoneNumber = "0913313487",
        CardTitle = "Automation Specialist I",
        HireDate = new DateTime(2011, 5, 4),
        ResignationDate = null,
        PrimaryEmail = "crowellb@paginegialle.it",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "BA",
        JobFunctions = null,
        DeptCode = "S",
        PrimaryDepartment = true,
        IsSupervisor = true
    },
    new HrEmplModel
    {
        Account = "ezmeralda",
        Name = "若瑾",
        EnglishName = "ezmeralda Durkin",
        EmployeeID = "A6996",
        ExpiredTime = new DateTime(2029, 5, 18),
        Gender = "other",
        IDNumber = "A123456789",
        BirthDate = new DateTime(56, 6, 8),
        PhoneNumber = "0918733181",
        CardTitle = "Office Assistant I",
        HireDate = new DateTime(2024, 10, 28),
        ResignationDate = new DateTime(2027, 1, 9),
        PrimaryEmail = "edurkinc@tripadvisor.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "UX",
        JobFunctions = null,
        DeptCode = "RD",
        PrimaryDepartment = true,
        IsSupervisor = true
    },
    new HrEmplModel
    {
        Account = "rina",
        Name = "俊澤",
        EnglishName = "rina Cootes",
        EmployeeID = "A9043",
        ExpiredTime = new DateTime(2029, 10, 8),
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(62, 6, 6),
        PhoneNumber = "0971141652",
        CardTitle = "Senior Quality Engineer",
        HireDate = new DateTime(2017, 3, 9),
        ResignationDate = null,
        PrimaryEmail = "rcootesd@unblog.fr",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "UX",
        JobFunctions = new List<string> { "軟體開發" },
        DeptCode = "HR",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "dory",
        Name = "雨婷",
        EnglishName = "dory Ashard",
        EmployeeID = "A6241",
        ExpiredTime = null,
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(61, 4, 4),
        PhoneNumber = "0944897131",
        CardTitle = "Business Systems Development Analyst",
        HireDate = new DateTime(2013, 2, 25),
        ResignationDate = null,
        PrimaryEmail = "dasharde@latimes.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "BE",
        JobFunctions = null,
        DeptCode = "HR",
        PrimaryDepartment = true,
        IsSupervisor = true
    },
    new HrEmplModel
    {
        Account = "conway",
        Name = "浩霖",
        EnglishName = "conway McAlpine",
        EmployeeID = "A2169",
        ExpiredTime = null,
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(100, 6, 25),
        PhoneNumber = "0975728820",
        CardTitle = "Physical Therapy Assistant",
        HireDate = new DateTime(2016, 4, 8),
        ResignationDate = new DateTime(2029, 12, 20),
        PrimaryEmail = "cmcalpinef@ted.com",
        SecondaryEmail = "cmcalpinef@scientificamerican.com",
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "PM",
        JobFunctions = null,
        DeptCode = "BD",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "rebekkah",
        Name = "博豪",
        EnglishName = "rebekkah Trotter",
        EmployeeID = "A1287",
        ExpiredTime = null,
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(85, 4, 1),
        PhoneNumber = "0981131325",
        CardTitle = "Assistant Professor",
        HireDate = new DateTime(2020, 5, 8),
        ResignationDate = new DateTime(2026, 6, 4),
        PrimaryEmail = "rtrotterg@abc.net.au",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "IS",
        JobFunctions = null,
        DeptCode = "T",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "kath",
        Name = "博裕",
        EnglishName = "kath Duplain",
        EmployeeID = "A1665",
        ExpiredTime = null,
        Gender = "male",
        IDNumber = "A123456789",
        BirthDate = new DateTime(81, 11, 11),
        PhoneNumber = "0934443938",
        CardTitle = "Payment Adjustment Coordinator",
        HireDate = new DateTime(2011, 12, 21),
        ResignationDate = new DateTime(2026, 4, 5),
        PrimaryEmail = "kduplainh@cnet.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "FE",
        JobFunctions = null,
        DeptCode = "BD",
        PrimaryDepartment = true,
        IsSupervisor = true
    },
    new HrEmplModel
    {
        Account = "betteanne",
        Name = "芮涵",
        EnglishName = "betteanne Facchini",
        EmployeeID = "A3542",
        ExpiredTime = null,
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(62, 6, 8),
        PhoneNumber = "0960426450",
        CardTitle = "Account Coordinator",
        HireDate = new DateTime(2009, 9, 25),
        ResignationDate = new DateTime(2029, 11, 13),
        PrimaryEmail = "bfacchinii@cnbc.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "SA",
        JobFunctions = null,
        DeptCode = "RD",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "betteanne",
        Name = "芮涵",
        EnglishName = "betteanne Facchini",
        EmployeeID = "A3542",
        ExpiredTime = null,
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(62, 6, 8),
        PhoneNumber = "0960426450",
        CardTitle = "Account Coordinator",
        HireDate = new DateTime(2009, 9, 25),
        ResignationDate = new DateTime(2029, 11, 13),
        PrimaryEmail = "bfacchinii@cnbc.com",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "TA",
        JobFunctions = new List<string> { "技術支援" },
        DeptCode = "T",
        PrimaryDepartment = false,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "antons",
        Name = "辰華",
        EnglishName = "antons Walcher",
        EmployeeID = "A8962",
        ExpiredTime = new DateTime(2029, 5, 14),
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(79, 7, 26),
        PhoneNumber = "0931240619",
        CardTitle = "Environmental Tech",
        HireDate = new DateTime(2013, 5, 8),
        ResignationDate = null,
        PrimaryEmail = "awalcherj@google.fr",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "BE",
        JobFunctions = new List<string> { "後端開發", "技術支援" },
        DeptCode = "T",
        PrimaryDepartment = true,
        IsSupervisor = false
    },
    new HrEmplModel
    {
        Account = "antons",
        Name = "辰華",
        EnglishName = "antons Walcher",
        EmployeeID = "A8962",
        ExpiredTime = new DateTime(2029, 5, 14),
        Gender = "female",
        IDNumber = "A123456789",
        BirthDate = new DateTime(79, 7, 26),
        PhoneNumber = "0931240619",
        CardTitle = "Environmental Tech",
        HireDate = new DateTime(2013, 5, 8),
        ResignationDate = null,
        PrimaryEmail = "awalcherj@google.fr",
        SecondaryEmail = null,
        AccountLocked = false,
        AccountActive = true,
        JobTitleID = "PM",
        JobFunctions = new List<string> { "專案管理" },
        DeptCode = "S",
        PrimaryDepartment = false,
        IsSupervisor = false
    }
};

#region 同步部門
// 同步部門資料
async Task SyncDepts(List<HrDeptModel> originHrDepts)
{
    // 部門資料階層化
    var hierarchyDeptList = BuildHierarchy(originHrDepts);
    // 建立部門層級與轉換後的部門清單
    var (deptLevList, deptList) = BuildDeptLevs(hierarchyDeptList, 1);
    // 比對與同步部門階層資料
    await MapDeptLevels(deptLevList);
    // 比對與同步部門資料
    await MapDepts(deptList);
    return;
};
// 部門資料階層化
List<HrDeptModel> BuildHierarchy(List<HrDeptModel> originHrDepts)
{
    // 取得有子部門的父部門資料
    var getHRParentDeptList = originHrDepts.Where(x => x.HasSubDepartments).ToList();
    // 將子部門資料加入父部門
    foreach (var dept in getHRParentDeptList)
    {
        var getSubDept = originHrDepts.Where(x => x.ParentDeptCode == dept.DeptCode).ToList();
        dept.SubDepts = getSubDept;
    };
    // 取得沒有父部門的部門資料
    var hierarchyDeptList = originHrDepts.Where(x => string.IsNullOrEmpty(x.ParentDeptCode)).ToList();
    return hierarchyDeptList;
};
// 建立部門層級與轉換後的部門清單
(List<DeptLevelViewModel> deptLevList, List<DeptModel> deptList) BuildDeptLevs(List<HrDeptModel> hierarchy, int level)
{
    // 部門層級清單
    var deptLevList = new List<DeptLevelViewModel>();
    // 部門清單
    var deptList = new List<DeptModel>();

    foreach (var dept in hierarchy)
    {
        // 確認不新增重複部門層級
        if (!deptLevList.Any(x => x.Seq == level))
        {
            // 新增當前層級的資料
            deptLevList.Add(new DeptLevelViewModel
            {
                Code = $"lev{level}", // 部門層級代號
                Name = $"Level{level}", // 部門層級名稱
                Seq = level, // 部門層級的階層與排序 ( 從 1 開始 )
                CorpCode = _corpCode, // 公司代號
                Active = true // 是否啟用部門層級
            });
        }
        // 轉換部門資料並加入部門層級
        deptList.Add(new DeptModel
        {
            Code = dept.DeptCode, // 部門代號
            Name = dept.DeptName, // 部門名稱
            Active = true, // 是否啟用部門
            IncludeSubDept = dept.HasSubDepartments, // 是否包含子部門
            ParentCode = dept.ParentDeptCode, // 父部門代號 ( 可設定為空值，代表無父部門 )
            Description = dept.DeptDescription, // 描述
            DeptLevelCode = $"lev{level}", // 部門層級代號
            Seq = dept.Order - 1 // 部門的排序 ( 從 0 開始 )
        });
        // 遞迴處理子部門，層級加 1，並將結果加入主清單
        if (dept.SubDepts != null && dept.SubDepts.Count > 0)
        {
            var (deptLevListTemp, deptListTemp) = BuildDeptLevs(dept.SubDepts, level + 1);
            deptLevList.AddRange(deptLevListTemp);
            deptList.AddRange(deptListTemp);
        }
    };
    return (deptLevList, deptList);
};
// 比對與同步部門階層資料
async Task MapDeptLevels(List<DeptLevelViewModel> hrDeptLevs)
{
    // 更新清單
    var deptLevelToUpdate = new List<DeptLevelViewModel>();
    // 新增清單
    var deptLevelToAdd = new List<DeptLevelViewModel>();
    // 停用清單
    var deptLevelToDeactivate = new List<DeptLevelViewModel>();

    // 取得部門層級資料
    var uofDeptLevs = await UofxService.BASE.DeptLevel.Get(_corpCode);

    // 建立以 Code 為鍵的字典以加快查找速度
    var hrDeptDict = hrDeptLevs.ToDictionary(x => x.Code, x => x);
    var uofDeptDict = uofDeptLevs.ToDictionary(x => x.Code, x => x);

    // 處裡要更新和新增的項目
    foreach (var hrDept in hrDeptLevs)
    {
        // 若此項目已存在，加入更新清單
        if (uofDeptDict.ContainsKey(hrDept.Code))
        {
            deptLevelToUpdate.Add(hrDept);
        }
        // 此項目不存在，加入新增清單
        else
        {
            deptLevelToAdd.Add(hrDept);
        }
    };
    // 處理要停用的項目
    foreach (var uofDept in uofDeptLevs)
    {
        // 若此項目不存在於 HR 系統，加入停用清單
        if (!hrDeptDict.ContainsKey(uofDept.Code))
        {
            deptLevelToDeactivate.Add(uofDept);
        };
    };

    // 更新部門層級
    await UpdateDeptLevels(deptLevelToUpdate);
    // 新增部門層級
    await CreateDeptLevels(deptLevelToAdd);
    // 停用部門層級
    await DeactivateDeptLevels(deptLevelToDeactivate);
    return;
};
// 更新部門層級
async Task UpdateDeptLevels(List<DeptLevelViewModel> deptLevels)
{
    foreach (var deptLevel in deptLevels)
    {
        try
        {
            // 更新部門層級狀態
            var updateStatus = await UofxService.BASE.DeptLevel.UpdateStatus(new DeptLevelUpdateStatusModel()
            {
                CorpCode = _corpCode, // 公司代號
                Code = deptLevel.Code, // 部門層級代號
                Active = true // 是否啟用部門層級
            });
            // 更新部門層級資料
            var update = await UofxService.BASE.DeptLevel.Update(new DeptLevelUpdateModel()
            {
                CorpCode = _corpCode, // 公司代號
                OriginalCode = deptLevel.Code, // 原始部門層級代號 ( 設為部門層級代號 )
                Code = deptLevel.Code, // 部門層級代號
                Name = deptLevel.Name // 部門層級名稱
            });
            // 若部門層級狀態、資料更新成功，加入成功訊息
            if (updateStatus == true && update == true) successMsg.Add($"已更新 {deptLevel.Code} 部門層級");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法更新 {deptLevel.Code} 部門層級，error: {ex.Message}");
        }
    };
};
// 新增部門層級
async Task CreateDeptLevels(List<DeptLevelViewModel> deptLevels)
{
    foreach (var deptLevel in deptLevels)
    {
        try
        {
            // 新增部門層級
            var resualt = await UofxService.BASE.DeptLevel.Create(deptLevel);
            // 若新增成功，加入成功訊息
            if (resualt == true) successMsg.Add($"已新增 {deptLevel.Code} 部門層級");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法新增 {deptLevel.Code} 部門層級，error: {ex.Message}");
        }
    };
};
// 停用部門層級
async Task DeactivateDeptLevels(List<DeptLevelViewModel> deptLevels)
{
    foreach (var deptLevel in deptLevels)
    {
        try
        {
            // 停用部門層級
            var resualt = await UofxService.BASE.DeptLevel.UpdateStatus(new DeptLevelUpdateStatusModel()
            {
                CorpCode = _corpCode, // 公司代號
                Code = deptLevel.Code, // 部門層級代號
                Active = false // 停用部門層級
            });
            // 若停用成功，加入成功訊息
            if (resualt == true) successMsg.Add($"已停用 {deptLevel.Code} 部門層級");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法停用 {deptLevel.Code} 部門層級，error: {ex.Message}");
        }
    };
};
// 比對與同步部門資料
async Task MapDepts(List<DeptModel> hrDepts)
{
    // 更新清單
    var deptToUpdate = new List<DeptModel>();
    // 新增清單
    var deptToAdd = new List<DeptModel>();
    // 停用清單
    var deptToDeactivate = new List<DeptModel>();

    // 取得部門資料
    var uofDepts = await UofxService.BASE.Department.Get(new DepartmentGetModel() { CorpCode = _corpCode, IncludeSubDept = true });

    // 建立以 Code 為鍵的字典以加快查找速度
    var hrDeptDict = hrDepts.ToDictionary(x => x.Code, x => x);
    var uofDeptDict = uofDepts.ToDictionary(x => x.Code, x => x);

    // 處理要更新和新增的項目
    foreach (var hrDept in hrDepts)
    {
        // 若此項目已存在，加入更新清單
        if (uofDeptDict.ContainsKey(hrDept.Code))
        {
            deptToUpdate.Add(new DeptModel
            {
                ParentCode = hrDept.ParentCode, // 父部門代號 ( 可設定為空值，代表無父部門 )
                Code = hrDept.Code, // 部門代號
                Name = hrDept.Name, // 部門名稱
                DeptLevelCode = hrDept.DeptLevelCode, // 部門層級代號
                Description = hrDept.Description, // 描述
                Active = hrDept.Active, // 是否啟用部門
                Seq = hrDept.Seq // 部門的排序 ( 從 0 開始 )
            });
        }
        // 此項目不存在，加入新增清單
        else
        {
            deptToAdd.Add(new DeptModel
            {
                ParentCode = hrDept.ParentCode, // 父部門代號 ( 可設定為空值，代表無父部門 )
                Code = hrDept.Code, // 部門代號
                Name = hrDept.Name, // 部門名稱
                DeptLevelCode = hrDept.DeptLevelCode, // 部門層級代號
                Description = hrDept.Description, // 描述
                Active = hrDept.Active, // 是否啟用部門
                Seq = hrDept.Seq // 部門的排序 ( 從 0 開始 )
            });
        }
    };
    // 處理要停用的項目
    foreach (var uofDept in uofDepts)
    {
        // 若此項目不存在於 HR 系統，加入停用清單
        if (!hrDeptDict.ContainsKey(uofDept.Code))
        {
            deptToDeactivate.Add(new DeptModel
            {
                Code = uofDept.Code, // 部門代號
                Active = uofDept.Active, // 是否啟用部門
                DeptLevelCode = uofDept.DeptLevelCode // 部門層級代號
            });
        }
    };

    // 更新部門
    await UpdateDepartments(deptToUpdate);
    // 新增部門
    await CreateDepartments(deptToAdd);
    // 停用部門
    await DeactivateDepartments(deptToDeactivate);
    return;
};
// 更新部門
async Task UpdateDepartments(List<DeptModel> departments)
{
    foreach (var dept in departments)
    {
        try
        {
            // 更新部門狀態
            var updateStatus = await UofxService.BASE.Department.UpdateState(new DepartmentUpdateStatusModel()
            {
                CorpCode = _corpCode, // 公司代號
                Code = dept.Code, // 部門代號
                Active = dept.Active // 是否啟用部門
            });
            // 更新部門資料
            var update = await UofxService.BASE.Department.Update(new DepartmentUpdateModel()
            {
                CorpCode = _corpCode, // 公司代號
                OriginalCode = dept.Code, // 原始部門代號 ( 設為部門代號 )
                Code = dept.Code, // 部門代號
                Name = dept.Name, // 部門名稱
                DeptLevelCode = dept.DeptLevelCode, // 部門層級代號
                Description = dept.Description // 描述
            });
            // 若部門狀態、資料更新成功，加入成功訊息
            if (update == true) successMsg.Add($"已更新 {dept.Code} 部門");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法更新 {dept.Code} 部門，error: {ex.Message}");
        }
    };
};
// 新增部門
async Task CreateDepartments(List<DeptModel> departments)
{
    try
    {
        foreach (var dept in departments)
        {
            // 新增部門
            var result = await UofxService.BASE.Department.Create(new DepartmentCreateModel()
            {
                ParentCode = dept.ParentCode, // 父部門代號 ( 可設定為空值，代表無父部門 )
                CorpCode = _corpCode, // 公司代號
                Code = dept.Code, // 部門代號
                DeptLevelCode = dept.DeptLevelCode, // 部門層級代號
                Name = dept.Name, // 部門名稱
                Description = dept.Description // 描述
            });
            // 若新增成功，加入成功訊息
            if (result == true) successMsg.Add($"已新增 {dept.Code} 部門");
        };
    }
    catch (Exception ex)
    {
        // 捕捉錯誤並記錄到 errorMsg 中  
        errorMsg.Add($"無法新增 {departments} 部門，error: {ex.Message}");
    }
};
// 停用部門
async Task DeactivateDepartments(List<DeptModel> departments)
{
    foreach (var dept in departments)
    {
        try
        {
            // 停用部門
            var result = await UofxService.BASE.Department.UpdateState(new DepartmentUpdateStatusModel()
            {
                CorpCode = _corpCode, // 公司代號
                Code = dept.Code, // 部門代號
                Active = false // 是否啟用部門
            });
            // 若停用成功，加入成功訊息
            if (result == true) successMsg.Add($"已停用 {dept.Code} 部門");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法停用 {dept.Code} 部門，error: {ex.Message}");
        }
    };
};
#endregion

#region 同步人員
// 同步人員資料
async Task SyncEmpls(List<HrEmplModel> originHrEmpls)
{
    // 同步職稱職務
    await SyncJobTitlesAndFunctions(originHrEmpls);
    // 轉換人員清單
    var hrEmpls = ConvertToEmplModels(originHrEmpls);
    // 比對與同步人員資料
    await MapEmpls(hrEmpls);
    // 同步部門主管
    await SyncSuperiors(originHrEmpls);
};
// 同步職稱職務
async Task SyncJobTitlesAndFunctions(List<HrEmplModel> originHrEmpls)
{
    // 取得 HR 系統職稱職務
    List<string> JobTitleCodes = originHrEmpls.Select(e => e.JobTitleID).ToList();
    List<string> JobFuncs = originHrEmpls.SelectMany(e => e.JobFunctions ?? new List<string>()).ToList();

    // 取得 UOF X 系統職稱職務
    var uofJobTitles = await UofxService.BASE.JobTitle.Get(_corpCode);
    var uofJobFuncs = await UofxService.BASE.JobFunc.Get(_corpCode);

    // 找出要新增的職稱職務
    var JobTitleToAdd = JobTitleCodes.Except(uofJobTitles.Select(j => j.Code)).ToList();
    var JobFuncToAdd = JobFuncs.Except(uofJobFuncs.Select(j => j.Code)).ToList();

    foreach (var jobTitle in JobTitleToAdd)
    {
        try
        {
            // 新增職稱
            var result = await UofxService.BASE.JobTitle.Create(new JobTitleViewModel()
            {
                CorpCode = _corpCode, // 職稱代號
                Rank = 50, // 職稱階層 ( 將簽核層級預設為 50 再由管理員到 UOF X 調整 )
                Seq = 1, // 職稱排序 ( 從 1 開始 )
                Code = jobTitle, // 職稱代號
                Title = jobTitle, // 職稱名稱
                Active = true // 啟用職稱
            });
            // 若新增成功，加入成功訊息
            if (result == true) successMsg.Add($"已新增 {jobTitle} 職稱");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法新增 {jobTitle} 職稱，error: {ex.Message}");
        }
    };
    foreach (var jobFunc in JobFuncToAdd)
    {
        try
        {
            // 新增職務
            var result = await UofxService.BASE.JobFunc.Create(new JobFuncViewModel()
            {
                CorpCode = _corpCode, // 職稱代號
                CategoryName = jobFunc, // 職務類別名稱
                Code = jobFunc, // 職務代號
                JobFunc = jobFunc, // 職務名稱
                Active = true // 啟用職務
            });
            // 若新增成功，加入成功訊息
            if (result == true) successMsg.Add($"已新增 {jobFunc} 職務");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法新增 {jobFunc} 職務，error: {ex.Message}");
        }
    };
    return;
};
// 轉換人員清單
List<EmplModel> ConvertToEmplModels(List<HrEmplModel> originHrEmpls)
{
    // 以 Account 作為 key 分組
    var groupedEmpls = originHrEmpls.GroupBy(e => e.Account);
    // 每一組的分組資料 g 轉換成一個新的 EmplModel
    return groupedEmpls.Select(g => new EmplModel
    {
        CorpCode = _corpCode, // 公司代號
        Account = g.Key, // 人員帳號
        Name = g.First().Name, // 中文姓名
        Gender = g.First().Gender == "male" ? "0" : (g.First().Gender == "female" ? "1" : "2"), // 性別 ( 男性 male => 0, 女性 female => 1, 其他 other => 2 )
        EnglishName = g.First().EnglishName, // 英文姓名
        EmployeeNumber = g.First().EmployeeID, // 員工編號
        ExpiredTime = g.First().ExpiredTime, // 帳號過期時間
        ResignationDate = g.First().ResignationDate, // 離職日
        IdCardNumber = g.First().IDNumber, // 身份證字號
        BirthDate = new DateTime(g.First().BirthDate.Year + 1911, g.First().BirthDate.Month, g.First().BirthDate.Day), // 生日 (民國年轉為西元年)
        PhoneNumber = g.First().PhoneNumber, // 行動電話
        BusinessCard = g.First().CardTitle, // 名片職稱
        HireDate = g.First().HireDate, // 到職日
        Email = g.First().PrimaryEmail, // 主要信箱
        EmailEx = g.First().SecondaryEmail, // 其他信箱
        Active = g.First().AccountActive, // 是否停用人員帳號
        Locked = g.First().AccountLocked, // 是否鎖定人員帳號
        // 將每個分組中的資料轉換成一個新的 EmpCreateOfDeptItemRequestModel
        Depts = g.Select(e => new EmpCreateOfDeptItemRequestModel
        {
            Code = e.DeptCode, // 部門代號
            IsMainDept = e.PrimaryDepartment, // 是否為主要部門
            JobTitleCode = e.JobTitleID, // 職稱代號
            JobFuncs = e.JobFunctions // 職務代號 ( 可多筆 )

        }).ToList()
    }).ToList();
};
// 比對與同步人員資料
async Task MapEmpls(List<EmplModel> hrEmpls)
{
    // 更新清單
    var emplToUpdate = new List<EmplModel>();
    // 新增清單
    var emplToAdd = new List<EmplModel>();
    // 停用清單
    var emplToDeactivate = new List<EmplModel>();

    // 將已存在人員放入需更新清單，不存在人員放入需新增清單
    foreach (var hrEmpl in hrEmpls)
    {
        // 取得人員資料
        var empl = await UofxService.BASE.OrgEmpl.Get(new EmplQueryRequestModel()
        {
            CorpCode = _corpCode, // 公司代號
            UserType = UserType.Account, // 人員的類別 ( 設為 Account )
            UserCode = hrEmpl.Account // 帳號
        });
        // 若人員存在，加入更新清單
        if (empl.Account != null)
        {
            emplToUpdate.Add(hrEmpl);
        }
        // 不存在，加入新增清單
        else
        {
            emplToAdd.Add(hrEmpl);
            // 如果現在日期大於帳號過期時間，加入停用清單
            if (hrEmpl.ExpiredTime != null && DateTime.Now > hrEmpl.ExpiredTime)
            {
                emplToDeactivate.Add(hrEmpl);
            };
        }
    };
    // 更新人員
    await UpdateEmpls(emplToUpdate);
    // 新增人員
    await CreateEmpls(emplToAdd);
    // 停用人員
    await DeactivateEmpls(emplToDeactivate);
};
// 更新人員
async Task UpdateEmpls(List<EmplModel> empls)
{
    foreach (var empl in empls)
    {
        try
        {
            // 更新人員狀態
            var updateStatus = await UofxService.BASE.OrgEmpl.UpdateAcctStatus(new EmplUpdateAcctStatusRequestModel
            {
                CorpCode = _corpCode, // 公司代號
                UserType = UserType.Account, // 人員的類別 ( 設為 Account )
                UserCode = empl.Account, // 帳號
                Active = empl.Active // 是否停用人員帳號
            });
            // 更新人員鎖定狀態
            var updateLocked = await UofxService.BASE.OrgEmpl.UpdateAcctLocked(new EmplUpdateAcctLockedRequestModel
            {
                CorpCode = _corpCode, // 公司代號
                UserType = UserType.Account, // 人員的類別 ( 設為 Account )
                UserCode = empl.Account, // 帳號
                Locked = empl.Locked // 是否鎖定人員帳號
            });
            // 更新人員資料
            var update = await UofxService.BASE.OrgEmpl.Update(new EmplUpdateRequestModel
            {
                CorpCode = _corpCode, // 公司代號
                UserType = UserType.Account, // 人員的類別 ( 設為 Account )
                UserCode = empl.Account, // 帳號
                Account = empl.Account, // 人員帳號
                Name = empl.Name, // 中文姓名
                EmployeeNumber = empl.EmployeeNumber, // 員工編號
                Gender = empl.Gender, // 性別
                EnglishName = empl.EnglishName, // 英文姓名
                IdCardNumber = empl.IdCardNumber, // 身份證字號
                BirthDate = empl.BirthDate, // 生日
                PhoneNumber = empl.PhoneNumber, // 行動電話
                BusinessCard = empl.BusinessCard, // 名片職稱
                HireDate = empl.HireDate, // 到職日
                Email = empl.Email, // 主要信箱
                EmailEx = empl.EmailEx // 	其他信箱
            });
            // 更新人員過期時間
            var updateExpiredTime = await UofxService.BASE.OrgEmpl.UpdateAcctExpiredTime(new EmplUpdateAcctExpiredTimeRequestModel
            {
                CorpCode = _corpCode, // 公司代號
                UserType = UserType.Account, // 人員的類別 ( 設為 Account )
                UserCode = empl.Account, // 帳號
                ExpiredTime = empl.ExpiredTime // 帳號過期時間
            });
            // 更新人員離職日期
            var updateResignationDate = await UofxService.BASE.OrgEmpl.UpdateEmplResignationDate(new EmplUpdateResignationDateRequestModel
            {
                CorpCode = _corpCode, // 公司代號
                UserType = UserType.Account, // 人員的類別 ( 設為 Account )
                UserCode = empl.Account, // 帳號
                ResignationDate = empl.ResignationDate // 離職日
            });
            // 更新人員部門
            var updateDepts = await UofxService.BASE.OrgEmpl.UpdateEmplDept(new EmplUpdateDeptRequestModel
            {
                CorpCode = _corpCode, // 公司代號
                UserType = UserType.Account, // 人員的類別 ( 設為 Account )
                UserCode = empl.Account, // 帳號
                Depts = empl.Depts.Select(d => new DeptRequestModel
                {
                    Code = d.Code, // 部門代號
                    IsMainDept = d.IsMainDept, // 是否為主要部門
                    JobTitleCode = d.JobTitleCode, // 職稱代號
                    JobFuncs = d.JobFuncs // 職務代號 ( 可多筆 )
                }).ToList()
            });
            // 若所有更新皆成功，記錄成功訊息
            if (updateStatus == true && updateLocked == true && update == true && updateExpiredTime == true && updateResignationDate == true && updateDepts == true)
            {
                successMsg.Add($"已更新 {empl.Account} 人員");
            };
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法更新 {empl.Account} 人員，error: {ex.Message}");
        }
    };
};
// 新增人員
async Task CreateEmpls(List<EmplModel> empls)
{
    foreach (var empl in empls)
    {
        try
        {
            // 新增人員
            var result = await UofxService.BASE.OrgEmpl.CreateEmpl(new EmpCreateRequestModel
            {
                CorpCode = _corpCode, // 公司代號
                Account = empl.Account, // 人員帳號
                Name = empl.Name, // 中文姓名
                EnglishName = empl.EnglishName, // 英文姓名
                EmployeeNumber = empl.EmployeeNumber, // 員工編號
                ExpiredTime = empl.ExpiredTime, // 帳號過期時間
                Gender = empl.Gender, // 性別
                IdCardNumber = empl.IdCardNumber, // 身份證字號
                BirthDate = empl.BirthDate, // 	生日
                PhoneNumber = empl.PhoneNumber, // 行動電話
                BusinessCard = empl.BusinessCard, // 名片職稱
                HireDate = empl.HireDate, // 到職日
                Email = empl.Email, // 主要信箱
                EmailEx = empl.EmailEx, // 其他信箱
                Depts = empl.Depts // 所屬部門職務
            });
            // 若新增成功，加入成功訊息
            if (result == true) successMsg.Add($"已新增 {empl.Account} 人員");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法新增 {empl.Account} 人員，error: {ex.Message}");
        }
    };
};
// 停用人員
async Task DeactivateEmpls(List<EmplModel> empls)
{
    foreach (var empl in empls)
    {
        try
        {
            // 停用人員
            var result = await UofxService.BASE.OrgEmpl.UpdateAcctStatus(new EmplUpdateAcctStatusRequestModel
            {
                CorpCode = _corpCode, // 公司代號
                UserType = UserType.Account, // 人員的類別 ( 設為 Account )
                UserCode = empl.Account, // 帳號
                Active = false // 停用人員帳號
            });
            // 若停用成功，加入成功訊息
            if (result == true) successMsg.Add($"已停用 {empl.Account} 人員");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法停用 {empl.Account} 人員，error: {ex.Message}");
        }
    };
};
// 同步部門主管
async Task SyncSuperiors(List<HrEmplModel> originHrEmpls)
{
    // 取得 HR 系統主管資料
    var superiors = originHrEmpls.Where(e => e.IsSupervisor == true).Select(e => new SuperiorMpdel
    {
        Account = e.Account, // 人員帳號
        DeptCode = e.DeptCode // 部門代號
    }).ToList();

    foreach (var superior in superiors)
    {
        try
        {
            // 設定部門主管
            var result = await UofxService.BASE.Department.Manager.Set(new DeptSetManagerModel()
            {
                CorpCode = _corpCode, // 公司代號
                Code = superior.DeptCode, // 部門代號
                Type = UserType.Account, // 人員的類別 ( 設為 Account )
                Value = superior.Account // 帳號
            });
            // 若設定成功，加入成功訊息
            if (result == true) successMsg.Add($"已設定 {superior.Account} 為 {superior.DeptCode} 部門主管");
        }
        catch (Exception ex)
        {
            // 捕捉錯誤並記錄到 errorMsg 中  
            errorMsg.Add($"無法設定 {superior.Account} 為 {superior.DeptCode} 部門主管，error: {ex.Message}");
        }
    };
};
#endregion

#region 重製
// 重製，用於刪除範例建立的所有資料
async Task Reset()
{
    await DeleteEmpls(new List<string> { "costanza", "natty", "julina", "catharina", "nita", "cary", "gelya", "carmelina", "raviv", "chev", "ezmeralda", "rina", "dory", "conway", "rebekkah", "kath", "betteanne", "antons" });
    await DelteDepts(new List<string> { "BD", "RD", "T", "HR", "S" });
    await DeleteDeptLevels(new List<string> { "lev1", "lev2", "lev3" });
    await DeleteJobTitles(new List<string> { "PM", "TA", "SA", "UX", "QA", "BA", "BE", "IS", "FE" });
    await DeleteJobFuncs(new List<string> { "資訊安全管理", "使用者介面設計", "軟體開發", "技術支援", "後端開發", "專案管理" });
}
// 刪除人員
async Task DeleteEmpls(List<string> strings)
{
    foreach (var code in strings)
    {
        try
        {
            // 刪除人員
            var result = await UofxService.BASE.OrgEmpl.Delete(new EmplDeleteRequestModel()
            {
                CorpCode = _corpCode,
                UserType = UserType.Account,
                UserCode = code
            });
            // 若刪除成功，加入成功訊息
            if (result == true) successMsg.Add($"已刪除 {code} 人員");
        }
        catch (Exception ex)
        {
            errorMsg.Add($"無法刪除 {code} 人員，error: {ex.Message}");
        }
    }
}
// 刪除部門
async Task DelteDepts(List<string> strings)
{
    foreach (var code in strings)
    {
        try
        {
            // 刪除部門
            var result = await UofxService.BASE.Department.Remove(new DepartmentRemoveModel()
            {
                CorpCode = _corpCode,
                Code = code
            });
            // 若刪除成功，加入成功訊息
            if (result == true) successMsg.Add($"已刪除 {code} 部門");
        }
        catch (Exception ex)
        {
            errorMsg.Add($"無法刪除 {code} 部門，error: {ex.Message}");
        }
    }
}
// 刪除部門層級
async Task DeleteDeptLevels(List<string> strings)
{
    foreach (var code in strings)
    {
        try
        {
            // 刪除部門層級
            var result = await UofxService.BASE.DeptLevel.Delete(new DeptLevelDeleteModel()
            {
                CorpCode = _corpCode,
                Code = code
            });
            // 若刪除成功，加入成功訊息
            if (result == true) successMsg.Add($"已刪除 {code} 部門層級");
        }
        catch (Exception ex)
        {
            errorMsg.Add($"無法刪除 {code} 部門層級，error: {ex.Message}");
        }
    }
}
// 刪除職稱
async Task DeleteJobTitles(List<string> strings)
{
    foreach (var code in strings)
    {
        try
        {
            // 刪除職稱
            var result = await UofxService.BASE.JobTitle.Delete(new JobTitleDeleteModel()
            {
                CorpCode = _corpCode,
                Code = code
            });
            // 若刪除成功，加入成功訊息
            if (result == true) successMsg.Add($"已刪除 {code} 職稱");
        }
        catch (Exception ex)
        {
            errorMsg.Add($"無法刪除 {code} 職稱，error: {ex.Message}");
        }
    }
}
// 刪除職務
async Task DeleteJobFuncs(List<string> strings)
{
    foreach (var code in strings)
    {
        try
        {
            // 刪除職務
            var result = await UofxService.BASE.JobFunc.Delete(new JobFuncDeleteModel()
            {
                CorpCode = _corpCode,
                Code = code
            });
            // 若刪除成功，加入成功訊息
            if (result == true) successMsg.Add($"已刪除 {code} 職務");

        }
        catch (Exception ex)
        {
            errorMsg.Add($"無法刪除 {code} 職務，error: {ex.Message}");
        }
    }
}
#endregion

try
{
    // 同步人員部門資料
    await SyncDepts(originHrDepts);
    await SyncEmpls(originHrEmpls);

    // 重製，用於刪除範例建立的所有資料
    //await Reset();

    // 列印錯誤成功訊息
    ConsoleList(errorMsg);
    ConsoleList(successMsg);
}
catch (Exception ex)
{
    //將 exception 轉換成較容易判斷的 model
    var model = UofxService.Error.ConvertToModel(ex);
    //將 model 轉成 json 格式印出
    Console.WriteLine(UofxService.Json.Convert(model));
}

# region 類別 Model
/// <summary>
/// UOF X 部門類別
/// </summary>
public class DeptModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public bool IncludeSubDept { get; set; }
    public string ParentCode { get; set; }
    public string Description { get; set; }
    public string DeptLevelCode { get; set; }
    public int Seq { get; set; }
    public List<DeptModel> SubDepts { get; set; } = new List<DeptModel>(); // 用於儲存子部門
}
/// <summary>
/// UOF X 主管類別
/// </summary>
public class SuperiorMpdel
{
    public string DeptCode { get; set; }
    public string Account { get; set; }
}
/// <summary>
/// UOF X 人員類別
/// </summary>
public class EmplModel
{
    public string CorpCode { get; set; }
    public string Account { get; set; }
    public string Name { get; set; }
    public string EnglishName { get; set; }
    public string EmployeeNumber { get; set; }
    public DateTime? ExpiredTime { get; set; }
    public DateTime? ResignationDate { get; set; }
    public string Gender { get; set; }
    public string IdCardNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string BusinessCard { get; set; }
    public DateTime? HireDate { get; set; }
    public string Email { get; set; }
    public string EmailEx { get; set; }
    public bool Locked { get; set; }
    public bool Active { get; set; }
    public List<EmpCreateOfDeptItemRequestModel> Depts { get; set; }
}
/// <summary>
/// HR 部門類別
/// </summary>
public class HrDeptModel
{
    public string DeptName { get; set; }
    public string DeptCode { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; }
    public bool HasSubDepartments { get; set; }
    public string ParentDeptCode { get; set; }
    public string DeptDescription { get; set; }
    public string DeptLevelCode { get; set; }
    public List<HrDeptModel> SubDepts { get; set; } = new List<HrDeptModel>();
}
/// <summary>
/// HR 人員類別
/// </summary>
public class HrEmplModel
{
    public string Account { get; set; }
    public string Name { get; set; }
    public string EnglishName { get; set; }
    public string EmployeeID { get; set; }
    public DateTime? ExpiredTime { get; set; }
    public string Gender { get; set; }
    public string IDNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string CardTitle { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime? ResignationDate { get; set; }
    public string PrimaryEmail { get; set; }
    public string SecondaryEmail { get; set; }
    public bool AccountLocked { get; set; }
    public bool AccountActive { get; set; }
    public string JobTitleID { get; set; }
    public List<string> JobFunctions { get; set; }
    public string DeptCode { get; set; }
    public bool PrimaryDepartment { get; set; }
    public bool IsSupervisor { get; set; }
}
#endregion
