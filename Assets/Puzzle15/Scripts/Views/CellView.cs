using System;
using Puzzle15;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CellView : MonoBehaviour, IPointerDownHandler
{
    public event Action<CellView> OnCellClicked;

    [SerializeField] private TMP_Text _numberText;

    public RectTransform ParentRectTransform {  get; set; }
    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void SetDigitText(string digitText)
    {
        _numberText.text = digitText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnCellClicked?.Invoke(this);
        }
    }
}
