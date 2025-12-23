using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CellView : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public event Action<CellView> OnCellClicked;

    [SerializeField] private TMP_Text _numberText;

    public RectTransform ParentRectTransform {  get; set; }
    public RectTransform RectTransform { get; private set; }

    private bool _isLeftMouseButtonPressed = false;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //
        {
            _isLeftMouseButtonPressed = true;
        }
        if (Input.GetMouseButtonUp(0)) //
        {
            _isLeftMouseButtonPressed = false;
        }
    }

    public void SetDigitText(string digitText)
    {
        _numberText.text = digitText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isLeftMouseButtonPressed = true;
            OnCellClicked?.Invoke(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isLeftMouseButtonPressed)
        {
            OnCellClicked?.Invoke(this);
        }
    }
}
