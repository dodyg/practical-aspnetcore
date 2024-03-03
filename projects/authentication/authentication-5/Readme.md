## Authentication using Identity API in .NET 8

### To add the Identity API to our app, we need to follow the below steps:

- Add the required EF Core packages
  - dotnet add package Microsoft.EntityFrameworkCore.Design
  - dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
  - dotnet add package Microsoft.EntityFrameworkCore.SQLite (using simple SQLite for this demo)
- Extend IdentityDbContext and IdentityUser

```
      public class ApplicationDBContext:IdentityDbContext<ApplicationUser>
      {
          public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
          {
          }
      }
```

```
      public class ApplicationUser : IdentityUser
      {

      }
```

- Add IdentityAPI endpoints in Program.cs

```
      builder.Services
          .AddIdentityApiEndpoints<ApplicationUser>()
          .AddEntityFrameworkStores<ApplicationDBContext>();
```

- Create DB migrations

```
      dotnet ef migrations add InitialSchema
      dotnet ef database update
```

- Authorise the API

```
      app.MapGet("/weatherforecast", () =>
      {
      var forecast =  Enumerable.Range(1, 5).Select(index =>
          new WeatherForecast
          (
              DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
              Random.Shared.Next(-20, 55),
              summaries[Random.Shared.Next(summaries.Length)]
          ))
          .ToArray();
          return forecast;
      })
      .WithName("GetWeatherForecast")
      .RequireAuthorization() -- add this line of code
      .WithOpenApi();
```
