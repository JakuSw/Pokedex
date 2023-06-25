using Pokedex.Dtos;

namespace Pokedex.models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Height { get; set; }
    public double Weight { get; set; }
    public List<Skill> Skills { get; set; } = new List<Skill>();

    public void Update(PokemonFormDto pokemonFormDto)
    {
        Name = pokemonFormDto.Name;
        Height = pokemonFormDto.Height;
        Weight = pokemonFormDto.Weight;
    }
}