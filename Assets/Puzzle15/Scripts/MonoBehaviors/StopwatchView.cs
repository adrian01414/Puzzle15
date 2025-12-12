using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Puzzle15
{
    public class StopwatchView : MonoBehaviour, ISettableFieldView
    {
        [SerializeField] private TextMeshProUGUI _stopwatchOutputText = null;

        [SerializeField] private float _updateInterval = 0.04f;

        private float timeCounter = 0f;

        public void UpdateStopwatch(float time)
        {
            if (timeCounter >= _updateInterval)
            {
                SetValue(time.ToString("F2") + "s");
                timeCounter = 0f;
            }
            timeCounter += Time.deltaTime;
        }

        public void SetValue(string value)
        {
            _stopwatchOutputText.text = value;
        }
    }
}
