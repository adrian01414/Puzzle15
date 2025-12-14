using Zenject;

namespace Puzzle15
{
    public class StopWatchEventHandler: IInitializable
    {
        private PuzzleGrid _grid;
        private Stopwatch _stopwatch;

        public StopWatchEventHandler(PuzzleGrid grid, Stopwatch stopwatch)
        {
            _grid = grid;
            _stopwatch = stopwatch;
        }

        public void Initialize()
        {
            _grid.OnCellMoved += StartStopWatch;
            _grid.OnWon += StopStopwatch;
        }

        private void StartStopWatch(CellMoveData cellSwapData)
        {
            _stopwatch.Start();
            _grid.OnCellMoved -= StartStopWatch;
        }

        private void StopStopwatch() {
            _stopwatch.Stop();
            _grid.OnWon -= StopStopwatch;
        }
    }
}
