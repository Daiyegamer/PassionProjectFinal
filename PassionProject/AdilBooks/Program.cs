using AdilBooks.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AdilBooks.Services;
using AdilBooks.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ✅ Fix Duplicate DbContext Registration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ✅ Identity Services
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ✅ Register Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

// ✅ Add MVC Controllers
builder.Services.AddControllersWithViews();

// ✅ Add Swagger with Authentication Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AdilBooks API",
        Version = "v1",
        Description = "API documentation for AdilBooks project"
    });

    // ✅ Add Authorization in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}' to authenticate."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// ✅ Configure Middleware Order
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Reading Room API v1");
            c.RoutePrefix = "swagger";
        });
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdilBooks API v1");
        c.RoutePrefix = "swagger"; // Swagger UI available at /swagger
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // ✅ Add Authentication
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

// ✅ Fix Routing Conflicts
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "books",
    pattern: "Books/{action=List}/{id?}",
    defaults: new { controller = "BooksPage" });

app.MapControllerRoute(
    name: "authors",
    pattern: "Authors/{action=List}/{id?}",
    defaults: new { controller = "AuthorPage" });

app.MapControllerRoute(
    name: "publishers",
    pattern: "Publishers/{action=List}/{id?}",
    defaults: new { controller = "PublishersPage" });

app.Run();
