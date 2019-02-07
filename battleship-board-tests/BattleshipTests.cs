using System;
using battleship_board;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace battleship_board_tests {

    [TestClass]
    public class BattleshipTests {

        [TestMethod]
        public void Should_ThrowException_WhenConstructedWithInvalidDimensions() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new Battleship(-1, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new Battleship(0, 0));
        }

        [TestMethod]
        public void Should_BeUndamaged_WhenConstructed() {
            var battleship = new Battleship(4, 1);
            for (var i = 0; i < 4; i++) {
                Assert.IsFalse(battleship.IsDamagedAt(new Coord { X = i, Y = 0 }));
            }
        }

        [TestMethod]
        public void Should_Sink_OnlyWhenAllCellsDamaged() {
            var battleship = new Battleship(4, 1);
            for (var i = 0; i < 4; i++) {
                Assert.IsFalse(battleship.IsSunk());
                battleship.InflictDamageAt(new Coord { X = i, Y = 0 });
            }
            Assert.IsTrue(battleship.IsSunk());
        }

        [TestMethod]
        public void Should_NotSink_WhenOneCellRepeatedlyDamaged() {
            var battleship = new Battleship(4, 1);
            for (var i = 0; i < 4; i++) {
                Assert.IsFalse(battleship.IsSunk());
                battleship.InflictDamageAt(new Coord { X = 0, Y = 0 });
            }
            Assert.IsFalse(battleship.IsSunk());
        }

        [TestMethod]
        public void IsDamagedAt_WhenCalledWithInvalidCoordinate_ThrowsException() {
            var battleship = new Battleship(2, 2);
            Assert.ThrowsException<IndexOutOfRangeException>( // Before
                () => battleship.IsDamagedAt(new Coord { X = -1, Y = -1 }));
            Assert.ThrowsException<IndexOutOfRangeException>( // Off row
                () => battleship.IsDamagedAt(new Coord { X = 2, Y = 1 }));
            Assert.ThrowsException<IndexOutOfRangeException>( // After
                () => battleship.IsDamagedAt(new Coord { X = 2, Y = 2 }));
        }

        [TestMethod]
        public void InflictDamageAt_WhenCalledWithInvalidCoordinate_ThrowsException() {
            var battleship = new Battleship(2, 2);
            Assert.ThrowsException<IndexOutOfRangeException>( // Before
                () => battleship.InflictDamageAt(new Coord { X = -1, Y = -1 }));
            Assert.ThrowsException<IndexOutOfRangeException>( // Off row
                () => battleship.InflictDamageAt(new Coord { X = 2, Y = 1 }));
            Assert.ThrowsException<IndexOutOfRangeException>( // After
                () => battleship.InflictDamageAt(new Coord { X = 2, Y = 2 }));
        }
    }

}