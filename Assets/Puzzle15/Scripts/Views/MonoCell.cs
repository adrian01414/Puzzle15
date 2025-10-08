using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class MonoCell : MonoBehaviour, IPointerDownHandler
{
    public event Action<int, int> OnCellClicked = null;

    [SerializeField] private TextMeshProUGUI _digitText = null;

    private int _i = 0;
    private int _j = 0;

    public void Initialize(int digit, int i, int j)
    {
        _digitText.text = digit.ToString();
        _i = i;
        _j = j;
    }

    public void SetPosition(int i, int j)
    {
        _i = i;
        _j = j;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnCellClicked?.Invoke(_i, _j);
        }
    }
}
