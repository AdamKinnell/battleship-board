using System.Collections.Generic;
using System.Linq;
using battleship_board;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace battleship_board_tests {

    [TestClass()]
    public class BattleshipLocationTests {

        [TestMethod()]
        public void IterateFootprint_Returns4Coordinates_For2x2Battleship() {
            var location = new BattleshipLocation() {
                Battleship = new Battleship(2, 2),
                TopLeft = new Coord() { X = 2, Y = 2 },
            };
            var footprintSize = location.IterateFootprint().Count();
            Assert.AreEqual(footprintSize, 4);
        }

        [TestMethod()]
        public void IterateFootprint_Returns1Coordinate_For1x1Battleship() {
            var location = new BattleshipLocation() {
                Battleship = new Battleship(1, 1),
                TopLeft = new Coord() { X = 2, Y = 2 },
            };
            var footprintSize = location.IterateFootprint().Count();
            Assert.AreEqual(footprintSize, 1);
        }

        [TestMethod()]
        public void IterateFootprint_ReturnsCorrectlyOffsetCoordinates_For2x2Battleship() {
            var location = new BattleshipLocation() {
                Battleship = new Battleship(2, 2),
                TopLeft = new Coord() { X = 3, Y = 3 },
            };

            var actual_footprint = location.IterateFootprint().ToList();
            var expected_footprint = new List<Coord> {
                new Coord() { X = 3, Y = 3 },
                new Coord() { X = 4, Y = 3 },
                new Coord() { X = 3, Y = 4 },
                new Coord() { X = 4, Y = 4 },
            };

            // Compare regardless of order
            CollectionAssert.AreEquivalent(expected_footprint, actual_footprint);
        }

        [TestMethod]
        public void ToBattleShipCoord_ReturnsBattleshipOrigin_WhenGivenTopLeft() {
            var location = new BattleshipLocation() {
                Battleship = new Battleship(2, 2),
                TopLeft = new Coord() { X = 3, Y = 3 },
            };

            // Top-left
            var expected = new Coord() { X = 0, Y = 0 };
            var actual = location.ToBattleshipCoord(location.TopLeft);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToBattleShipCoord_ReturnsBattleshipCoordinate_WhenGivenParentCoordinate() {
            var location = new BattleshipLocation() {
                Battleship = new Battleship(2, 2),
                TopLeft = new Coord() { X = 3, Y = 3 },
            };

            // Top-right
            var expected = new Coord() { X = 1, Y = 0 };
            var actual = location.ToBattleshipCoord(new Coord() { X = 4, Y = 3 });
            Assert.AreEqual(expected, actual);

            // Bottom-right
            expected = new Coord() { X = 1, Y = 1 };
            actual = location.ToBattleshipCoord(new Coord() { X = 4, Y = 4 });
            Assert.AreEqual(expected, actual);

            // Bottom-left
            expected = new Coord() { X = 0, Y = 1 };
            actual = location.ToBattleshipCoord(new Coord() { X = 3, Y = 4 });
            Assert.AreEqual(expected, actual);

        }
    }

}