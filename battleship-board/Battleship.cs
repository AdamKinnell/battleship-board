﻿using System;
using System.Diagnostics.Contracts;

namespace battleship_board {

    /// <summary>
    ///     Represents the state of a single battleship.
    ///     Each battleship is represented as a rectangle made up of individual cells.
    /// </summary>
    public class Battleship {

        public Battleship(int width, int height) {
            if (width < 1)
                throw new ArgumentOutOfRangeException(nameof(width),
                    "Must have non-negative dimensions");
            if (height < 1)
                throw new ArgumentOutOfRangeException(nameof(height),
                    "Must have non-negative dimensions");

            this.Width = width;
            this.Height = height;
            this.Cells = new CellState[width, height];
        }

        // Fields /////////////////////////////////////////

        public int Width { get; }

        public int Height { get; }

        private CellState[,] Cells { get; }

        // Functions //////////////////////////////////////

        public bool IsDamagedAt(Coord coord) {
            if (!this.IsWithinBounds(coord))
                throw new ArgumentOutOfRangeException(nameof(coord),
                    "Coordinate must refer to a valid cell");

            return this.Cells[coord.X, coord.Y] == CellState.Damaged;
        }

        public void InflictDamageAt(Coord coord) {
            if (!this.IsWithinBounds(coord))
                throw new ArgumentOutOfRangeException(nameof(coord),
                    "Coordinate must refer to a valid cell");

            this.Cells[coord.X, coord.Y] = CellState.Damaged;
        }

        private bool IsWithinBounds(Coord coord) {
            return coord.X >= 0 && coord.X < this.Width &&
                   coord.Y >= 0 && coord.Y < this.Height;
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