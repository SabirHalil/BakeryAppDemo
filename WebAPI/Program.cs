using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);








// Autofac kullanmak için burda 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});


// Add services to the container.

builder.Services.AddCors();


///**********
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var tokenOptions = builder.Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            ClockSkew = TimeSpan.Zero

        };
    });

ServiceTool.Create(builder.Services);


///**********



/*
//***  constructor ITakenCourseService istenirse sen ona TakenCourseManager gönder... ***
builder.Services.AddSingleton<ITakenCourseService,TakenCourseManager>();

builder.Services.AddSingleton<ITakenCourseDal,EfTakenCourseDal>();

builder.Services.AddSingleton<IStudentService,StudentManager>();
builder.Services.AddSingleton<IStudentDal,EfStudentDal>();
*/


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
app.UseCors(builder=>builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


//app.urls.add("https://localhost:7207");
//app.urls.add("https://192.168.1.106:7207");

app.Urls.Add("https://localhost:7207");
app.Urls.Add("https://192.168.1.3:7207");
//app.Urls.Add("https://192.168.1.8:7207");

//app.Run("https://192.168.218.177:7207", https://localhost:7207");
app.Run();
