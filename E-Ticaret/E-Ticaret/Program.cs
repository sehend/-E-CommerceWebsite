using Core.EmailOptions;
using Core.Model;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Data;
using Data.Repository;
using Data.UnitOfWorks;
using E_Ticaret.Extenion;
using E_Ticaret.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        options =>
        {
            options.MigrationsAssembly("Data");
        });
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddIDentityWithExtenions();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IMainPageService, MainPageService>();

builder.Services.AddScoped<IMainPageRepository, MainPageRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddScoped<IBasketService, BasketService>();



var app = builder.Build();

using (var scop = app.Services.CreateScope())
{
    var roleManager = scop.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

    await PermissionSeed.Seed(roleManager, app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
