using System;
using R3;
using Zenject;

namespace Puzzle15
{
    public class CounterPresenter: IInitializable, IDisposable
    {
        private readonly Counter _model;
        private readonly SettableFieldView _view;

        private CompositeDisposable _disposables = new();

        public CounterPresenter(Counter model, SettableFieldView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize() => _model.Value.Subscribe(SetCounterValue).AddTo(_disposables);

        public void Dispose() => _disposables.Dispose();

        private void SetCounterValue(int value) => _view.SetValue(value.ToString());
    }
}
