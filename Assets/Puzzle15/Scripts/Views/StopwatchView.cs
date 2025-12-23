using UnityEngine;

namespace Puzzle15
{
    public class StopwatchView : SettableFieldView
    {
        [SerializeField] private float _updateInterval = 0.04f;

        private float _timeCounter = 0f;

        private bool _isSet = true;

        private string _currentValue = "0";

        public override void SetValue(string value)
        {
            _currentValue = value;
        }

        private void Update()
        {
            if (_isSet)
            {
                if (!_outputText.text.Equals(_currentValue))
                {
                    base.SetValue(_currentValue);
                    _isSet = false;
                }
            } else
            {
                if (_timeCounter >= _updateInterval)
                {
                    _isSet = true;
                    _timeCounter = 0f;
                }
                _timeCounter += Time.deltaTime;
            }

            
        }
    }
}
