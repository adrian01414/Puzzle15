using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChooseLevelConfigurationButton : MonoBehaviour
{
    public event Action<LevelConfig> OnClicked;

    [field: SerializeField] public TMP_Text Text { get; private set; }

    public LevelConfig LevelConfig { get; private set; }

    private Button _button;

    public void Initialize(LevelConfig levelConfig)
    {
        LevelConfig = levelConfig;
        Text.text = LevelConfig.Size + "x" + LevelConfig.Size;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClickButton);
    }

    public void ClickButton()
    {
        OnClicked?.Invoke(LevelConfig);
    }
}