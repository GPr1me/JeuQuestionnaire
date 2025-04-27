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
    public async Task<IActionResult> StartGame([FromBody] List<Question> questions)
    {
      await _gameService.StartGame(questions);
      return Ok();
    }

    [HttpPost]
    [Route("next")]
    public async Task<IActionResult> NextQuestion()
    {
      await _gameService.NextQuestion();
      return Ok();
    }

    [HttpPost]
    [Route("end")]
    public async Task<IActionResult> EndGame()
    {
      await _gameService.EndGame();
      return Ok();
    }

    [HttpPost]
    [Route("reset")]
    public async Task<IActionResult> ResetGame()
    {
      await _gameService.ResetGame();
      return Ok();
    }

    [HttpPost]
    [Route("stats")]
    public async Task<IActionResult> ShowGameStats()
    {
      await _gameService.SendScores();
      return Ok();
    }

    [HttpGet]
    [Route("currentQuestion")]
    public async Task<IActionResult> GetCurrentQuestion()
    {
      var question = await _gameService.GetCurrentQuestion();
      if (question == null)
      {
        return NoContent();
      }
      return Ok(question);
    }
  }
}
