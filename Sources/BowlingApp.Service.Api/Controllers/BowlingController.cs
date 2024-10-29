using BowlingApp.Service.AppService;
using BowlingApp.Service.Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingApp.Service.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class BowlingController : Controller
{
    public BowlingController(IBowlingService bowlingService)
    {
        this.bowlingService = bowlingService;
    }

    private IBowlingService bowlingService { get; }

    [HttpPost("initialize")]
    public ActionResult<Game> InitializeGame() => Ok(bowlingService.InitializeGame());

    [HttpPost("{gameId}/record")]
    public ActionResult RecordDelivery(int gameId, [FromBody] int pins) =>
        Ok(bowlingService.RecordDelivery(gameId, pins));

    [HttpGet("{gameId}/frame/{frameNumber}")]
    public ActionResult<FrameScore> GetFrameScore(int gameId, int frameNumber) =>
        Ok(bowlingService.GetFrameScore(gameId, frameNumber));

    [HttpGet("{gameId}/total")]
    public ActionResult<GameScore> GetTotalScore(int gameId) =>
        Ok(bowlingService.GetTotalScore(gameId));
}
