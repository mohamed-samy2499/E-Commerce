using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.ApplicationDbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add The App Db Context with Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("no conection string was found");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// adding identity services like user manager, role manager etc.
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // configure identity services options
        options.Password.RequiredLength = 4;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
