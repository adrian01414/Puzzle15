using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Puzzle15 {
    public class ChooseLevelConfigurationPanel : MonoBehaviour
    {
        [SerializeField] private ChooseLevelConfigurationButton _buttonPrefab;
        [SerializeField, Min(0)] private float _fontSize = 24f;

        private LevelConfigurationManager _configManager;
        private List<ChooseLevelConfigurationButton> _buttons = new();

        [Inject]
        public void Construct(LevelConfigurationManager configManager)
        {
            _configManager = configManager;
        }

        private void Awake()
        {
            foreach (LevelConfig config in _configManager.LevelConfigs)
            {
                ChooseLevelConfigurationButton button = Instantiate(_buttonPrefab, transform);
                var buttonRect = button.GetComponent<RectTransform>();

                buttonRect.sizeDelta = new Vector2(buttonRect.rect.width, 150);
                button.Initialize(config);
                button.Text.fontSize = _fontSize;

                button.OnClicked += ChooseDifficult;
                _buttons.Add(button);
            }
        }

        public void ChooseDifficult(LevelConfig config)
        {
            _configManager.CurrentConfig = config;
            SceneManager.LoadScene("Game"); // !!!!!
        }

        private void OnValidate()
        {
            if(_buttons != null)
            {
                foreach (var button in _buttons)
                {
                    button.Text.fontSize = _fontSize;
                }
            }    
        }

        private void OnDisable()
        {
            foreach(var button in _buttons)
            {
                button.OnClicked -= ChooseDifficult;
            }
        }
    }
}
