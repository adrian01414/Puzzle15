using System;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class MonoCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterOutputText = null;

        private Counter _counter = null;

        [Inject]
        public void Construct(Counter counter)
        {
            _counter = counter;
        }

        private void OnEnable()
        {
            _counter.Value.Subscribe(SetValue);
        }

        private void SetValue(int value)
        {
            _counterOutputText.text = value.ToString();
        }
    }
}
