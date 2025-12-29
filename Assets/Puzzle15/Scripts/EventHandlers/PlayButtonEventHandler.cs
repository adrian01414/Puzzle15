using System;
using Zenject;

namespace Puzzle15
{
    public class PlayButtonEventHandler: IInitializable, IDisposable
    {
        private readonly PlayButton _playButton;
        private readonly ChooseLevelView _view;

        public PlayButtonEventHandler(PlayButton playButton, ChooseLevelView view)
        {
            _playButton = playButton;
            _view = view;
        }

        public void Initialize() => _playButton.OnButtonUp += ShowChooseLevelPanel;

        private void ShowChooseLevelPanel() => _view.gameObject.SetActive(true);

        public void Dispose() => _playButton.OnButtonUp -= ShowChooseLevelPanel;
    }
}
