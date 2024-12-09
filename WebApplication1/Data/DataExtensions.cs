using System;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var dbcontext = scope.ServiceProvider.GetService<GameStoreContext>();
        dbcontext.Database.Migrate();
    }
}
