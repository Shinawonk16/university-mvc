using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using UniversityManagementMvc.Data.ContextClass;
using UniversityManagementMvc.Implementation.Respository;
using UniversityManagementMvc.Implementation.Services;
using UniversityManagementMvc.Interface.Respository;
using UniversityManagementMvc.Interface.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
  {
      config.LoginPath = "/Login/Login";
      config.Cookie.Name = "UniversityManagementMvc";
      config.LogoutPath = "/Login/LogOut";
  });

builder.Services.AddAuthentication();
builder.Services.AddScoped<IChancellorService, ChancellorServices>();
builder.Services.AddScoped<IChancellorRespository, ChancellorRespository>();

builder.Services.AddScoped<ILecturerService, LecturerServices>();
builder.Services.AddScoped<ILecturerRespository, LecturerRespository>();

builder.Services.AddScoped<IManagementService, ManagementServices>();
builder.Services.AddScoped<IManagementRespository, ManagementRespository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRespository, StudentRespository>();

builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<IUserRespository, UserRespository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
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
app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
