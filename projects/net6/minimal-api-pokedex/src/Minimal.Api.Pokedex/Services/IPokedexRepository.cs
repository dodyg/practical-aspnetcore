using Minimal.Api.Pokedex.Models;

namespace Minimal.Api.Pokedex.Services
{
    public interface IPokedexRepository
    {
        public Task<PokemonEntity[]> Read();
    }
}
