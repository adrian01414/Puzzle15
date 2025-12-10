using TMPro;
using UnityEngine.Events;

internal interface IButton
{
    public event UnityAction OnClicked;
    public TMP_Text Text { get; }
}