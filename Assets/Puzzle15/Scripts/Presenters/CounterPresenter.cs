using System;
using R3;
using Zenject;

namespace Puzzle15
{
    public class CounterPresenter: IInitializable, IDisposable
    {
        private Counter _model;
        private ISettableFieldView _view;

        private CompositeDisposable _disposables = new();

        public CounterPresenter(Counter model, ISettableFieldView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _model.Value.Subscribe(SetCounterValue).AddTo(_disposables);
        }

        private void SetCounterValue(int value)
        {
            _view.SetValue(value.ToString());
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
