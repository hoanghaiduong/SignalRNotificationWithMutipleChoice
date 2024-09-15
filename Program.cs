using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using myapp.Data;
using myapp.Hubs;
using myapp.MiddlewareExtensions;
using myapp.Repositories;
using myapp.SubscribeTableDependency;

namespace myapp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddSignalR();


            builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Singleton);


            // DI
            builder.Services.AddSingleton<UserRepository>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<NotificationHub>();
            builder.Services.AddSingleton<SubscribeNotificationTableDependencies>();

            // Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None; // Avoid serializing System.Type
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.MapHub<NotificationHub>("/notificationHub");
            app.MapControllerRoute(
              name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSqlTableDependency<SubscribeNotificationTableDependencies>(connectionString);
            app.MapRazorPages();
            app.MapControllers();
            app.Run();
        }
    }
}
