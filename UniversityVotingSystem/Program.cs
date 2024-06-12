using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityVotingSystem;
using UniversityVotingSystem.DataBase;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.Repository;

var builder = WebApplication.CreateBuilder(args);

//Logging
builder.Services.AddLogging();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Exception handling
//builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//Razor pages
builder.Services.AddRazorPages()/*.AddRazorPagesOptions(options =>
options.RootDirectory = "/webpages"
)*/.AddRazorPagesOptions(options => {
    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
    options.Conventions.AddAreaPageRoute("UserAuthentication","/Login/LoginPage", "Login");
    options.Conventions.AddAreaPageRoute("UserAuthentication", "/Register/Registration", "Registration");
    options.Conventions.AddAreaPageRoute("VotingManipulations", "/CreateVoting/CreateVoting", "CreateVoting");
    options.Conventions.AddPageRoute("/Profile/Profile", "/Profile");});

//Database
var connectionString = builder.Configuration.GetConnectionString("Default");
connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException("The connection string has not been yet initialized");
}

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseMySQL(connectionString);
});

builder.Services.AddScoped<IDataBaseRepository, DataBaseRepository>();
builder.Services.AddScoped<IPropositionVotingRepository, PropositionVotingRepository>();
//Authentication
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
string googleConfigurationId = builder.Configuration["GoogleAuth:clientId"] ?? "nothingId";
string googleConfigurationSecret = builder.Configuration["GoogleAuth:clientSecret"] ?? "noSecret";

//Check for google configuration id and secret to be found
if (!googleConfigurationId.Equals("nothingId") && !googleConfigurationSecret.Equals("noSecret"))
{
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = googleConfigurationId;
        googleOptions.ClientSecret = googleConfigurationSecret;
    });
}

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
builder.Services.AddSignalR();
var app = builder.Build();/*
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseExceptionHandler(_ => {});*/
app.UseRouting();
app.UseStaticFiles();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<VoteHub>("/vote");
app.MapGet("/", context => {
        context.Response.Redirect("/MainPage");
        return Task.CompletedTask;
    });
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
