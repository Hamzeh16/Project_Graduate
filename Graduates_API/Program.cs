using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Repositry;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// Identity-Dienste hinzufügen
builder.Services.AddIdentity<ApplicantUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.ConfigureApplicationCookie(option =>
//{
//    option.LoginPath = $"/Identity/Account/Login";
//    option.LogoutPath = $"/Identity/Account/Logout";
//    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
//});

builder.Services.AddScoped<IUnityofWork, UnityofWork>();

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

app.Run();
