using System;
using WebApplication1.Dtos;

namespace WebApplication1.Endpoints;

public static class GamesEndpoints
{
    private static readonly List<GameDto> games = [
        new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992, 7, 15)),
    new (
        2,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(1992, 7, 15)),
    new (
        3,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(1998, 7, 15)),
    ];

    public static RouteGroupBuilder MapGamesEndpoins(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // get games
        group.MapGet("/", () => games);

        //show game by id
        group.MapGet("/{id}", (int Id) =>
        {
            GameDto? game = games.Find(game => game.Id == Id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName("GetGame");

        //store new game
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            /*if (string.IsNullOrEmpty(newGame.Name))
            {
                return Results.BadRequest("Name is required");
            }*/

            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            // تحقق إذا لم يتم العثور على العنصر
            if (index == -1)
            {
                return Results.NotFound(); // إذا لم يتم العثور على اللعبة، أرجع NotFound
            }
            
            games[index] = new GameDto(
               id,
               updatedGame.Name,
               updatedGame.Genre,
               updatedGame.Price,
               updatedGame.ReleaseDate
           );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
