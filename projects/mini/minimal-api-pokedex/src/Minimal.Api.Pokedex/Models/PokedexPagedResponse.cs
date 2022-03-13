namespace Minimal.Api.Pokedex.Models
{
    public class PokedexPagedResponse : PokedexResponse
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }
}
