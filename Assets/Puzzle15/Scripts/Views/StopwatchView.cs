using TMPro;
using UnityEngine;

namespace Puzzle15
{
    public class StopwatchView : MonoBehaviour, ISettableFieldView
    {
        [SerializeField] private TextMeshProUGUI _stopwatchOutputText = null;
        [SerializeField] private float _updateInterval = 0.04f;

        private bool _isCanUpdate = false;
        private float _timeCounter = 0f;

        private void Update()
        {
            if (_timeCounter >= _updateInterval)
            {
                _isCanUpdate = true;
            }
            _timeCounter += Time.deltaTime;
        }

        public void SetValue(string value)
        {
            if (_isCanUpdate)
            {
                _stopwatchOutputText.text = value;
                _timeCounter = 0f;
                _isCanUpdate = false;
            }
        }
    }
}
