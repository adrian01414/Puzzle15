using Zenject;

namespace Puzzle15
{
    public class CounterEventHandler: IInitializable
    {
        private Grid _gridModel;
        private Counter _counter;

        public CounterEventHandler(Grid gridModel, Counter counter)
        {
            _gridModel = gridModel;
            _counter = counter;
        }

        public void Initialize() => _gridModel.OnCellSwapped += IncreaseCounter;

        public void IncreaseCounter() => _counter.Increase();
    }
}
