using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HelloWorldWithReload 
{
    public class User
    {

    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var password = context.Request.Query["password"];

                if (string.IsNullOrWhiteSpace(password))
                    password = "123456789";

                var hasher = new PasswordHasher<User>();
                var hashedPassword = hasher.HashPassword(new User(), password);

                return context.Response.WriteAsync($"Append ?password at url to test your own password hashing. Password : {password} => Hashed : {hashedPassword}");
            });
        }
    }
    
   public class Program
    {
        public static void Main(string[] args)
        {
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}