using Minimal.Api.Pokedex.Models;

namespace Minimal.Api.Pokedex.Services
{
    public class PokedexService : IPokedexService
    {
        private readonly IPokedexRepository _pokedexRepository;

        public PokedexService(IPokedexRepository pokedexRepository)
        {
            _pokedexRepository = pokedexRepository;
        }
        public async Task<PokedexResponse> GetAllAsync()
        {
            var pokemons = await _pokedexRepository.Read();
            return BuildResponse(pokemons);
        }

        public async Task<PokedexPagedResponse> GetAsync(int page, int pageSize)
        {
            var pokemons = await _pokedexRepository.Read();
            return BuildPagedResponse(page, pageSize, pokemons);
        }

        public async Task<PokemonEntity> GetAsync(string name)
        {
            var pokemons = await _pokedexRepository.Read();
            return pokemons.FirstOrDefault(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        public async Task<PokedexPagedResponse> SearchAsync(string query, int page, int pageSize)
        {
            var pokemons = await _pokedexRepository.Read();
            var filteredPokemons = pokemons.Where(p => {
                    var variation = p.Variations[0];
                    return variation.Name.ToLowerInvariant().Contains(query.ToLowerInvariant()) ||
                           variation.Description.ToLowerInvariant().Contains(query.ToLowerInvariant()) ||
                           variation.Image.ToLowerInvariant().Contains(query.ToLowerInvariant()) ||
                           variation.Specie.ToLowerInvariant().Contains(query.ToLowerInvariant());
                });
            return BuildPagedResponse(page, pageSize, filteredPokemons.ToArray());
        }

        private PokedexPagedResponse BuildPagedResponse(int page, int pageSize, PokemonEntity[] pokemons)
        {
            var pokemonCount = pokemons.Count();
            var totalaPages = Convert.ToInt32(Math.Ceiling((double)pokemonCount / pageSize));
            int skipCount = (page - 1) * pageSize;
            return new PokedexPagedResponse
            {
                Data = pokemons.Skip(skipCount).Take(pageSize).Select(p => new PokemonListItemEntity
                {
                    Num = p.Num,
                    Name = p.Name,
                    Image = p.Variations[0].Image
                }).ToArray(),
                Page = page,
                TotalPages = totalaPages,
                TotalResults = pokemonCount
            };
        }

        private static PokedexResponse BuildResponse(PokemonEntity[] pokemons)
        {
            return new PokedexResponse
            {
                Data = pokemons.Select(p => new PokemonListItemEntity
                {
                    Num = p.Num,
                    Name = p.Name,
                    Image = p.Variations[0].Image
                }).ToArray(),
            };
        }
    }
}
