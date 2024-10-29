namespace BowlingApp.Service.Store.Models;

public class Game
{
    public int Id { get; set; } // Unique identifier for each game
    public List<Frame> Frames { get; set; } // Collection of frames in the game
    public int CurrentFrame { get; set; } // The current frame being played
    public bool IsComplete { get; set; } // Flag to check if the game is finished
    public int TotalScore { get; set; } // Total score of the game

    public Game()
    {
        Frames = new List<Frame>();
        for (int i = 0; i < 10; i++)
        {
            Frames.Add(new Frame { FrameNumber = i + 1 });
        }
        CurrentFrame = 1;
        IsComplete = false;
        TotalScore = 0;
    }
}
