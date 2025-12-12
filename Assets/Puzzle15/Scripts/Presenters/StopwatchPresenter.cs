using System.Diagnostics;

namespace Puzzle15
{
    public class StopwatchPresenter
    {
        private Stopwatch _model;
        private ISettableFieldView _view;

        public StopwatchPresenter(Stopwatch model, ISettableFieldView view)
        {
            _model = model;
            _view = view;
        }
    }
}

