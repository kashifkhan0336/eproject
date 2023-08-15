using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Eproject.Data;
using Eproject.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Eproject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("EprojectContextConnection") ?? throw new InvalidOperationException("Connection string 'EprojectContextConnection' not found.");

            builder.Services.AddDbContext<EprojectContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<EprojectUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EprojectContext>();
            builder.Services.AddScoped<SignInManager<EprojectUser>, CustomSignInManager<EprojectUser>>();
            builder.Services.AddCoreAdmin();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            var app = builder.Build();
            app.MapDefaultControllerRoute();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = new string[] { "Admin", "Facualty", "Student" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<EprojectUser>>();
                string email = "kashifkhan0336@gmail.com";
                string password = "Kashif_123";

                if(await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new EprojectUser
                    {
                        Email = email,
                        UserName = email
                    };
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            app.Run();
        }
    }
}