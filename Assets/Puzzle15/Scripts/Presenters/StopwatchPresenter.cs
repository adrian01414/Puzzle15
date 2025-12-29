using System;
using R3;
using Zenject;

namespace Puzzle15
{
    public class StopwatchPresenter: IInitializable, IDisposable
    {
        private readonly Stopwatch _model;
        private readonly ISettableFieldView _view;

        private CompositeDisposable _disposables = new();

        public StopwatchPresenter(Stopwatch model, ISettableFieldView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize() => _model.Time.Subscribe(SetTime).AddTo(_disposables);

        public void Dispose() => _disposables.Dispose();

        private void SetTime(float value) => _view.SetValue($"{value.ToString("F2")}s");
    }
}

