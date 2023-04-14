using ChildCare.Code.Attributes;
using ChildCare.Services;
using ChildCare.Services.Dashboard;
using ChildCareCore.SiteKeys;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var gatewayURL = "https://localhost:7163/";
// Add services to the container.

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;

});

SiteKeys.Configure(builder.Configuration.GetSection("SiteKeys"));

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //options.IdleTimeout = TimeSpan.FromSeconds(10);               
});

builder.Services.Configure<CookieTempDataProviderOptions>(options =>
{
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

});
#region Service Implementation 

builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpService, HttpService>();
builder.Services.AddHttpClient<IAccountService, AccountService>(client =>
{
    client.BaseAddress = new Uri(gatewayURL);
});

builder.Services.AddHttpClient<IDashboardService, DashboardService>(client =>
{
    client.BaseAddress = new Uri(gatewayURL);
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.LoginPath = "/Login/SignInUser";
});

#endregion

var app = builder.Build();
ContextProvider.Configure(app.Services.GetRequiredService<IHttpContextAccessor>(), app.Environment);
// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "superadmin",
    pattern: "{area=SuperAdmin}/{controller=Dashboard}/{action=Dashboard}/{id?}");

//app.MapControllerRoute(name: "user",
//    pattern: "{area=User}/{controller=Payment}/{action=PaymentIndex}/{id?}");

app.Run();
