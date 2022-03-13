using Minimal.Api.Pokedex.Models;
using Minimal.Api.Pokedex.Services;

namespace Minimal.Api.Pokedex
{
    public static class PokedexApiEndpoint
    {
        public static IEndpointRouteBuilder MapPokedexApiRoutes(this IEndpointRouteBuilder builder)
        {
            MapPagedPokemonListing(builder);
            MapPokemonRead(builder);
            MapAllPokemonListing(builder);
            MapSearchingPokemon(builder);
            return builder;
        }

        private static void MapSearchingPokemon(IEndpointRouteBuilder builder)
        {
            builder.MapGet(RouteConstants.PokemonSearch, async (string query, int? page, int? pageSize, IPokedexService service) =>
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return Results.BadRequest();
                }
                return Results.Ok(await service.SearchAsync(query, page ?? 1, pageSize ?? 20));
            })
            .Produces<PokedexPagedResponse>(StatusCodes.Status200OK)
            .Produces<PokedexPagedResponse>(StatusCodes.Status400BadRequest);
        }

        private static void MapAllPokemonListing(IEndpointRouteBuilder builder)
        {
            builder.MapGet(RouteConstants.AllPokemonListing, async (IPokedexService service) =>
            {
                return await service.GetAllAsync();
            })
            .Produces<PokedexResponse>(StatusCodes.Status200OK);
        }

        private static void MapPokemonRead(IEndpointRouteBuilder builder)
        {
            builder.MapGet(RouteConstants.PokemonRead, async (string name, IPokedexService service) =>
            {
                var pokemon = await service.GetAsync(name);
                if (pokemon == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(pokemon);
            })
            .Produces<PokemonEntity>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }

        private static void MapPagedPokemonListing(IEndpointRouteBuilder builder)
        {
            builder.MapGet(RouteConstants.PagedPokemonListing, async (int? page, int? pageSize, IPokedexService service) =>
            {
                return await service.GetAsync(page ?? 1, pageSize ?? 20);
            })
            .Produces<PokedexPagedResponse>(StatusCodes.Status200OK);
        }
    }
}