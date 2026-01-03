using System;
using Zenject;

namespace Puzzle15
{
    public class WinPanelPresenter: IInitializable, IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private readonly Counter _counter;
        private readonly PuzzleGrid _grid;
        private readonly WinPanelView _view;

        public WinPanelPresenter(Stopwatch stopwatch, Counter counter, PuzzleGrid grid, WinPanelView view)
        {
            _stopwatch = stopwatch;
            _counter = counter;
            _grid = grid;
            _view = view;
        }

        public void Initialize()
        {
            _grid.OnWon += ShowPanel;
            _view.OnRestartButtonClicked += RestartLevel;

            _view.gameObject.SetActive(false);
        }

        public void ShowPanel() {
            _view.gameObject.SetActive(true);
            _view.SetTimeText($"{_stopwatch.Time.CurrentValue.ToString("F2")}s");
            _view.SetMovesText(_counter.Value.ToString());
        }

        public void RestartLevel()
        {
            _view.gameObject.SetActive(false);
            _grid.Initialize();
        }

        public void Dispose()
        {
            _grid.OnWon -= ShowPanel;
            _view.OnRestartButtonClicked -= RestartLevel;
        }
    }
}
