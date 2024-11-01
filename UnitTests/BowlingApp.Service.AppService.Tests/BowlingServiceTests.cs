using BowlingApp.Service.Store;

namespace BowlingApp.Service.AppService.Tests;

[TestFixture]
public class BowlingServiceTests
{
    private BowlingService _bowlingService;

    [SetUp]
    public void SetUp()
    {
        _bowlingService = new BowlingService();
    }

    [TearDown]
    public void TearDown() 
    {
        StaticRepository.CleareTheGameRepository();    
    }


    [Test]
    public void InitializeGame_ShouldCreateAndReturnNewGame()
    {
        // Act
        var game = _bowlingService.InitializeGame();

        // Assert
        Assert.IsNotNull(game);
        Assert.That(game.Id, Is.EqualTo(1));
        Assert.IsTrue(StaticRepository.Games.ContainsKey(game.Id));

    }

    [Test]
    public void RecordDelivery_ShouldRecordFirstDelivery()
    {
        // Arrange
        var game = _bowlingService.InitializeGame();

        // Act
        var result = _bowlingService.RecordDelivery(game.Id, 7);

        // Assert
        Assert.That(result, Is.EqualTo("OK"));
        var frame = game.Frames[game.CurrentFrame - 1];
        Assert.That(frame.FirstDelivery, Is.EqualTo(7));
        Assert.IsTrue(frame.IsOpen);
    }

    [Test]
    public void RecordDelivery_ShouldRegisterStrikeAndMoveToNextFrame()
    {
        // Arrange
        var game = _bowlingService.InitializeGame();

        // Act
        var result = _bowlingService.RecordDelivery(game.Id, 10);

        // Assert
        Assert.That(result, Is.EqualTo("OK"));
        var frame = game.Frames[0];
        Assert.IsTrue(frame.IsStrike);
        Assert.That(game.CurrentFrame, Is.EqualTo(2));  // Moved to next frame
    }

    [Test]
    public void RecordDelivery_ShouldRegisterSpareWhenFrameAddsUpToTen()
    {
        // Arrange
        var game = _bowlingService.InitializeGame();

        // Act
        _bowlingService.RecordDelivery(game.Id, 5);
        var result = _bowlingService.RecordDelivery(game.Id, 5);

        // Assert
        Assert.That(result, Is.EqualTo("OK"));
        var frame = game.Frames[0];
        Assert.IsTrue(frame.IsSpare);
        Assert.IsFalse(frame.IsOpen);
    }

    [Test]
    public void GetFrameScore_ShouldReturnCorrectScoreForGivenFrame()
    {
        // Arrange
        var game = _bowlingService.InitializeGame();
        _bowlingService.RecordDelivery(game.Id, 5);
        _bowlingService.RecordDelivery(game.Id, 3);

        // Act
        var frameScore = _bowlingService.GetFrameScore(game.Id, 1);

        // Assert
        Assert.That(frameScore.FrameTotal, Is.EqualTo(8));
        Assert.That(frameScore.FrameNumber, Is.EqualTo(1));
    }

    [Test]
    public void GetTotalScore_ShouldReturnCorrectTotalScore()
    {
        // Arrange
        var game = _bowlingService.InitializeGame();
        _bowlingService.RecordDelivery(game.Id, 10); // Strike
        _bowlingService.RecordDelivery(game.Id, 5);
        _bowlingService.RecordDelivery(game.Id, 4);

        // Act
        var gameScore = _bowlingService.GetTotalScore(game.Id);

        // Assert
        Assert.That(gameScore.TotalScore, Is.EqualTo(28));
    }
}
