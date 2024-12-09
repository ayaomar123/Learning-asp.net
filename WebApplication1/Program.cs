using WebApplication1.Data;
using WebApplication1.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//to connect to database
//this is not ideal if we not set it in appsettings.json
//var connString = "Data Source=GameStore.db";

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();


app.MapGamesEndpoins();

app.MigrateDb();
app.Run();
