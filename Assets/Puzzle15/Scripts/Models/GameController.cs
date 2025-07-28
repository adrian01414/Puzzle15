using System;

public class GameController
{
    public event Action<int, int, int, int> OnCellSwapped = null;
    public event Action OnWin = null;

    private int[,] _grid = null;
    private int _gridSize = 0;

    public GameController(int gridSize)
    {
        _grid = new int[gridSize, gridSize];
        _gridSize = gridSize;
        InitializeGrid();
    }

    public void ClickOnCell(int i, int j){
        if (_grid[i - 1, j] == 0 && i > 0) SwapCells(i, j, i - 1, j);
        if (_grid[i + 1, j] == 0 && i < _gridSize) SwapCells(i, j, i + 1, j);
        if (_grid[i, j - 1] == 0 && j > 0) SwapCells(i, j, i, j - 1);
        if (_grid[i, j + 1] == 0 && j < _gridSize) SwapCells(i, j, i, j + 1);
        if (CheckWin()) OnWin?.Invoke();
    }

    public void SwapCells(int i, int j, int ni, int nj)
    {
        int temp = _grid[i, j];
        _grid[i, j] = _grid[ni, nj];
        _grid[ni, nj] = temp;

        OnCellSwapped?.Invoke(i, j, ni, nj);
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < _gridSize; i++)
        {
            for (int j = 0; j < _gridSize; j++)
            {
                _grid[i, j] = j + (i * _gridSize);
            }
        }
        //shuffle(if(win) shuffle)
    }

    private bool CheckWin()
    {
        // check
        return false;
    }
}
