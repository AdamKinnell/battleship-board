using System.Collections.Generic;

namespace battleship_board {

    /// <summary>
    ///     Represents the location and footprint of a particular battleship
    ///     within a parent coordinate system.
    /// </summary>
    public class BattleshipLocation {

        // Properties /////////////////////////////////////

        /// <summary>
        ///     The battleship placed here.
        /// </summary>
        public Battleship Battleship { get; set; }

        /// <summary>
        ///     The parent coordinate of the top-left cell of the battleship.
        /// </summary>
        public Coord TopLeft { get; set; }

        // Functions //////////////////////////////////////

        /// <summary>
        ///     Iterate over each coordinate of the placement's footprint
        ///     in the parent coordinate system. Order is not guaranteed.
        /// </summary>
        /// <returns> An enumerable list of coordinates. </returns>
        public IEnumerable<Coord> IterateFootprint() {
            for (var x = 0; x < Battleship.Width; x++)
            for (var y = 0; y < Battleship.Height; y++)
                yield return new Coord() {
                    X = x + TopLeft.X,
                    Y = y + TopLeft.Y,
                };
        }

        /// <summary>
        ///     Convert a point in the parent coordinate system to
        ///     one in the battleship coordinate system.
        /// 
        ///     There is no guarantee that the coordinate refers
        ///     to a valid cell on the battleship. Only the translation is performed.
        /// 
        ///     When given the TopLeft point, (0,0) will be returned.
        /// </summary>
        /// <param name="coord"> A point in the parent coordinate system. </param>
        /// <returns> A point in the battleship coordinate system. </returns>
        public Coord ToBattleshipCoord(Coord coord) {
            return new Coord() {
                X = coord.X - TopLeft.X,
                Y = coord.Y - TopLeft.Y,
            };
        }
    }

}