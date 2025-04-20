using Game.Core.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/questions")]
public class QuestionsController : ControllerBase
{
  private readonly Game.App.Services.Interfaces.IQuestionExecutor _questionExecutor;

  public QuestionsController(Game.App.Services.Interfaces.IQuestionExecutor questionExecutor)
  {
    _questionExecutor = questionExecutor;
  }

  [HttpGet]
  public async Task<ActionResult<List<Question>>> GetAll()
  {
    return Ok(await _questionExecutor.GetAll());
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Question>> Get(Guid id)
  {
    var question = await _questionExecutor.Get(id);
    if (question == null)
    {
      return NotFound();
    }
    return Ok(question);
  }

  [HttpPost]
  public async Task<IActionResult> Create(Question question)
  {
    await _questionExecutor.Create(question);
    return CreatedAtAction(nameof(Get), new { id = question.Id }, question);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(Guid id, Question question)
  {
    if (id != question.Id)
    {
      return BadRequest();
    }

    await _questionExecutor.Update(question);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    await _questionExecutor.Delete(id);
    return NoContent();
  }
}
