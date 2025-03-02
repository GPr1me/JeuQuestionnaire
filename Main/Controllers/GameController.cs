using Game.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class GameController : Controller
  {
    [HttpGet]
    [Route("Test")]
    public IActionResult TestComm(string message, int number)
    {
      return Ok(new TestMessage()
      {
        Message = $"Message successfully received by API: {message}",
        Number = number
      });
    }
  }
}
