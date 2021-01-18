using BattleshipGame.Models;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BattleshipGame.Tests
{
    public class BoardTests
    {
        private readonly Mock<IRandomizer> _randomProviderMock = new Mock<IRandomizer>();
        private readonly IBoard _board;

        public BoardTests()
        {            
            _board = new Board(10, _randomProviderMock.Object);
        }


        [Fact]
        public void AddShipRandomly_ShipCanBePlaced_PlacesShipOnBoard()
        {
            //arrange
            var shipSize = 5;
            var ship = new Ship(shipSize, "Test");
            _randomProviderMock.Setup(rpm => rpm.Next(It.IsAny<int>())).Returns(() => 1);
            //act
            _board.AddShipRandomly(ship);

            //assert
            Assert.False(_board.AreAllShipsSinked);
        }

        [Fact]
        public void AddShipRandomly_ShipTooBigToBePlaced_ThrowsAnException()
        {
            //arrange
            var ship = new Ship(15, "Test");
            _randomProviderMock.Setup(rpm => rpm.Next(It.IsAny<int>())).Returns(() => 1);
            // act & assert
            Assert.Throws<Exception>(() => _board.AddShipRandomly(ship));
        }

        [Fact]
        public void ShootAt_EmptyBoardPosition_ReturnsMissShootResult()
        {
            // act 
            var shootResult = _board.ShootAt(new Coordinate(0,0));

            //assert
            Assert.Null(shootResult.ShipHit);
            Assert.True(shootResult.ShootStatus == Enums.ShootStatus.ShipMissed);
        }

        [Fact]
        public void ShootAt_BoardPositionWithShip_ReturnsShipHitShootResult()
        {
            //arrange
            _randomProviderMock.Setup(rpm => rpm.Next(It.IsAny<int>())).Returns(() => 0);
            _board.AddShipRandomly(new Ship(1, "Test"));

            // act 
            var shootResult = _board.ShootAt(new Coordinate(0, 0));

            //assert
            Assert.NotNull(shootResult.ShipHit);
            Assert.True(shootResult.ShootStatus == Enums.ShootStatus.ShipHit);
        }

        [Fact]
        public void ShootAt_BoardPositionAlreadyHit_ReturnsAlreadyHitShootResult()
        {
            //arrange
            _board.ShootAt(new Coordinate(0, 0));

            // act 
            var shootResult = _board.ShootAt(new Coordinate(0, 0));

            //assert
            Assert.True(shootResult.ShootStatus == Enums.ShootStatus.AlreadyHit);
        }

        [Fact]
        public void ToString_OfInitialBoard_ReturnsExpectedResult()
        {
            //arrange
            var board10x10 = "  1 2 3 4 5 6 7 8 9 10\nA| | | | | | | | | | |\nB| | | | | | | | | | |\nC| | | | | | | | | | |\nD| | | | | | | | | | |\nE| | | | | | | | | | |\nF| | | | | | | | | | |\nG| | | | | | | | | | |\nH| | | | | | | | | | |\nI| | | | | | | | | | |\nJ| | | | | | | | | | |\n";

            //act
            var stringRepresentation = _board.ToString();
            
            //assert
            Assert.Equal(board10x10, stringRepresentation);
        }

        [Fact]
        public void ToString_AfterShotAtBoard_ReturnsExpectedResult()
        {
            //arrange
            _randomProviderMock.Setup(rpm => rpm.Next(It.IsAny<int>())).Returns(() => 0);
            _board.AddShipRandomly(new Ship(1, "Test"));

            //act
            _board.ShootAt(new Coordinate(0, 0));
            _board.ShootAt(new Coordinate(1, 1));
            _board.ShootAt(new Coordinate(2, 2));
            var stringRepresentation = _board.ToString();

            //assert
            Assert.Equal(2, stringRepresentation.Count(c =>c == 'o'));
            Assert.Equal(1, stringRepresentation.Count(c => c == 'X'));
        }
    }
}
