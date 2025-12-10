using System;
using System.Text;
using UnityEngine;

namespace Puzzle15
{
    public sealed class Grid
    {
        public event Action<int, int, int, int> OnCellSwapped = null;
        public event Action OnWin = null;

        private readonly int _gridSize = 0;
        private int[,] _cells = null;

        public Grid(IGridGenerator gridGenerator, int gridSize)
        {
            if (gridSize < 3)
            {
                throw new Exception("Grid size must be more then 3!");
            }
            //_cells = new int[gridSize, gridSize];
            _gridSize = gridSize;
            _cells = gridGenerator.Generate(gridSize);
            if(!IsGridValid())
            {
                throw new Exception("Generator returns not valid grid!");
            }
            //InitializeGrid();
        }

        public void ClickOnCell(int i, int j)
        {
            if (i > 0 && _cells[i - 1, j] == 0) { SwapCells(i, j, i - 1, j); }
            else if (i < _gridSize - 1 && _cells[i + 1, j] == 0) { SwapCells(i, j, i + 1, j); }
            else if (j > 0 && _cells[i, j - 1] == 0) { SwapCells(i, j, i, j - 1); }
            else if (j < _gridSize - 1 && _cells[i, j + 1] == 0) { SwapCells(i, j, i, j + 1); }

            if (IsWin()) OnWin?.Invoke();
        }

        public int[,] GetCells()
        {
            return (int[,])_cells.Clone();
        }

        public int GetGridSize()
        {
            return _gridSize;
        }

        public int this[int i, int j]
        {
            get => _cells[i, j];
        }

        private void SwapCells(int i, int j, int ni, int nj)
        {
            (_cells[ni, nj], _cells[i, j]) = (_cells[i, j], _cells[ni, nj]);
            OnCellSwapped?.Invoke(i, j, ni, nj);
        }

        private bool IsWin()
        {
            for (int i = _gridSize - 1; i > 0; i--)
            {
                for (int j = _gridSize - 1; j > 0; j--)
                {
                    if (i == _gridSize - 1) j--;
                    if (_cells[i, j] != j + (i * _gridSize) + 1) return false;
                }
            }
            return true;
        }

        private bool IsGridValid()
        {
            int[] grid = new int[_gridSize * _gridSize];
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    grid[j + (i * _gridSize)] = _cells[i, j];
                }
            }
            Array.Sort(grid);

            for (int i = 0; i < _gridSize * _gridSize; i++)
            {
                if (grid[i] != i) return false;
            }
            return true;
        }

        private void DebugGrid()
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    result.Append(_cells[i, j] + " ");
                }
                result.Append("\n");
            }
            Debug.Log(result.ToString());
        }
    }
}