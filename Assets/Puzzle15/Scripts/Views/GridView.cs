using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class GridView : MonoBehaviour, IGridView
{
    public event Action<int, int> OnCellClicked = null;

    [SerializeField] private Transform _cellsParentTransfom = null;

    private MonoCell[,] _cells = null;

    void IGridView.Initialize(int size, int[,] values, GameObject cellPrefab)
    {
        var cellsRect = GetComponent<RectTransform>();
        _cells = new MonoCell[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var cell = Instantiate(cellPrefab, _cellsParentTransfom);
                var cellRect = cell.GetComponent<RectTransform>();
                cellRect.sizeDelta = new Vector2(cellsRect.rect.width / size,
                                                 cellsRect.rect.height / size);
                cellRect.anchorMin = new Vector2(0, 1);
                cellRect.anchorMax = new Vector2(0, 1);
                cellRect.anchoredPosition = new Vector2(j * cellRect.rect.width + cellRect.rect.width / 2,
                                                        -i * cellRect.rect.height - cellRect.rect.height / 2);
                cell.name = "Cell [" + i + ", " + j + "]";
                var monoCell = cell.GetComponent<MonoCell>();
                _cells[i, j] = monoCell;
                monoCell.Initialize(values[i, j], i, j);
            }
        }

        foreach (var cell in _cells)
        {
            cell.OnCellClicked += OnCellClicked;
        }
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
        {
            cell.OnCellClicked -= OnCellClicked;
        }
    }
}
