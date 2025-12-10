using System;

namespace Puzzle15
{
    public class Stopwatch
    {
        public event Action OnStart = null;
        public event Action OnStop = null;
        public event Action OnRestart = null;
        public event Action OnReset = null;
        public event Action<float> OnUpdate = null;

        private float _time = 0f;
        private bool _isStopped = true;

        public float Time => _time;

        public Stopwatch()
        {
            _time = 0f;
            _isStopped = true;
        }

        public void Update(float deltaTime)
        {
            if(!_isStopped)
            {
                _time += deltaTime;
                OnUpdate?.Invoke(deltaTime);
            }
        }

        public void Start()
        {
            _isStopped = false;
            OnStart?.Invoke();
        }

        public void Stop()
        {
            _isStopped = true;
            OnStop?.Invoke();
        }

        public void Restart()
        {
            _time = 0f;
            Start();
            OnRestart?.Invoke();
        }

        public void Reset()
        {
            _time = 0f;
            OnReset?.Invoke();
        }
    }
}