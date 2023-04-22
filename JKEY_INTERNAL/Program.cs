using JKEY_INTERNAL.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//Connect DB
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
