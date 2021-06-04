using Business;
using System.Collections.Generic;

namespace Prism8Puzzle.Core
{
    public class TilesServiceResult
    {
        public TilesServiceResult(IList<Tile> result)
        {
            Result = result;
        }

        public IList<Tile> Result { get; }
    }
}
