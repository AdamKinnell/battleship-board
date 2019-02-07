namespace battleship_board {

    /// <summary>
    ///     Represents a 0-based 2D coordinate.
    /// </summary>
    public struct Coord {

        /// <summary>
        ///     The 0-based x coordinate along the row.
        ///     Corresponds to the dimension of width.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     The 0-based x coordinate along the column.
        ///     Corresponds to the dimension of height.
        /// </summary>
        public int Y { get; set; }

    }

}