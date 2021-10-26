using Minimal.Api.Pokedex.Models;

namespace Minimal.Api.Pokedex.Services
{
    public interface IPokedexService
    {
        public Task<PokedexPagedResponse> GetAsync(int page, int pageSize);
        public Task<PokemonEntity> GetAsync(string name);
        public Task<PokedexResponse> GetAllAsync();
        public Task<PokedexPagedResponse> SearchAsync(string query, int page, int pageSize);
    }
}
