using Microsoft.EntityFrameworkCore;

namespace Pokedex.Data;

public class PokedexContext : DbContext
{
    public PokedexContext(DbContextOptions<PokedexContext> options) : base(options) { }
}