using Ede.Uofx.ThirdPartyAd.Sample.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.Configure<GoogleAuthInfo>(builder.Configuration.GetSection("GoogleAuthInfo"));
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/// <summary>
/// 若想要參考 HomeController 的範例：
/// 1.請將 pattern...=Home; 取消註解
/// 2.請將 pattern...=Google; 註解
/// </summary>
app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Google}/{action=Index}/{id?}");

app.Run();
