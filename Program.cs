using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CSIX.Data;
using CSIX.Services;
using CSIX.Hubs;

namespace CSIX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<CSIXContext>(options =>
                options.UseSqlServer(connectionString, sqlServerOptions =>
        sqlServerOptions.EnableRetryOnFailure()
    ));
            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<CSIXUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<CSIXContext>();
            // Add services to the container.
            builder.Services.AddRazorPages();

            var _c = builder.Configuration.GetConnectionString("AzureSignalRConnectionString");
            builder.Services.AddSignalR().AddAzureSignalR(options =>
            {
                options.ConnectionString = builder.Configuration.GetConnectionString("AzureSignalRConnectionString");
            });

            builder.Services.AddScoped<IDBService, DBService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<TasksHub>("/CheckTasks");
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}