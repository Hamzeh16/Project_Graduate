using Graduates_Data.Data;
using Graduates_Model.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Identity-Dienste hinzufügen
builder.Services.AddIdentity<ApplicantUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//builder.Services.ConfigureApplicationCookie(option =>
//{
//    option.LoginPath = $"/Identity/Account/Login";
//    option.LogoutPath = $"/Identity/Account/Logout";
//    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
//});
//builder.Services.AddScoped<IUnitOfWorkRepositray, UnitOfWorkRepositray>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{area=Applicant}/{controller=Home}/{action=Index}/{id?}");


app.Run();
