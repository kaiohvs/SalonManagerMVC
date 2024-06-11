using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalonManager.Data.Context;
using SalonManager.Data.Repositories;
using SalonManager.Domain.Interfaces;
using SalonManager.Services.Mapper;
using SalonManager.Services.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.LoginPath = "/Account/Login"; // Defina a p�gina de login
           options.AccessDeniedPath = "/Account/AccessDenied";

       });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireAuthenticatedUser(); // Requer que o usu�rio esteja autenticado
        policy.RequireRole("Admin"); // Requer que o usu�rio tenha a fun��o 'Admin'
    });
});

//Confifura��o de AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllersWithViews();

// Configurar DbContext com a string de conex�o
builder.Services.AddDbContext<AppDbContext>(options =>
{
    IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();


    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

// Registrar reposit�rios e servi�os
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("Views/Shared/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Adicionar os middlewares de autentica��o e autoriza��o
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "Login",
    pattern: "{controller=Account}/{action=Login}/{id?}");




// Configure o roteamento para servir o arquivo HTML como p�gina inicial
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
