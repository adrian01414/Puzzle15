using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle15
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ChooseLevelView : MonoBehaviour
    {
        public event Action<int> OnLevelChose;

        [SerializeField] private GameObject _antiClicker;
        [SerializeField] private List<ChooseLevelButton> buttons;
        [SerializeField] private Button _exitButton;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private void OnEnable()
        {
            _antiClicker.SetActive(true);

            foreach (var button in buttons)
            {
                button.OnButtonClicked += SelectGridSize;
            }

            _exitButton.onClick.AddListener(Exit);
            ShowPanelAnimation();
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Exit()
        {
            _antiClicker.SetActive(false);
            gameObject.SetActive(false);
        }

        private void SelectGridSize(int gridSize)
        {
            OnLevelChose?.Invoke(gridSize);
        }

        private void ShowPanelAnimation()
        {
            _canvasGroup.alpha = 0;
            var animation = DOTween.Sequence();

            animation
                .Append(_canvasGroup.DOFade(1, 0.3f))
                .Join(_rectTransform.DOAnchorPos(Vector2.zero, 0.3f)
                    .From(_rectTransform.anchoredPosition - new Vector2(0, 100)))
                .Play();
        }

        private void OnDisable()
        {
            foreach (var button in buttons)
            {
                button.OnButtonClicked -= SelectGridSize;
            }
        }
    }
}
