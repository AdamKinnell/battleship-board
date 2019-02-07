using System;
using battleship_board;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace battleship_board_tests {

    [TestClass]
    public class BattleshipBoardTests {

        [TestMethod]
        public void Should_ThrowException_WhenConstructedWithInvalidDimensions() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BattleshipBoard(-1, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BattleshipBoard(1, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BattleshipBoard(0, 0));
        }

        [TestMethod]
        public void AddingBattleship_ShouldReturnFalse_WhenNotPlacedOnBoard() {
            var board = new BattleshipBoard(10, 10);

            Assert.IsFalse(board.AddBattleship( // Above
                new Battleship(1, 5),
                new Coord() { X = 0, Y = -1 }));
            Assert.IsFalse(board.AddBattleship( // Left
                new Battleship(1, 5),
                new Coord() { X = -5, Y = 0 }));
            Assert.IsFalse(board.AddBattleship( // Right
                new Battleship(1, 5),
                new Coord() { X = 10, Y = 0 }));
            Assert.IsFalse(board.AddBattleship( // Below
                new Battleship(1, 5),
                new Coord() { X = 0, Y = 10 }));
        }

        [TestMethod]
        public void AddingBattleship_ShouldReturnFalse_WhenPartiallyOffBoard() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsFalse(board.AddBattleship( // Off lower-right
                new Battleship(2, 2),
                new Coord() { X = 9, Y = 9 }));
            Assert.IsFalse(board.AddBattleship( // Off lower-left
                new Battleship(2, 2),
                new Coord() { X = -1, Y = 9 }));
        }

        [TestMethod]
        public void AddingBattleship_ShouldReturnTrue_WhenExactSizeOfBoard() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship(
                new Battleship(10, 10),
                new Coord() { X = 0, Y = 0 }));
        }

        [TestMethod]
        public void AddingBattleship_ShouldReturnTrue_WhenInLowerRightCorner() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship(
                new Battleship(2, 2),
                new Coord() { X = 8, Y = 8 }));
        }

        [TestMethod]
        public void AddingBattleshipTwice_ShouldThrowException() {
            var board = new BattleshipBoard(10, 10);
            var battleship = new Battleship(2, 2);
            board.AddBattleship(battleship, new Coord() { X = 0, Y = 0 });

            Assert.ThrowsException<ArgumentException>(() => // Same location
                board.AddBattleship(battleship, new Coord() { X = 0, Y = 0 }));
            Assert.ThrowsException<ArgumentException>(() => // Different location
                board.AddBattleship(battleship, new Coord() { X = 0, Y = 1 }));
        }

        [TestMethod]
        public void AddingBattleship_ShouldReturnFalse_WhenFootprintFullyOccupied() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship( // Add first
                new Battleship(4, 1),
                new Coord() { X = 2, Y = 2 }));
            Assert.IsFalse(board.AddBattleship( // Try add second; smaller & overlapping
                new Battleship(2, 1),
                new Coord() { X = 3, Y = 2 }));
        }

        [TestMethod]
        public void AddingBattleship_ShouldReturnFalse_WhenFootprintPartiallyOccupied() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship( // Add first
                new Battleship(4, 1),
                new Coord() { X = 2, Y = 2 }));
            Assert.IsFalse(board.AddBattleship( // Try add second; offset by 2
                new Battleship(4, 1),
                new Coord() { X = 4, Y = 2 }));
        }

        [TestMethod]
        public void AddingBattleship_ShouldReturnFalse_WhenOverlapPerpendicular() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship( // Add first
                new Battleship(4, 1),
                new Coord() { X = 0, Y = 3 }));
            Assert.IsFalse(board.AddBattleship( // Try add second; perpendicular overlap
                new Battleship(1, 4),
                new Coord() { X = 3, Y = 0 }));
        }

        [TestMethod]
        public void ReceivingAttack_WhenBoardEmpty_ResultsInMiss() {
            var board = new BattleshipBoard(3, 3);
            for (var x = 0; x < board.Width; x++)
            for (var y = 0; y < board.Height; y++) {
                Assert.IsFalse(board.ReceiveAttackAt(new Coord() { X = x, Y = y }));
            }
        }

        [TestMethod]
        public void ReceivingAttack_OnEmptyCell_ResultsInMiss() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship( // Add battleship
                new Battleship(2, 2),
                new Coord() { X = 3, Y = 3 }));

            Assert.IsFalse(board.ReceiveAttackAt( // Top-left of board
                new Coord() { X = 0, Y = 0 }));
            Assert.IsFalse(board.ReceiveAttackAt( // Top-right of board
                new Coord() { X = 9, Y = 0 }));
            Assert.IsFalse(board.ReceiveAttackAt( // Bottom-left of board
                new Coord() { X = 0, Y = 9 }));
            Assert.IsFalse(board.ReceiveAttackAt( // Bottom-right of board
                new Coord() { X = 9, Y = 9 }));

            Assert.IsFalse(board.ReceiveAttackAt( // Above battleship
                new Coord() { X = 4, Y = 2 }));
            Assert.IsFalse(board.ReceiveAttackAt( // Below battleship
                new Coord() { X = 4, Y = 5 }));
            Assert.IsFalse(board.ReceiveAttackAt( // Left of battleship
                new Coord() { X = 2, Y = 3 }));
            Assert.IsFalse(board.ReceiveAttackAt( // Right of battleship
                new Coord() { X = 7, Y = 3 }));
        }

        [TestMethod]
        public void ReceivingAttack_OnOccupiedCell_ResultsInHit() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship(
                new Battleship(2, 2),
                new Coord() { X = 3, Y = 3 }));

            Assert.IsTrue(board.ReceiveAttackAt( // 1st battleship cell
                new Coord() { X = 3, Y = 3 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 2nd battleship cell
                new Coord() { X = 4, Y = 3 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 3rd battleship cell
                new Coord() { X = 3, Y = 4 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 4th battleship cell
                new Coord() { X = 4, Y = 4 }));
        }

        [TestMethod]
        public void ReceivingAttack_OnOccupiedDamagedCell_ResultsInHit() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship(
                new Battleship(2, 2),
                new Coord() { X = 3, Y = 3 }));

            // First round: Cells previously undamaged
            Assert.IsTrue(board.ReceiveAttackAt( // 1st battleship cell
                new Coord() { X = 3, Y = 3 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 2nd battleship cell
                new Coord() { X = 4, Y = 3 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 3rd battleship cell
                new Coord() { X = 3, Y = 4 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 4th battleship cell
                new Coord() { X = 4, Y = 4 }));

            // Second round: Cells previously damaged
            Assert.IsTrue(board.ReceiveAttackAt( // 1st battleship cell
                new Coord() { X = 3, Y = 3 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 2nd battleship cell
                new Coord() { X = 4, Y = 3 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 3rd battleship cell
                new Coord() { X = 3, Y = 4 }));
            Assert.IsTrue(board.ReceiveAttackAt( // 4th battleship cell
                new Coord() { X = 4, Y = 4 }));
        }

        [TestMethod]
        public void HasBattleshipsRemaining_ReturnsFalse_WhenNoBattleships() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsFalse(board.HasBattleshipsRemaining());
        }

        [TestMethod]
        public void HasBattleshipsRemaining_ReturnsTrue_WhenOneUndamagedBattleship() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship(
                new Battleship(4, 1),
                new Coord() { X = 0, Y = 0 }));

            Assert.IsTrue(board.HasBattleshipsRemaining());
        }

        [TestMethod]
        public void HasBattleshipsRemaining_ReturnsTrue_UntilLoneBattleshipSunk() {
            var board = new BattleshipBoard(10, 10);
            Assert.IsTrue(board.AddBattleship(
                new Battleship(4, 1),
                new Coord() { X = 0, Y = 0 }));

            // Attack all cells of the lone battleship
            for (var i = 0; i < 4; i++) {
                Assert.IsTrue(board.HasBattleshipsRemaining());
                board.ReceiveAttackAt(new Coord() { X=i, Y=0 });
            }
            Assert.IsFalse(board.HasBattleshipsRemaining());
        }
    }

}