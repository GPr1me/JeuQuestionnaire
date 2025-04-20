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

    public async Task Create(Question question)
    {
      await _context.Questions.AddAsync(question);
      await _context.SaveChangesAsync();
    }

    public async Task Update(Question question)
    {
      var localEntity = _context.GetLocalOrAttach(_context.Questions, q => q.Id == question.Id, () => question);
      localEntity.Text = question.Text;
      localEntity.Options = question.Options;
      await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
      var question = await _context.Questions.FindAsync(id);
      if (question != null)
      {
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
      }
    }
  }
}
