using R3;
using Zenject;

namespace Puzzle15
{
    public class Stopwatch : ITickable
    {
        public ReadOnlyReactiveProperty<float> Time => _time;

        private ReactiveProperty<float> _time = new(0f);
        private bool _isStopped = true;

        public void Tick() { if(!_isStopped) _time.Value += UnityEngine.Time.deltaTime; }

        public void Start() => _isStopped = false;

        public void Stop() => _isStopped = true;

        public void Reset() => _time.Value = 0f;
    }
}