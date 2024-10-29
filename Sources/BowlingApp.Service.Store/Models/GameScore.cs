namespace BowlingApp.Service.Store.Models;

public class GameScore
{
    public int TotalScore { get; set; }
    public List<FrameScore> FrameScores { get; set; }
}
