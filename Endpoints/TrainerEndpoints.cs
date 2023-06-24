namespace Pokedex.Endpoints;

public static class TrainerEndpoints
{
    public static void AddTrainerEndpoints(WebApplication app)
    {
        app.MapGet("/", () =>
        {
            return Results.Ok("Hello world");
        });
    }
}