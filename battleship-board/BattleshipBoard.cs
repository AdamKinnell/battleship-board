using System;
using System.Collections.Generic;
using System.Linq;

namespace battleship_board {

    /// <summary>
    ///     Represents the state of a single player's game board.
    /// 
    ///     A board is represented as a grid of cells on which
    ///     battleships can be placed. The top left is coordinate (0,0)
    /// 
    ///     A single battleship may occupy multiple cells,
    ///     though it may not overlap with another battleship and
    ///     must be contained entirely within the board.
    /// </summary>
    public class BattleshipBoard {

        /// <summary>
        ///     Construct a new board with the specified dimensions.
        /// </summary>
        /// <param name="width"> The width of the board, in cells </param>
        /// <param name="height"> The height of the board, in cells </param>
        public BattleshipBoard(int width = 10, int height = 10) {
            if (width < 1)
                throw new ArgumentOutOfRangeException(nameof(width),
                    "Must have positive width");
            if (height < 1)
                throw new ArgumentOutOfRangeException(nameof(height),
                    "Must have positive height");

            Width = width;
            Height = height;
            Cells = new BattleshipLocation[width, height];
            Battleships = new List<BattleshipLocation>();
        }

        // Properties /////////////////////////////////////

        /// <summary>
        ///     The width of the board, in cells.
        /// </summary>
        public int Width { get; }

        /// <summary>
        ///     The height of the board, in cells.
        /// </summary>
        public int Height { get; }

        /// <summary>
        ///     A 2D grid to record which cells are currently occupied
        ///     by the footprint of a battleship.
        ///     Accessed like: Cells[x,y]
        /// </summary>
        private BattleshipLocation[,] Cells { get; }

        /// <summary>
        ///     The battleships currently on the board, along with their locations.
        /// </summary>
        private List<BattleshipLocation> Battleships { get; }

        // Public Functions ///////////////////////////////

        /// <summary>
        ///     Add a new battleship to the board,
        ///     such that it's top-left cell is located at the specified coordinate.
        /// </summary>
        /// <param name="battleship"> The battleship to add to the board. </param>
        /// <param name="topLeft"> Where the battleship is to be placed. </param>
        /// <returns> </returns>
        public bool AddBattleship(Battleship battleship, Coord topLeft) {

            // Since battleships hold state, duplicate references can't be allowed
            if (Battleships.Any(b => b.Battleship == battleship))
                throw new ArgumentException(
                    "This battleship is already on the board",
                    nameof(battleship));

            // Validate location
            var location = new BattleshipLocation() { Battleship = battleship, TopLeft = topLeft };
            if (!FootprintFitsOnBoard(location)) return false;
            if (!FootprintUnoccupied(location)) return false;

            // Place battleship
            MarkFootprint(location);
            Battleships.Add(location);

            return true;
        }

        /// <summary>
        ///     Process an attack on a single board cell.
        /// 
        ///     If a battleship occupies that cell,
        ///     the corresponding part of that ship will be damaged.
        ///
        ///     Subsequent attacks on the same cell will count as hits,
        ///     but will not create any further damage.
        /// </summary>
        /// <param name="coord"> The coordinate of a cell on the board. </param>
        /// <returns> True if the attack hit a battleship, false if it missed. </returns>
        public bool ReceiveAttackAt(Coord coord) {

            // Find which battleship was hit, if any
            var location = GetBattleshipAt(coord);
            if (location == null) return false;

            // Damage the targeted cell of the battleship
            location.Battleship.InflictDamageAt(
                location.ToBattleshipCoord(coord)
            );

            return true;
        }

        /// <summary>
        ///     Check if there are any battleships remaining on the board which
        ///     are still afloat (not sunk).
        /// </summary>
        /// <returns></returns>
        public bool HasBattleshipsRemaining() {
            return Battleships.Any(l => !l.Battleship.IsSunk());
        }

        // Private Functions //////////////////////////////

        /// <summary>
        ///     Check if the entire footprint fits within the boundaries of the board.
        /// </summary>
        /// <param name="location"> The location to check the footprint of. </param>
        /// <returns> True if it fits, else false. </returns>
        private bool FootprintFitsOnBoard(BattleshipLocation location) {
            return location.TopLeft.X >= 0 &&
                   location.TopLeft.Y >= 0 &&
                   location.TopLeft.X + location.Battleship.Width <= Width &&
                   location.TopLeft.Y + location.Battleship.Height <= Height;
        }

        /// <summary>
        ///     Check if the entire footprint of the given location is unoccupied.
        /// </summary>
        /// <param name="location"> The location to check the footprint of. </param>
        /// <returns> True if its footprint is clear, else false. </returns>
        private bool FootprintUnoccupied(BattleshipLocation location) {
            return location.IterateFootprint()
                .All(coord => Cells[coord.X, coord.Y] == null);
        }

        /// <summary>
        ///     Mark the footprint of the given battleship location as occupied.
        /// </summary>
        /// <param name="location"> The location to mark the footprint of. </param>
        private void MarkFootprint(BattleshipLocation location) {
            foreach (var coord in location.IterateFootprint())
                Cells[coord.X, coord.Y] = location;
        }

        /// <summary>
        ///     Return information about the battleship at the given location, if one exists.
        /// </summary>
        /// <param name="coord"> The coordinate of a cell on the board. </param>
        /// <returns> Information about the battleship, or null. </returns>
        private BattleshipLocation GetBattleshipAt(Coord coord) {
            return Cells[coord.X, coord.Y];
        }
    }

}