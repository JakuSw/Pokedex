using Microsoft.EntityFrameworkCore;
using Pokedex.models;

namespace Pokedex.Data;

public class PokedexContext : DbContext
{
    public PokedexContext(DbContextOptions<PokedexContext> options) : base(options) { }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
}