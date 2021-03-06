﻿using System;
using System.Linq;

namespace battleship_board {

    /// <summary>
    ///     Represents the state of a single battleship.
    /// 
    ///     A battleship is represented as a grid of cells,
    ///     each of which have their own damage status.
    /// </summary>
    public class Battleship {

        /// <summary>
        ///     Construct a new battleship with the specified dimensions.
        /// </summary>
        /// <param name="width"> The width of the battleship, in cells </param>
        /// <param name="height"> The height of the battleship, in cells </param>
        public Battleship(int width, int height) {
            if (width < 1)
                throw new ArgumentOutOfRangeException(nameof(width),
                    "Must have positive width");
            if (height < 1)
                throw new ArgumentOutOfRangeException(nameof(height),
                    "Must have positive height");

            Width = width;
            Height = height;
            Cells = new CellState[width, height];
        }

        // Properties /////////////////////////////////////

        /// <summary>
        ///     The width of the battleship, in cells.
        /// </summary>
        public int Width { get; }

        /// <summary>
        ///     The height of the battleship, in cells.
        /// </summary>
        public int Height { get; }

        /// <summary>
        ///     The state of damage to the battleship.
        ///     Accessed like: Cells[x,y]
        /// </summary>
        private CellState[,] Cells { get; }

        // Functions //////////////////////////////////////

        /// <summary>
        ///     Check if the specified cell of this battleship is damaged.
        /// </summary>
        /// <param name="coord"> The 0-based coordinate referring to a valid cell </param>
        /// <returns> True if damaged, else false </returns>
        public bool IsDamagedAt(Coord coord) {
            return Cells[coord.X, coord.Y] == CellState.Damaged;
        }

        /// <summary>
        ///     Inflict damage on the specified cell of this battleship.
        /// </summary>
        /// <param name="coord"> The 0-based coordinate referring to a valid cell </param>
        public void InflictDamageAt(Coord coord) {
            Cells[coord.X, coord.Y] = CellState.Damaged;
        }

        /// <summary>
        ///     Check if all cells on this battleship have been damaged.
        /// </summary>
        /// <returns> True if sunk, else false </returns>
        public bool IsSunk() {
            return Cells.Cast<CellState>()
                .All(x => x == CellState.Damaged);
        }

        // Types //////////////////////////////////////////

        /// <summary>
        ///     The possible states of each Battleship cell.
        /// </summary>
        private enum CellState {
            Undamaged,
            Damaged
        }
    }

}