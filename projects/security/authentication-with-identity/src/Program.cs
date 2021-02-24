using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication.Services;
using Microsoft.Extensions.Hosting;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			// Add the database context we will use.
			services.AddDbContext<ApplicationDbContext>(options=>
				options.UseSqlite("Data Source=identity_db.db"));

			// Add Identity and configure it to use the default user and role models and the database context we just added.
			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

			// Configure Identity.
			services.Configure<IdentityOptions>(options=>
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

			// Add the application services.
            services.AddTransient<IEmailSender, EmailSender>(); // Service uses to send emails.

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
    }
}
