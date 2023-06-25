using Pokedex.Data;
using Pokedex.Dtos;
using Pokedex.models;

namespace Pokedex.Endpoints;

public class SkillEndpoint
{
    public static void AddSkillEndpoints(WebApplication app)
    {
        app.MapPost("/skill", (PokedexContext context, SkillFormDto skillForm) =>
        {
            var skill = new Skill()
            {
                Name = skillForm.Name,
                Description = skillForm.Description
            };
            context.Skills.Add(skill);
            context.SaveChanges();
            return Results.Created($"/skill/{skill.Id}", skill);
            
        });
        app.MapDelete("/skill/{id:int}", (PokedexContext context, int id) =>
        {
            var skill = context.Skills.FirstOrDefault(s => s.Id == id);
            if (skill is null)
            {
                return Results.NotFound();

            }

            context.Skills.Remove(skill);
            context.SaveChanges();
            return Results.NoContent();
            
            
        });
        app.MapGet("/skill/{id:int}", (PokedexContext context, int id) =>
        {
            var skill = context.Skills.FirstOrDefault(s => s.Id == id);
            if (skill is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(skill);


        });
    }
}