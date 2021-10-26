using Minimal.Api.Pokedex.Services;

namespace Minimal.Api.Pokedex.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPokedexApi(this IServiceCollection services)
        {
            services.AddScoped<IPokedexRepository, PokedexRepository>();
            services.AddScoped<IPokedexService, PokedexService>();
            return services;
        }
    }
}
