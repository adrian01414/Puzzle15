using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class CounterView : MonoBehaviour, ISettableFieldView
    {
        [SerializeField] private TMP_Text _counterOutputText = null;

        public void SetValue(string value)
        {
            _counterOutputText.text = value;
        }
    }
}
