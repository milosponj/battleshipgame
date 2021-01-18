using BattleshipGame.Models;
using Moq;
using Xunit;

namespace BattleshipGame.Tests
{
    public class GameTests
    {
        private readonly Game _game;
        private readonly Mock<IUserInteraction> userInteractionMock = new Mock<IUserInteraction>();
        private readonly Mock<IBoard> boardMock = new Mock<IBoard>();

        public GameTests()
        {
            boardMock.Setup(bm => bm.Size).Returns(10);
            _game = new Game(boardMock.Object, userInteractionMock.Object);
        }

        [Fact]
        public void Play_OnProperCoordinates_BehavesAsExpected()
        {
            //arrange
            userInteractionMock.SetupSequence(mock => mock.Read())
                .Returns("A1")
                .Returns("B2")
                .Returns("C3")
                .Returns("D4");
            boardMock.Setup(mock => mock.ShootAt(It.IsAny<Coordinate>()))
                .Returns(new ShootResult());

            boardMock.Setup(mock => mock.ShootAt(It.Is<Coordinate>(c => c.X == 3 && c.Y == 3)))
                .Returns(new ShootResult())
                .Callback(() => boardMock.Setup(mock => mock.AreAllShipsSinked).Returns(true)); ;

            //act
            _game.Play();

            //assert
            boardMock.Verify(mock => mock.ShootAt(new Coordinate(0, 0)), Times.Once);
            boardMock.Verify(mock => mock.ShootAt(new Coordinate(1, 1)), Times.Once);
            boardMock.Verify(mock => mock.ShootAt(new Coordinate(2, 2)), Times.Once);
            boardMock.Verify(mock => mock.ShootAt(new Coordinate(3, 3)), Times.Once);
            boardMock.Verify(mock => mock.ShootAt(It.Is<Coordinate>(c=> c.X != 0 && c.X != 1 && c.X != 2 && c.X != 3)), Times.Never);
            boardMock.Verify(mock => mock.ShootAt(It.Is<Coordinate>(c=> c.Y != 0 && c.Y != 1 && c.Y != 2 && c.Y != 3)), Times.Never);
        }

        [Fact]
        public void Play_OnInvalidCoordinates_BehavesAsExpected()
        {
            //arrange
            userInteractionMock.SetupSequence(mock => mock.Read())
                .Returns("z1")
                .Returns("12")
                .Returns("2A")
                .Returns("55")
                .Returns("D4");

            boardMock.Setup(mock => mock.ShootAt(It.Is<Coordinate>(c => c.X == 3 && c.Y == 3)))
                .Returns(new ShootResult())
                .Callback(() => boardMock.Setup(mock => mock.AreAllShipsSinked).Returns(true)); ;

            //act
            _game.Play();

            //assert
            boardMock.Verify(mock => mock.ShootAt(It.IsAny<Coordinate>()), Times.Once);
            userInteractionMock.Verify(mock => mock.Write("Invalid coordinate, please try again.", System.ConsoleColor.White), Times.Exactly(4));            
        }
    }
}
