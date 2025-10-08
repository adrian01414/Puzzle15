using System;
using System.Text;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Puzzle15
{
    public sealed class Grid : IGridModel
    {
        public event Action<int, int, int, int> OnCellSwapped = null;
        public event Action OnWin = null;

        private readonly int _gridSize = 0;
        private int[,] _cells = null;

        public Grid(int gridSize, int[,] grid = null)
        {
            if (grid == null)
            {
                _cells = new int[gridSize, gridSize];
                _gridSize = gridSize;
                InitializeGrid();
            }
            else
            {
                _cells = (int[,])grid.Clone();
                _gridSize = gridSize;
                CheckGridValid();
            }
        }

        public void ClickOnCell(int i, int j)
        {
            if (i > 0 && _cells[i - 1, j] == 0) { SwapCells(i, j, i - 1, j); }
            else if (i < _gridSize - 1 && _cells[i + 1, j] == 0) { SwapCells(i, j, i + 1, j); }
            else if (j > 0 && _cells[i, j - 1] == 0) { SwapCells(i, j, i, j - 1); }
            else if (j < _gridSize - 1 && _cells[i, j + 1] == 0) { SwapCells(i, j, i, j + 1); }
            if (CheckWin()) OnWin?.Invoke();
        }

        public int[,] GetCells()
        {
            return (int[,])_cells.Clone();
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

        private void InitializeGrid()
        {
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    _cells[i, j] = j + (i * _gridSize);
                }
            }
            do
            {
                System.Random rng = new System.Random();

                for (int i = _gridSize * _gridSize - 1; i > 0; i--)
                {
                    int j = rng.Next(i + 1);

                    int currentRow = i / _gridSize;
                    int currentCol = i % _gridSize;
                    int randomRow = j / _gridSize;
                    int randomCol = j % _gridSize;

                    (_cells[randomRow, randomCol], _cells[currentRow, currentCol]) = (_cells[currentRow, currentCol], _cells[randomRow, randomCol]);
                }
            }
            while (CheckWin());
        }

        private bool CheckWin()
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

        private void CheckGridValid()
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
                if (grid[i] != i) throw new Exception("Grid not valid!");
            }
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