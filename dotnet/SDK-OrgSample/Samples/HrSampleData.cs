using SDK_FirstSample.Models;

namespace SDK_FirstSample.Samples;

public static class HrSampleData
{
    // HR 系統部門資料
    public static List<HrDeptModel> Departments =>
    [
        new()
        {
            DeptName = "研究與開發部",
            DeptCode = "RD",
            Order = 2,
            IsActive = true,
            HasSubDepartments = false,
            ParentDeptCode = "HR",
            DeptDescription = "Contamination NEC"
        },
        new()
        {
            DeptName = "業務發展部",
            DeptCode = "BD",
            Order = 1,
            IsActive = true,
            HasSubDepartments = false,
            ParentDeptCode = "HR",
            DeptDescription = "Adenovirus infect NOS"
        },
        new()
        {
            DeptName = "教育訓練部",
            DeptCode = "T",
            Order = 3,
            IsActive = true,
            HasSubDepartments = false,
            ParentDeptCode = "HR",
            DeptDescription = "TB peritonitis-histo dx"
        },
        new()
        {
            DeptName = "人力資源部",
            DeptCode = "HR",
            Order = 1,
            IsActive = true,
            HasSubDepartments = true,
            ParentDeptCode = "S",
            DeptDescription = "Spon abor w pel inf-unsp"
        },
        new()
        {
            DeptName = "服務處",
            DeptCode = "S",
            Order = 1,
            IsActive = true,
            HasSubDepartments = true,
            ParentDeptCode = "",
            DeptDescription = "Oth coll stn obj-per NEC"
        }
    ];

    // HR 系統職稱資料
    public static List<HrJobTitleModel> JobTitles =>
    [
        new() { JobTitleCode = "UX", JobTitleName = "使用者體驗設計師" },
        new() { JobTitleCode = "PM", JobTitleName = "專案經理" },
        new() { JobTitleCode = "TA", JobTitleName = "技術分析師" },
        new() { JobTitleCode = "SA", JobTitleName = "系統分析師" },
        new() { JobTitleCode = "QA", JobTitleName = "品質保證工程師" },
        new() { JobTitleCode = "BA", JobTitleName = "業務分析師" },
        new() { JobTitleCode = "BE", JobTitleName = "後端工程師" },
        new() { JobTitleCode = "IS", JobTitleName = "資訊安全專員" },
        new() { JobTitleCode = "FE", JobTitleName = "前端工程師" }
    ];

    // HR 系統職務資料
    public static List<HrJobFunctionModel> JobFunctions =>
    [
        new() { JobFunctionCode = "SEC", JobFunctionName = "資訊安全管理" },
        new() { JobFunctionCode = "UI", JobFunctionName = "使用者介面設計" },
        new() { JobFunctionCode = "DEV", JobFunctionName = "軟體開發" },
        new() { JobFunctionCode = "SUP", JobFunctionName = "技術支援" },
        new() { JobFunctionCode = "BE", JobFunctionName = "後端開發" },
        new() { JobFunctionCode = "PM", JobFunctionName = "專案管理" }
    ];

