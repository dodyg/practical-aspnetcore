namespace Minimal.Api.Pokedex.Models
{
    public class PokemonEntity
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public Variation[] Variations { get; set; }
        public string Link { get; set; }
    }

    public class Variation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string[] Types { get; set; }
        public string Specie { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string[] Abilities { get; set; }
        public Stats Stats { get; set; }
        public string[] Evolutions { get; set; }
    }

    public class Stats
    {
        public int Total { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpeedAttack { get; set; }
        public int SpeedDefense { get; set; }
        public int Speed { get; set; }
    }

}