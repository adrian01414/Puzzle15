using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChooseLevelConfigurationButton : MonoBehaviour, IButton
{
    public event UnityAction OnClicked
    {
        add { _button.onClick.AddListener(value); }
        remove { _button.onClick.RemoveListener(value); }
    }

    [field: SerializeField] public TMP_Text Text { get; private set; }

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
}