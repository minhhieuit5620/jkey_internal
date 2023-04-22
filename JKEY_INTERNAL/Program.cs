 
using JKEY_INTERNAL.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System.Globalization;
 
﻿
using JKEY_INTERNAL.Models;
using JKEY_INTERNAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
 

var builder = WebApplication.CreateBuilder(args);


 
builder.Services.AddLocalization();
var localizationOptions = new RequestLocalizationOptions();
var supportedCulture = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("vi-VN")
};
localizationOptions.SupportedCultures = supportedCulture;
localizationOptions.SupportedUICultures = supportedCulture;
localizationOptions.SetDefaultCulture("en-US");
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
//Connect DB
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<JkeyInternalContext>(x => x.UseSqlServer(connection));
//// Add services to the container.
builder.Services.AddControllersWithViews();
 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});
builder.Services.AddLocalization();


 



//builder.Services.AddIdentity<User, IdentityRole>(options =>
//{
//    //quy định format password
//    options.Password.RequireDigit = false;//Cần có số
//    options.Password.RequireLowercase = false;//Cần chữ thường
//    options.Password.RequireUppercase = false;//Cần chữ hoa
//    options.Password.RequireNonAlphanumeric = false;//Cần ký tự
//    options.Password.RequiredLength = 6;//min length

//}).AddEntityFrameworkStores<JkeyInternalContext>();


//Dependence Injection 
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IManagerUserService, ManagerUserService>();


////Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),


        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true
    };
});

Cache.Init("Hazelcast", "localhost:5701", "dev", "", 0);
//Cache.InitHazelcast("Hazelcast", "localhost:5701");

var app = builder.Build();

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Sink(new ElasticsearchSink(new ElasticsearchSinkOptions(new Uri("http://localhost:32832"))
{
    AutoRegisterTemplate = true,
    IndexFormat = "api-gateWay-vm-241",
    ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "123456"),
    CustomFormatter = new ElasticsearchJsonFormatter()
})).CreateLogger();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
