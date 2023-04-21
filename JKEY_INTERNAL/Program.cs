
using JKEY_INTERNAL.Models;
using JKEY_INTERNAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Connect DB
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<JkeyInternalContext>(x => x.UseSqlServer(connection));
//// Add services to the container.
builder.Services.AddControllersWithViews();



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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
