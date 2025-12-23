using System;
using Zenject;

namespace Puzzle15
{
    public class StopwatchEventHandler: IInitializable, IDisposable
    {
        private readonly PuzzleGrid _grid;
        private readonly Stopwatch _stopwatch;

        public StopwatchEventHandler(PuzzleGrid grid, Stopwatch stopwatch)
        {
            _grid = grid;
            _stopwatch = stopwatch;
        }

        public void Initialize()
        {
            _grid.OnCellMoved += StartStopwatch;
            _grid.OnInitialized += ResetStopwatch;
            _grid.OnWon += StopStopwatch;
        }

        public void Dispose()
        {
            _grid.OnCellMoved -= StartStopwatch;
            _grid.OnInitialized -= ResetStopwatch;
            _grid.OnWon -= StopStopwatch;
        }

        private void StartStopwatch(CellMoveData cellSwapData)
        {
            _stopwatch.Start();
        }

        private void StopStopwatch() {
            _stopwatch.Stop();
        }

        private void ResetStopwatch()
        {
            _stopwatch.Stop();
            _stopwatch.Reset();
        }
    }
}
