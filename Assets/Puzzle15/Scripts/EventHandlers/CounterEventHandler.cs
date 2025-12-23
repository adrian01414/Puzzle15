using System;
using Zenject;

namespace Puzzle15
{
    public class CounterEventHandler: IInitializable, IDisposable
    {
        private PuzzleGrid _grid;
        private Counter _counter;

        public CounterEventHandler(PuzzleGrid gridModel, Counter counter)
        {
            _grid = gridModel;
            _counter = counter;
        }

        public void Initialize()
        {
            _grid.OnCellMoved += IncreaseCounter;
            _grid.OnInitialized += ResetCounter;
        }

        public void IncreaseCounter(CellMoveData cellSwapData) => _counter.Increase();

        public void ResetCounter() => _counter.Reset();

        public void Dispose()
        {
            _grid.OnCellMoved -= IncreaseCounter;
            _grid.OnInitialized -= ResetCounter;
        }
    }
}
