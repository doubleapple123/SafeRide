using SafeRide.src.Interfaces;
using SafeRide.src.DataAccess;
using SafeRide.src.Models;
using SafeRide.src.Managers;
using SafeRide.src.Archiving;
using System.IO.Compression;
using System.Threading;
using SafeRide.src.Services;
using SafeRide.src.Interfaces;
using SafeRide.src.DataAccess;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using SafeRide.src.Security;
using SafeRide.src.Security.UserSecurity;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
