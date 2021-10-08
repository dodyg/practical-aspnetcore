using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rest_ease
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // define the base url for the API
            var client = RestClient.For<IWeatherApi>("https://wttr.in/");
            // set default values for properties, if needed
            client.Format = "j1";
            // Register the client
            services.AddSingleton(client);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var weatherApi = context.RequestServices.GetService<IWeatherApi>();
                
                var result = await weatherApi.GetWeatherForLocationAsync("paris");

                context.Response.Headers.Add("Content-Type", "application/json");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            });
        }
    }

    // define the API routes
    public interface IWeatherApi
    {
        [Query("format")]
        public string Format { get; set; }

        [Get]
        Task<Weather> GetWeatherAsync([Query("lang")] string language = "en");

        [Get("{location}")]
        Task<Weather> GetWeatherForLocationAsync([Path] string location, [Query("lang")] string language = "en");
    }

    // Define the API response object
    // The actual response has many more fields, but you can pick only the ones you need/want
    public class Weather
    {
        [JsonProperty("current_condition")]
        public List<CurrentCondition> CurrentCondition { get; set; }
    }

    public class CurrentCondition
    {
        [JsonProperty("FeelsLikeC")]
        public long FeelsLikeC { get; set; }

        [JsonProperty("FeelsLikeF")]
        public long FeelsLikeF { get; set; }

        [JsonProperty("cloudcover")]
        public long Cloudcover { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("localObsDateTime")]
        public string LocalObsDateTime { get; set; }

        [JsonProperty("observation_time")]
        public string ObservationTime { get; set; }

        [JsonProperty("precipInches")]
        public string PrecipInches { get; set; }

        [JsonProperty("precipMM")]
        public string PrecipMm { get; set; }

        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [JsonProperty("pressureInches")]
        public long PressureInches { get; set; }

        [JsonProperty("temp_C")]
        public long TempC { get; set; }

        [JsonProperty("temp_F")]
        public long TempF { get; set; }

        [JsonProperty("uvIndex")]
        public long UvIndex { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("visibilityMiles")]
        public long VisibilityMiles { get; set; }

        [JsonProperty("weatherCode")]
        public long WeatherCode { get; set; }

        [JsonProperty("weatherDesc")]
        public List<WeatherDesc> WeatherDesc { get; set; }

        [JsonProperty("winddir16Point")]
        public string Winddir16Point { get; set; }

        [JsonProperty("winddirDegree")]
        public long WinddirDegree { get; set; }

        [JsonProperty("windspeedKmph")]
        public long WindspeedKmph { get; set; }

        [JsonProperty("windspeedMiles")]
        public long WindspeedMiles { get; set; }
    }

    public class WeatherDesc
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}
