using System;
using UnityEngine;

namespace Puzzle15
{
    public interface IGridModel
    {
        public event Action<int, int, int, int> OnCellSwapped;
        public event Action OnWin;

        public void ClickOnCell(int i, int j);
        public int[,] GetCells();

        public int this[int i, int j] { get; }
    }
}

