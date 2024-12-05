using BowlingApp.Service.Api.Controllers;
using BowlingApp.Service.AppService;
using BowlingApp.Service.Store.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingApp.Service.Api.Tests;

[TestFixture]
public class BowlingControllerTests
{
    private Mock<IBowlingService> _mockBowlingService;
    private BowlingController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockBowlingService = new Mock<IBowlingService>();
        _controller = new BowlingController(_mockBowlingService.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
    }

    [Test]    
    public void InitializeGame_ShouldReturnNewGame()
    {
        // Arrange
        var game = new Game { Id = 1 };
        _mockBowlingService.Setup(s => s.InitializeGame()).Returns(game);

        // Act
        var result = _controller.InitializeGame();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(game));
        _mockBowlingService.Verify(s => s.InitializeGame(), Times.Once);
    }
}
