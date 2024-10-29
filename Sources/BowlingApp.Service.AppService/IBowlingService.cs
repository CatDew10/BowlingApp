using BowlingApp.Service.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingApp.Service.AppService;

public interface IBowlingService
{
    Game InitializeGame();
    string RecordDelivery(int gameId, int pinsKnockedDown);
    FrameScore GetFrameScore(int gameId, int frameNumber);
    GameScore GetTotalScore(int gameId);
}
