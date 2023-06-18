using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using System.Globalization;

namespace Nullam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<NullamDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AppDatabase")));

            CultureInfo estonianCulture = new CultureInfo("et-EE");
            CultureInfo.DefaultThreadCurrentCulture = estonianCulture;
            CultureInfo.DefaultThreadCurrentUICulture = estonianCulture;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler("/Event/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Event}/{action=Index}/{id?}");

            app.Run();
        }
    }
}