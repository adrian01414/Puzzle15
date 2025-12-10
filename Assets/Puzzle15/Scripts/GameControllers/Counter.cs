using R3;

namespace Puzzle15
{
    sealed public class Counter
    {
        public Observable<int> Value => _value;

        private ReactiveProperty<int> _value = new(0);

        public void Increase() => _value.Value++;

        public void Reset() => _value.Value = 0;
    }
}
