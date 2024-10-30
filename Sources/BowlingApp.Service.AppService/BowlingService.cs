using BowlingApp.Service.Store;
using BowlingApp.Service.Store.Models;

namespace BowlingApp.Service.AppService;

public class BowlingService : IBowlingService
{

    public Game InitializeGame()
    {
        var game = new Game { Id = 1};
        StaticRepository.Games.Add(game.Id, game);
        return game;
    }

    public string RecordDelivery(int gameId, int pinsKnockedDown)
    {
        if (!StaticRepository.Games.ContainsKey(gameId))
            throw new ArgumentException("Invalid game ID.");

        var game = StaticRepository.Games[gameId];
        var frame = game.Frames[game.CurrentFrame - 1];

        // Determine if this is the first, second, or third delivery in a frame
        if (frame.FirstDelivery == null)
        {
            frame.FirstDelivery = pinsKnockedDown;

            if (pinsKnockedDown == 10) // Strike condition
            {
                frame.IsStrike = true;
                frame.IsOpen = false;
                MoveToNextFrame(game);
            }
        }
        else if (frame.SecondDelivery == null)
        {
            frame.SecondDelivery = pinsKnockedDown;

            // Check for spare
            if (frame.FirstDelivery + frame.SecondDelivery == 10)
            {
                frame.IsSpare = true;
                frame.IsOpen = false;
            }
            else
            {
                frame.IsOpen = true;
            }

            MoveToNextFrame(game);
        }
        else if (game.CurrentFrame == 10 && (frame.IsStrike || frame.IsSpare))
        {
            // Special third delivery in the 10th frame
            frame.ThirdDelivery = pinsKnockedDown;
            game.IsComplete = true;
        }

        UpdateGameScores(game);

        return "OK";
    }

    public FrameScore GetFrameScore(int gameId, int frameNumber)
    {
        if (!StaticRepository.Games.ContainsKey(gameId))
            throw new ArgumentException("Invalid game ID.");

        var game = StaticRepository.Games[gameId];
        if (frameNumber < 1 || frameNumber > game.Frames.Count)
            throw new ArgumentException("Invalid frame number.");

        var frame = game.Frames[frameNumber - 1];
        return new FrameScore
        {
            FrameNumber = frame.FrameNumber,
            FrameTotal = frame.Score,
            IsStrike = frame.IsStrike,
            IsSpare = frame.IsSpare,
            IsOpen = frame.IsOpen
        };
    }

    public GameScore GetTotalScore(int gameId)
    {
        if (!StaticRepository.Games.ContainsKey(gameId))
            throw new ArgumentException("Invalid game ID.");

        var game = StaticRepository.Games[gameId];
        return new GameScore
        {
            TotalScore = game.TotalScore,
            FrameScores = game.Frames.Select(f => new FrameScore
            {
                FrameNumber = f.FrameNumber,
                FrameTotal = f.Score,
                IsStrike = f.IsStrike,
                IsSpare = f.IsSpare,
                IsOpen = f.IsOpen
            }).ToList()
        };
    }

    private void MoveToNextFrame(Game game)
    {
        if (game.CurrentFrame < 10)
        {
            game.CurrentFrame++;
        }
        else
        {
            game.IsComplete = true;
        }
    }

    private void UpdateGameScores(Game game)
    {
        int totalScore = 0;

        for (int i = 0; i < game.Frames.Count; i++)
        {
            var frame = game.Frames[i];

            if (frame.IsStrike)
            {
                frame.Score = 10 + GetStrikeBonus(game, i);
            }
            else if (frame.IsSpare)
            {
                frame.Score = 10 + GetSpareBonus(game, i);
            }
            else
            {
                frame.Score = (frame.FirstDelivery ?? 0) + (frame.SecondDelivery ?? 0);
            }

            totalScore += frame.Score;
        }

        game.TotalScore = totalScore;
    }

    private int GetStrikeBonus(Game game, int frameIndex)
    {
        int bonus = 0;
        if (frameIndex + 1 < game.Frames.Count)
        {
            var nextFrame = game.Frames[frameIndex + 1];
            bonus += nextFrame.FirstDelivery ?? 0;

            if (nextFrame.IsStrike && frameIndex + 2 < game.Frames.Count)
            {
                bonus += game.Frames[frameIndex + 2].FirstDelivery ?? 0;
            }
            else
            {
                bonus += nextFrame.SecondDelivery ?? 0;
            }
        }
        return bonus;
    }

    private int GetSpareBonus(Game game, int frameIndex)
    {
        return frameIndex + 1 < game.Frames.Count
            ? game.Frames[frameIndex + 1].FirstDelivery ?? 0
            : 0;
    }
}
