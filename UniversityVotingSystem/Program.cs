using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text;
using UniversityVotingSystem.DataBase;
using UniversityVotingSystem.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages().AddRazorPagesOptions(options => 
options.RootDirectory = "/webpages");

var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseMySQL(connectionString);
});

builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationDBContext>();

var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();
//app.MapGet("/", () => "Hello World!");

app.Run();
