using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text;
using UniversityVotingSystem.DataBase;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages().AddRazorPagesOptions(options => 
options.RootDirectory = "/webpages");

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseMySQL(connectionString);
});

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["GoogleAuth:clientId"];
        googleOptions.ClientSecret = builder.Configuration["GoogleAuth:clientSecret"];
    });
builder.Services.AddScoped<IDataBaseRepository, DataBaseRepository>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseStaticFiles();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/error", () => "sorry, an error occured");
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//    var roles = new[] { "Admin", "SimpleUser" };

//    foreach(var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//            await roleManager.CreateAsync(new IdentityRole(role));
//    }
//}

//using (var scope = app.Services.CreateScope())
//{
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

//    string email = "admin@admin.com";
//    string password = "Test1234,";

//    if(await userManager.FindByEmailAsync(email) == null)
//    {
//        var user = new User();
//        user.first_name = "Administrator";
//        user.second_name = "account";
//        user.phone_number = "1234141241";
//        user.role = "Admin";
//        user.EmailConfirmed = true;
//        user.UserName = "Administrator";
//        user.Email = email;

//        await userManager.CreateAsync(user, password);

//        await userManager.AddToRoleAsync(user, "Admin");
//    }
//}

app.Run();
