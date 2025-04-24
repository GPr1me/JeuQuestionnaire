using Game.App.Services.Interfaces;
using Game.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Game.Controllers
{

  [ApiController]
  [Route("api/game")]
  public class GameController : ControllerBase
  {
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
      _gameService = gameService;
    }

    [HttpPost]
    [Route("start")]
    public IActionResult StartGame([FromBody] List<Question> questions)
    {
      _gameService.StartGame(questions);
      return Ok();
    }

    [HttpPost]
    [Route("next")]
    public IActionResult NextQuestion()
    {
      _gameService.NextQuestion();
      return Ok();
    }

    [HttpPost]
    [Route("end")]
    public IActionResult EndGame()
    {
      _gameService.EndGame();
      return Ok();
    }

    [HttpGet]
    [Route("currentQuestion")]
    public IActionResult GetCurrentQuestion()
    {
      var question = _gameService.GetCurrentQuestion();
      if (question == null)
      {
        return NoContent();
      }
      return Ok(question);
    }
  }
}
