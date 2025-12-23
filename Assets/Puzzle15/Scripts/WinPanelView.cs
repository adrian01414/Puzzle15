using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Puzzle15
{
    public class WinPanelView : MonoBehaviour
    {
        public event UnityAction OnRestartButtonClicked
        {
            add { _restartButton.onClick.AddListener(value); }
            remove { _restartButton.onClick.RemoveListener(value); }
        }

        [SerializeField] private TMP_Text _timeOutputText;
        [SerializeField] private TMP_Text _movesOutputText;
        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            GetComponent<RectTransform>().DOShakeScale(0.3f, 0.2f);
        }

        public void SetTimeText(string text)
        {
            _timeOutputText.text = text;
        }

        public void SetMovesText(string text)
        {
            _movesOutputText.text = text;
        }
    }

}
