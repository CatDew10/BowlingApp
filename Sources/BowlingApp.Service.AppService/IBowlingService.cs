using BowlingApp.Service.Store.Models;

namespace BowlingApp.Service.AppService;

public interface IBowlingService
{
    Game InitializeGame();
    string RecordDelivery(int gameId, int pinsKnockedDown);
    FrameScore GetFrameScore(int gameId, int frameNumber);
    GameScore GetTotalScore(int gameId);
}
