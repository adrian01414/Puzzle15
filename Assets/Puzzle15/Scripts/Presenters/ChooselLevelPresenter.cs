using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace Puzzle15
{
    public class ChooselLevelPresenter: IInitializable, IDisposable
    {
        private readonly LevelConfig _model;
        private readonly ChooseLevelView _view;

        public ChooselLevelPresenter(LevelConfig levelConfig, ChooseLevelView view) { 
            _model = levelConfig;
            _view = view;
        }

        public void Initialize() => _view.OnLevelChose += SetGridSize;

        private void SetGridSize(int gridSize)
        {
            _model.GridSize = gridSize;
            SceneManager.LoadScene("Game");
        }

        public void Dispose() => _view.OnLevelChose -= SetGridSize;
    }
}
