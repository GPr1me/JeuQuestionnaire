using Game.App.Repos;
using Game.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Game.Dal.Repos
{
  public class QuestionRepository : IQuestionRepository
  {
    private readonly GameContext _context;

    public QuestionRepository(GameContext context)
    {
      _context = context;
    }

    public async Task<Question> Get(Guid id)
    {
      return await _context.Questions.Include(q => q.Options)
                                     .FirstAsync(q => q.Id == id);
    }

    public async Task<List<Question>> GetAll()
    {
      return await _context.Questions.Include(q => q.Options)
                                     .ToListAsync();
    }

    public async Task Create(Question created)
    {
      await _context.Questions.AddAsync(created);
      await _context.SaveChangesAsync();
    }

    public async Task Update(Question updated)
    {
      var toUpdate = await _context.Questions.Include(q => q.Options)
                                             .FirstOrDefaultAsync(q => q.Id == updated.Id);
      if (toUpdate is null) throw new ArgumentNullException(nameof(toUpdate), "Cannot update null reference");

      _context.Entry(toUpdate).CurrentValues.SetValues(updated);


      // Options to add
      updated.Options.ExceptBy(toUpdate.Options.Select(currentC => currentC.Id), newC => newC.Id)
                      .ToList()
                      .ForEach(o => toUpdate.Options.Add(o));

      // Options to remove
      toUpdate.Options.ExceptBy(updated.Options.Select(newC => newC.Id), currentC => currentC.Id)
                      .ToList()
                      .ForEach(o => toUpdate.Options.Remove(o));

      // Options to update (same Id but different properties)
      updated.Options.IntersectBy(toUpdate.Options.Select(currentC => currentC.Id), newC => newC.Id)
                      .ToList()
                      .ForEach(updatedOption =>
                      {
                        var existingOption = toUpdate.Options.First(o => o.Id == updatedOption.Id);
                        _context.Entry(existingOption).CurrentValues.SetValues(updatedOption);
                      });

      await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
      var toDelete = await _context.Questions.FindAsync(id);
      if (toDelete == null) throw new ArgumentNullException(nameof(toDelete), "Cannot delete null reference");

      _context.Questions.Remove(toDelete);
      await _context.SaveChangesAsync();
    }
  }
}
