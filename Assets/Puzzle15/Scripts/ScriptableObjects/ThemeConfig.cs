using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "ThemeConfig", menuName = "Scriptable Objects/ThemeConfig")]
public class ThemeConfig : ScriptableObject
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _cellViewPrefab;

    // public getters
    public GameObject CellViewPrefab => _cellViewPrefab;
}