    // HR 系統人員資料
    public static List<HrEmplModel> Employees =>
    [
        new()
        {
            Account = "costanza",
            Name = "松源",
            EnglishName = "costanza Posvner",
            EmployeeID = "A3897",
            Gender = "female",
            IDNumber = "O115627811",
            BirthDate = new DateTime(85, 7, 25),
            PhoneNumber = "0946323278",
            CardTitle = "Human Resources Assistant II",
            HireDate = new DateTime(2013, 12, 21),
            ResignationDate = new DateTime(2027, 10, 18),
            PrimaryEmail = "cposvner0@jalbum.net",
            SecondaryEmail = "cposvner0@phoca.cz",
            AccountActive = true,
            JobTitleID = "UX",
            DeptID = "T",
            PrimaryDepartment = true,
            IsSupervisor = true
        },
        new()
        {
            Account = "natty",
            Name = "丰逸",
            EnglishName = "natty Yankin",
            EmployeeID = "A9618",
            Gender = "female",
            IDNumber = "H114410825",
            BirthDate = new DateTime(85, 1, 14),
            PhoneNumber = "0934756998",
            CardTitle = "Pharmacist",
            HireDate = new DateTime(2020, 3, 2),
            PrimaryEmail = "nyankin1@wiley.com",
            AccountActive = true,
            JobTitleID = "PM",
            JobFunctionIDs = ["SEC"],
            DeptID = "HR",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "julina",
            Name = "思宇",
            EnglishName = "julina Andryushchenko",
            EmployeeID = "A8329",
            ExpiredTime = new DateTime(2023, 2, 10),
            Gender = "other",
            IDNumber = "G212463276",
            BirthDate = new DateTime(87, 4, 10),
            PhoneNumber = "0960836336",
            CardTitle = "Budget/Accounting Analyst III",
            HireDate = new DateTime(2024, 2, 7),
            PrimaryEmail = "jandryushchenko2@amazon.co.uk",
            AccountActive = true,
            JobTitleID = "TA",
            DeptID = "RD",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "catharina",
            Name = "琪煜",
            EnglishName = "catharina Edmonson",
            EmployeeID = "A6879",
            Gender = "female",
            IDNumber = "P207578784",
            BirthDate = new DateTime(90, 7, 18),
            PhoneNumber = "0995946581",
            CardTitle = "Actuary",
            HireDate = new DateTime(2012, 4, 15),
            PrimaryEmail = "cedmonson3@free.fr",
            AccountActive = true,
            JobTitleID = "TA",
            DeptID = "BD",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "nita",
            Name = "梓焓",
            EnglishName = "nita Lowre",
            EmployeeID = "A4523",
            ExpiredTime = new DateTime(2025, 9, 2),
            Gender = "male",
            IDNumber = "R102887411",
            BirthDate = new DateTime(63, 7, 18),
            PhoneNumber = "0995537127",
            CardTitle = "Compensation Analyst",
            HireDate = new DateTime(2011, 5, 17),
            PrimaryEmail = "nlowre4@privacy.gov.au",
            AccountActive = true,
            JobTitleID = "SA",
            JobFunctionIDs = ["UI"],
            DeptID = "T",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "cary",
            Name = "可馨",
            EnglishName = "cary Ralton",
            EmployeeID = "A4550",
            Gender = "male",
            IDNumber = "S208002077",
            BirthDate = new DateTime(62, 1, 26),
            PhoneNumber = "0969873340",
            CardTitle = "Dental Hygienist",
            HireDate = new DateTime(2008, 11, 24),
            PrimaryEmail = "cralton5@admin.ch",
            AccountActive = true,
            JobTitleID = "QA",
            DeptID = "BD",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "gelya",
            Name = "培安",
            EnglishName = "gelya Louder",
            EmployeeID = "A8117",
            Gender = "male",
            IDNumber = "W101757643",
            BirthDate = new DateTime(74, 3, 20),
            PhoneNumber = "0971386307",
            CardTitle = "Compensation Analyst",
            HireDate = new DateTime(2013, 7, 16),
            PrimaryEmail = "glouder6@homestead.com",
            AccountActive = true,
            JobTitleID = "PM",
            DeptID = "HR",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "nita",
            Name = "梓焓",
            EnglishName = "nita Lowre",
            EmployeeID = "A4523",
            ExpiredTime = new DateTime(2025, 9, 2),
            Gender = "male",
            IDNumber = "C115417663",
            BirthDate = new DateTime(63, 7, 18),
            PhoneNumber = "0995537127",
            CardTitle = "Compensation Analyst",
            HireDate = new DateTime(2011, 5, 17),
            PrimaryEmail = "nlowre4@privacy.gov.au",
            AccountActive = true,
            JobTitleID = "TA",
            DeptID = "HR"
        },
        new()
        {
            Account = "costanza",
            Name = "松源",
            EnglishName = "costanza Posvner",
            EmployeeID = "A3897",
            Gender = "female",
            IDNumber = "B213112522",
            BirthDate = new DateTime(85, 7, 25),
            PhoneNumber = "0946323278",
            CardTitle = "Human Resources Assistant II",
            HireDate = new DateTime(2013, 12, 21),
            ResignationDate = new DateTime(2027, 10, 18),
            PrimaryEmail = "cposvner0@jalbum.net",
            SecondaryEmail = "cposvner0@phoca.cz",
            AccountActive = true,
            JobTitleID = "UX",
            JobFunctionIDs = ["SEC"],
            DeptID = "RD"
        },
        new()
        {
            Account = "carmelina",
            Name = "漫妮",
            EnglishName = "carmelina Beadle",
            EmployeeID = "A3583",
            Gender = "male",
            IDNumber = "J214040335",
            BirthDate = new DateTime(85, 10, 8),
            PhoneNumber = "0911960804",
            CardTitle = "Accounting Assistant I",
            HireDate = new DateTime(2011, 3, 26),
            ResignationDate = new DateTime(2027, 9, 25),
            PrimaryEmail = "cbeadle9@go.com",
            AccountActive = true,
            JobTitleID = "TA",
            JobFunctionIDs = ["DEV"],
            DeptID = "HR",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "raviv",
            Name = "澤瀚",
            EnglishName = "raviv Atcherley",
            EmployeeID = "A8007",
            Gender = "male",
            IDNumber = "G212542414",
            BirthDate = new DateTime(98, 4, 20),
            PhoneNumber = "0976843491",
            CardTitle = "Tax Accountant",
            HireDate = new DateTime(2018, 4, 21),
            PrimaryEmail = "ratcherleya@yellowpages.com",
            AccountActive = true,
            JobTitleID = "TA",
            DeptID = "HR",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "chev",
            Name = "雅靜",
            EnglishName = "chev Rowell",
            EmployeeID = "A287",
            Gender = "female",
            IDNumber = "S116701087",
            BirthDate = new DateTime(88, 5, 2),
            PhoneNumber = "0913313487",
            CardTitle = "Automation Specialist I",
            HireDate = new DateTime(2011, 5, 4),
            PrimaryEmail = "crowellb@paginegialle.it",
            AccountActive = true,
            JobTitleID = "BA",
            DeptID = "S",
            PrimaryDepartment = true,
            IsSupervisor = true
        },
        new()
        {
            Account = "ezmeralda",
            Name = "若瑾",
            EnglishName = "ezmeralda Durkin",
            EmployeeID = "A6996",
            ExpiredTime = new DateTime(2029, 5, 18),
            Gender = "other",
            IDNumber = "D116142737",
            BirthDate = new DateTime(56, 6, 8),
            PhoneNumber = "0918733181",
            CardTitle = "Office Assistant I",
            HireDate = new DateTime(2024, 10, 28),
            ResignationDate = new DateTime(2027, 1, 9),
            PrimaryEmail = "edurkinc@tripadvisor.com",
            AccountActive = true,
            JobTitleID = "UX",
            DeptID = "RD",
            PrimaryDepartment = true,
            IsSupervisor = true
        },
        new()
        {
            Account = "rina",
            Name = "俊澤",
            EnglishName = "rina Cootes",
            EmployeeID = "A9043",
            ExpiredTime = new DateTime(2029, 10, 8),
            Gender = "female",
            IDNumber = "S118206414",
            BirthDate = new DateTime(62, 6, 6),
            PhoneNumber = "0971141652",
            CardTitle = "Senior Quality Engineer",
            HireDate = new DateTime(2017, 3, 9),
            PrimaryEmail = "rcootesd@unblog.fr",
            AccountActive = true,
            JobTitleID = "UX",
            JobFunctionIDs = ["DEV"],
            DeptID = "HR",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "dory",
            Name = "雨婷",
            EnglishName = "dory Ashard",
            EmployeeID = "A6241",
            Gender = "male",
            IDNumber = "Y106733062",
            BirthDate = new DateTime(61, 4, 4),
            PhoneNumber = "0944897131",
            CardTitle = "Business Systems Development Analyst",
            HireDate = new DateTime(2013, 2, 25),
            PrimaryEmail = "dasharde@latimes.com",
            AccountActive = true,
            JobTitleID = "BE",
            DeptID = "HR",
            PrimaryDepartment = true,
            IsSupervisor = true
        },
        new()
        {
            Account = "conway",
            Name = "浩霖",
            EnglishName = "conway McAlpine",
            EmployeeID = "A2169",
            Gender = "female",
            IDNumber = "R103802574",
            BirthDate = new DateTime(100, 6, 25),
            PhoneNumber = "0975728820",
            CardTitle = "Physical Therapy Assistant",
            HireDate = new DateTime(2016, 4, 8),
            ResignationDate = new DateTime(2029, 12, 20),
            PrimaryEmail = "cmcalpinef@ted.com",
            SecondaryEmail = "cmcalpinef@scientificamerican.com",
            AccountActive = true,
            JobTitleID = "PM",
            DeptID = "BD",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "rebekkah",
            Name = "博豪",
            EnglishName = "rebekkah Trotter",
            EmployeeID = "A1287",
            Gender = "male",
            IDNumber = "M107183316",
            BirthDate = new DateTime(85, 4, 1),
            PhoneNumber = "0981131325",
            CardTitle = "Assistant Professor",
            HireDate = new DateTime(2020, 5, 8),
            ResignationDate = new DateTime(2026, 6, 4),
            PrimaryEmail = "rtrotterg@abc.net.au",
            AccountActive = true,
            JobTitleID = "IS",
            DeptID = "T",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "kath",
            Name = "博裕",
            EnglishName = "kath Duplain",
            EmployeeID = "A1665",
            Gender = "male",
            IDNumber = "Q115514444",
            BirthDate = new DateTime(81, 11, 11),
            PhoneNumber = "0934443938",
            CardTitle = "Payment Adjustment Coordinator",
            HireDate = new DateTime(2011, 12, 21),
            ResignationDate = new DateTime(2026, 4, 5),
            PrimaryEmail = "kduplainh@cnet.com",
            AccountActive = true,
            JobTitleID = "FE",
            DeptID = "BD",
            PrimaryDepartment = true,
            IsSupervisor = true
        },
        new()
        {
            Account = "betteanne",
            Name = "芮涵",
            EnglishName = "betteanne Facchini",
            EmployeeID = "A3542",
            Gender = "female",
            IDNumber = "V217275312",
            BirthDate = new DateTime(62, 6, 8),
            PhoneNumber = "0960426450",
            CardTitle = "Account Coordinator",
            HireDate = new DateTime(2009, 9, 25),
            ResignationDate = new DateTime(2029, 11, 13),
            PrimaryEmail = "bfacchinii@cnbc.com",
            AccountActive = true,
            JobTitleID = "SA",
            DeptID = "RD",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "betteanne",
            Name = "芮涵",
            EnglishName = "betteanne Facchini",
            EmployeeID = "A3542",
            Gender = "female",
            IDNumber = "O212820223",
            BirthDate = new DateTime(62, 6, 8),
            PhoneNumber = "0960426450",
            CardTitle = "Account Coordinator",
            HireDate = new DateTime(2009, 9, 25),
            ResignationDate = new DateTime(2029, 11, 13),
            PrimaryEmail = "bfacchinii@cnbc.com",
            AccountActive = true,
            JobTitleID = "TA",
            JobFunctionIDs = ["SUP"],
            DeptID = "T"
        },
        new()
        {
            Account = "antons",
            Name = "辰華",
            EnglishName = "antons Walcher",
            EmployeeID = "A8962",
            ExpiredTime = new DateTime(2029, 5, 14),
            Gender = "female",
            IDNumber = "V205826774",
            BirthDate = new DateTime(79, 7, 26),
            PhoneNumber = "0931240619",
            CardTitle = "Environmental Tech",
            HireDate = new DateTime(2013, 5, 8),
            PrimaryEmail = "awalcherj@google.fr",
            AccountActive = true,
            JobTitleID = "BE",
            JobFunctionIDs = ["BE", "SUP"],
            DeptID = "T",
            PrimaryDepartment = true
        },
        new()
        {
            Account = "antons",
            Name = "辰華",
            EnglishName = "antons Walcher",
            EmployeeID = "A8962",
            ExpiredTime = new DateTime(2029, 5, 14),
            Gender = "female",
            IDNumber = "D204001661",
            BirthDate = new DateTime(79, 7, 26),
            PhoneNumber = "0931240619",
            CardTitle = "Environmental Tech",
            HireDate = new DateTime(2013, 5, 8),
            PrimaryEmail = "awalcherj@google.fr",
            AccountActive = true,
            JobTitleID = "PM",
            JobFunctionIDs = ["PM"],
            DeptID = "S"
        }
    ];
}
