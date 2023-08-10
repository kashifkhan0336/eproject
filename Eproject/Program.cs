using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Eproject.Data;
using Eproject.Areas.Identity.Data;

namespace Eproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("EprojectContextConnection") ?? throw new InvalidOperationException("Connection string 'EprojectContextConnection' not found.");

            builder.Services.AddDbContext<EprojectContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<EprojectUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<EprojectContext>();
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
            app.Run();
        }
    }
}