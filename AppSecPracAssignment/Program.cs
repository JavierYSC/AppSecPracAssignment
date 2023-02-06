using AppSecPracAssignment;
using AppSecAssignment.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using AppSecPracAssignment.ViewModels;
using AppSecPracAssignment.Service;

var builder = WebApplication.CreateBuilder(args);

var lockoutOption = new LockoutOptions()
{
	AllowedForNewUsers = true,
	DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1),
	MaxFailedAccessAttempts = 3,
};

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<GoogleCaptchaConfig>(builder.Configuration.GetSection("GoogleReCaptcha"));
builder.Services.AddTransient(typeof(GoogleCaptchaService));
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
	options.Lockout = lockoutOption;
}).AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache(); //save session in memory
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(30);
});

builder.Services.ConfigureApplicationCookie(Config =>
{
	Config.LoginPath = "/Login";
});
var app = builder.Build();

;// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");


app.Run();
