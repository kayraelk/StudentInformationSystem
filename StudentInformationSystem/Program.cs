using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem;
using StudentInformationSystem.Authorization;
using StudentInformationSystem.Data;
using StudentInformationSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

// Add Authorization services
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManageRoles", policy => policy.Requirements.Add(new PermissionRequirement("ManageRoles")));
    // Add more policies as needed
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<ICourseCodeService, CourseCodeService>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();// Ensure RoleManager and UserManager are registered
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<PasswordCheckService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Add this line to enable authentication
app.UseAuthorization();

// Ensure roles are created and admin user is assigned
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var adminEmail = "admin@gmail.com"; // Replace with your admin email
    var adminPassword = "1q2w3E*"; // Replace with your admin password
    RoleInitializer.Initialize(services, adminEmail, adminPassword).Wait();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
