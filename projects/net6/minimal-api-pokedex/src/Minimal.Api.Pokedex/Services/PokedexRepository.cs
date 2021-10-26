using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Minimal.Api.Pokedex.Models;

namespace Minimal.Api.Pokedex.Services
{
    public class PokedexRepository : IPokedexRepository
    {
        private readonly string _dataFile;
        private IList<PokemonEntity> _pokemons = null;
        private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public PokedexRepository(IWebHostEnvironment environment)
        {
            _dataFile = Path.Combine(environment.ContentRootPath,"Data","pokemon.json");
        }

        public async Task<PokemonEntity[]> Read()
        {
            if(_pokemons == null) 
                _pokemons = await LoadPokemons();
            return _pokemons.ToArray();
        }

        private async Task<IList<PokemonEntity>> LoadPokemons()
        {
            var data = await File.ReadAllTextAsync(_dataFile);
            var pokemons = new List<PokemonEntity>();
            using (var document = JsonDocument.Parse(data))
            {
                foreach(var item in document.RootElement.EnumerateArray())
                {
                    pokemons.Add(JsonSerializer.Deserialize<PokemonEntity>(item, _jsonOptions));
                }
            }
            return pokemons;
        }
    }
}
