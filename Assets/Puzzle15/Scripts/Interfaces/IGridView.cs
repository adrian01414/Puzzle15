using System;
using UnityEngine;

public interface IGridView
{
    public event Action<int, int> OnCellClicked;

    public void Initialize(int size, int[,] values, GameObject monoCellPrefab);
}
