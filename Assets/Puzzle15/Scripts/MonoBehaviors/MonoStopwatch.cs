using TMPro;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class MonoStopwatch : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _stopwatchOutputText = null;

        [SerializeField] private float _updateInterval = 0.04f;

        private Stopwatch _stopWatch = null;

        [Inject]
        public void Construct(Stopwatch stopWatch)
        {
            _stopWatch = stopWatch;
        }

        private void OnEnable()
        {
            _stopWatch.OnUpdate += UpdateStopwatch;
            _stopWatch.OnStop += UpdateStopwatchText;
            _stopWatch.OnReset += UpdateStopwatchText;
        }

        private float timeCounter = 0f;
        public void UpdateStopwatch(float deltaTime)
        {
            if (timeCounter >= _updateInterval)
            {
                UpdateStopwatchText();
                timeCounter = 0f;
            }
            timeCounter += deltaTime;
        }

        public void UpdateStopwatchText()
        {
            _stopwatchOutputText.text = _stopWatch.Time.ToString("F2") + "s";
        }
    }
}
