using Game.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Game.Dal
{
  internal class GameContextFactory : IDesignTimeDbContextFactory<GameContext>
  {
    GameContext IDesignTimeDbContextFactory<GameContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName)
                                                                   .AddJsonFile("Game/Game/appsettings.Development.json")
                                                                   .Build();

      var gameContextOptions = configuration.GetRequiredSection("GameContextOptions").Get<GameContextOptions>()!;

      var builder = new DbContextOptionsBuilder<GameContext>();

      builder.UseNpgsql(gameContextOptions.ConnectionString, b => b.MigrationsAssembly(gameContextOptions.AssemblyName));

      return new GameContext(builder.Options);
    }
  }
}