using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellView : MonoBehaviour, IPointerDownHandler
{
    public event Action<CellView> OnCellClicked;

    public RectTransform ParentRectTransform {  get; set; }
    public RectTransform RectTransform { get; private set; }

    [SerializeField] private TextMeshProUGUI _digitText;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void SetDigitText(string digitText)
    {
        _digitText.text = digitText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnCellClicked?.Invoke(this);
        }
    }
}
