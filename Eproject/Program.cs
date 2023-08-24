using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Eproject.Data;
using Eproject.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using Eproject.Models;
using Microsoft.AspNetCore.Builder;

namespace Eproject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("EprojectContextConnection") ?? throw new InvalidOperationException("Connection string 'EprojectContextConnection' not found.");

            builder.Services.AddDbContext<EprojectContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<EprojectUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8; // Adjust as needed
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<EprojectContext>();

            builder.Services.AddScoped<SignInManager<EprojectUser>, CustomSignInManager<EprojectUser>>();
            builder.Services.AddAutoMapper(typeof(Program));
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //    options.JsonSerializerOptions.WriteIndented = true;
            //});
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // UseAuthentication before UseAuthorization
            app.UseAuthorization();


            app.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "admin",
                    pattern: "admin/{controller=Home}/{action=Index}"); //This is Administrator route. You can you {id} and other parameters which you want

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"); // This route is for Controllers which are situated in project controller folder
            app.MapRazorPages();
            app.MapControllers();
            // Data seeding (roles, users)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EprojectContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<EprojectUser>>();
                SeedData.Initialize(scope.ServiceProvider);
                await SeedRolesAsync(roleManager);
                await SeedDefaultUserAsync(userManager);
            }

            app.Run();
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new string[] { "Admin", "Faculty", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedDefaultUserAsync(UserManager<EprojectUser> userManager)
        {
            string email = "kashifkhan0336@gmail.com";
            string password = "Kashif_123";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new EprojectUser
                {
                    Email = email,
                    UserName = email,
                    Status = UserStatus.Active,
                    Class = "1",
                    Section = "A",
                    Specification = "S",
                    Code = "A12345",
                    Name = "Admin"
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
