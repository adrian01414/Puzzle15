using System;
using Puzzle15;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseLevelButton : CustomButton
{
    public event Action<int> OnButtonClicked;

    [field: SerializeField] public int GridSize { get; private set; } = 4;

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (_isEntered)
            OnButtonClicked?.Invoke(GridSize);
    }
}
