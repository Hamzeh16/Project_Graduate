using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Repositry;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// For Entity Framework
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));


// For Identity
builder.Services.AddIdentity<ApplicantUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

// Sent Email
var emailConfig = configuration.GetSection("EmailConfigration").Get<EmailConfigration>();
builder.Services.AddSingleton(emailConfig);

// Add Configration For Required Email
builder.Services.Configure<IdentityOptions>(
    options => options.SignIn.RequireConfirmedEmail = true
    );

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IUnityofWork, UnityofWork>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();//*

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
