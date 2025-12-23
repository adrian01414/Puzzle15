using TMPro;
using UnityEngine;

namespace Puzzle15
{
    public abstract class SettableFieldView : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _outputText = null;

        private void Awake()
        {
            GetComponent<RectTransform>().SetStretch();
        }

        public virtual void SetValue(string value) => _outputText.text = value;
    }
}