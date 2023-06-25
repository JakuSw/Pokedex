using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Dtos;
using Pokedex.models;

namespace Pokedex.Endpoints;

public class PokemonEndpoint
{
    public static void AddPokemonEndpoint(WebApplication app)
    {
        app.MapPost("/pokemon", (PokedexContext context, PokemonFormDto pokemonForm) =>
        {
            var pokemon = new Pokemon()
            {
                Name = pokemonForm.Name,
                Height = pokemonForm.Height,
                Weight = pokemonForm.Weight
            };
            context.Pokemons.Add(pokemon);
            context.SaveChanges();
            return Results.Created($"/pokemon/{pokemon.Id}", pokemon);
            
        });
        app.MapDelete("/pokemon/{id:int}", (PokedexContext context, int id) =>
        {
            var pokemon = context.Pokemons.FirstOrDefault(s => s.Id == id);
            if (pokemon is null)
            {
                return Results.NotFound();

            }

            context.Pokemons.Remove(pokemon);
            context.SaveChanges();
            return Results.NoContent();
            
            
        });
        app.MapPut("/pokemon/{id:int}", (PokedexContext context, int id, PokemonFormDto pokemonForm) =>
        {
            var pokemon = context.Pokemons.FirstOrDefault(s => s.Id == id);
            if (pokemon is null)
            {
                return Results.NotFound();
            }
            pokemon.Update(pokemonForm);
            context.Pokemons.Update(pokemon);
            context.SaveChanges();
            return Results.Ok(pokemon);
        });

        app.MapGet("/pokemon/{id:int}", (PokedexContext context, int id) =>
        {
            var pokemon = context.Pokemons.FirstOrDefault(s => s.Id == id);
            if (pokemon is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(pokemon);
        });
        app.MapPatch("/pokemon/{id:int}/{skillId:int}", (PokedexContext context, int id, int skillId) =>
        {
            var pokemon = context.Pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon is null)
            {
                return Results.NotFound();
            }

            var skill = context.Skills.FirstOrDefault(s => s.Id == skillId);
            if (skill is null)
            {
                return Results.NotFound();
            }
            pokemon.Skills.Add(skill);
            context.Pokemons.Update(pokemon);
            context.SaveChanges();
            return Results.NoContent();
        });

        app.MapGet("/pokemon-skills/{id:int}", (PokedexContext context, int id) =>
        {
            var pokemon = context.Pokemons.Include(s => s.Skills).FirstOrDefault(p => p.Id == id);
            if (pokemon is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(pokemon);
        });
        app.MapGet("/pokemon", (PokedexContext context) =>
        {
            return context.Pokemons.ToList();
            
        });
        app.MapGet("/pokemon-search/{name}", (PokedexContext context, string name) =>
        {
            var pokemon = context.Pokemons.Where(p => p.Name == name).ToList();
            return Results.Ok(pokemon);
        });
        
    }   
        
}