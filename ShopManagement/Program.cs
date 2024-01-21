using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ServiceManagement.DAL;
using ServiceManagement.Services;
using ServiceManagement.Models;
using System.Text.Json.Serialization;
using ShopManagement.Services;
using static ServiceManagement.DAL.AnnouncementRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<AreaService>();
builder.Services.AddScoped<AreaRepository>();
builder.Services.AddScoped<StatusService>();
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<DocumentRepository>();
builder.Services.AddScoped<WorkTimeRepository>();
builder.Services.AddScoped<WorkTimeService>();
builder.Services.AddScoped<ZadaniaService>();
builder.Services.AddScoped<ZadanieRepository>();
builder.Services.AddScoped<AnnouncementService>();
builder.Services.AddScoped<AnnouncementRepositiory>();
builder.Services.AddScoped<IEmailService,EmailService>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConectionString")));
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ServiceManagementContext>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
            });


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});



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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
