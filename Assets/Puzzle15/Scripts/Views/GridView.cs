using System;
using UnityEngine;

public sealed class GridView : MonoBehaviour
{
    public event Action<int, int> OnCellClicked = null;

    [SerializeField] private Transform _cellsParentTransfom = null;
    [SerializeField] private GameObject _cellPrefab = null;

    private MonoCell[,] _cells = null;

    public void Initialize(int size, int[,] values)
    {
        var cellsRect = GetComponent<RectTransform>();
        _cells = new MonoCell[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var cell = Instantiate(_cellPrefab, _cellsParentTransfom);
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

                monoCell.Construct(values[i, j], i, j);
                if (values[i, j] == 0)
                {
                    monoCell.DisableCell();
                }
            }
        }

        foreach (var cell in _cells)
        {
            cell.OnCellClicked += ClickOnCell;
        }
    }


    public void SwapCells(int i, int j, int ni, int nj)
    {
        _cells[i, j].SetPosition(ni, nj);
        _cells[ni, nj].SetPosition(i, j);
        (_cells[i, j], _cells[ni, nj]) = (_cells[ni, nj], _cells[i, j]);

        var temp = _cells[i, j].transform.position;
        _cells[i, j].transform.position = new Vector3(_cells[ni, nj].transform.position.x,
                                                      _cells[ni, nj].transform.position.y,
                                                      _cells[ni, nj].transform.position.z);
        _cells[ni, nj].transform.position = new Vector3(temp.x,
                                                        temp.y,
                                                        temp.z);
    }

    private void ClickOnCell(int i, int j)
    {
        OnCellClicked?.Invoke(i, j);
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
        {
            if(cell != null)
            {
                cell.OnCellClicked -= OnCellClicked;
            }
        }
    }
}
