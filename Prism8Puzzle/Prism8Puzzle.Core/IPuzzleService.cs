using Business;
using System;

namespace Prism8Puzzle.Core
{
    public interface IPuzzleService
    {
        Board Board { get; set; }
        void GetRandomPuzzleAsync(EventHandler<TilesServiceResult> e);
        void GetSolutionPuzzleAsync(EventHandler<TilesServiceResult> e);
        void GetSolvedPuzzleAsync(EventHandler<NodeServiceResult> e);
    }
}
