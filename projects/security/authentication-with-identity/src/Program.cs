using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication.Services;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            EnsureDatabase(host);

            host.Run();
        }

        private static void EnsureDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var options = services.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
                try
                {
                    using (var dbContext = new ApplicationDbContext(options))
                    {
                        dbContext.Database.EnsureCreated();             
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while ensuringt database is created.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Add Database Services
            AddDatabaseServices(services);

            //Add Identity Services
            AddIdentity(services);

            // Add the application services.
            AddApplicationServices(services);

            // Add MVC.
            services.AddControllersWithViews();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>(); // Service uses to send emails.
        }

        private static void AddIdentity(IServiceCollection services)
        {
            // Add Identity and configure it to use the default user and role models and the database context we just added.
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity.
            services.Configure<IdentityOptions>(options =>
            {
                // We set the minimal password policy.
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
            });
        }

        private static void AddDatabaseServices(IServiceCollection services)
        {
            // Add the database context we will use.
            services.AddDbContext<ApplicationDbContext>(options =>
                            options.UseSqlite("Data Source=identity_db.db"));
        }
    }
}
