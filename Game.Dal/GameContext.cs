using Game.Core.Models;
using Game.Dal.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Game.Dal
{
  public class GameContext : DbContext
  {
    public GameContext() { }
    public GameContext(DbContextOptions<GameContext> options) : base(options) { }

    public virtual DbSet<Question> Questions { get; set; }
    public virtual DbSet<Answer> Answers { get; set; }

    public virtual T GetLocalOrAttach<T>(DbSet<T> collection, Func<T, bool> searchLocalQuery, Func<T> getAttachItem) where T : class
    {
      T? localEntity = collection.Local.FirstOrDefault(searchLocalQuery);

      if (localEntity == null)
      {
        localEntity = getAttachItem();
        collection.Attach(localEntity);
      }

      return localEntity;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new QuestionMap());
      modelBuilder.ApplyConfiguration(new AnswerMap());
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //  optionsBuilder.UseSeeding((context, _) => DataSeeder.Seed(context));
    //  optionsBuilder.UseAsyncSeeding((context, _, cancellationToken) => DataSeeder.SeedAsync(context, cancellationToken));
    //}
  }
}
