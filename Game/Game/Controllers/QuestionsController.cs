using Game.App.Services.Interfaces;
using Game.Core.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/questions")]
public class QuestionsController : ControllerBase
{
  private readonly IWebHostEnvironment _env;
  private readonly IQuestionExecutor _questionExecutor;

  public QuestionsController(IQuestionExecutor questionExecutor, IWebHostEnvironment env)
  {
    _questionExecutor = questionExecutor;
    _env = env;
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

  [HttpPost]
  [Route("uploadContent")]
  public async Task<IActionResult> UploadContent([FromForm] IFormFile file)
  {
    if (file == null || file.Length == 0)
      return BadRequest("No file uploaded.");

    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
    if (!Directory.Exists(uploadsFolder))
      Directory.CreateDirectory(uploadsFolder);

    var filePath = Path.Combine(uploadsFolder, file.FileName);
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }

    var relativePath = $"/uploads/{file.FileName}";
    return Ok(relativePath);
  }
}
