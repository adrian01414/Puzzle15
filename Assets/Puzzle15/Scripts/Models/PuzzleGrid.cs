using System;

namespace Puzzle15
{
    public class PuzzleGrid
    {
        public event Action OnInitialized;
        public event Action<CellMoveData> OnCellMoved;
        public event Action OnWon;

        public int[,] Cells => (int[,])_cells.Clone();
        public int Size => _size;

        private IGridGenerator _gridGenerator;
        private int _size;
        private int[,] _cells;

        private bool _isWin;

        public PuzzleGrid(IGridGenerator gridGenerator, int size)
        {
            if (size < 2) throw new Exception("Grid size must be more then 2!");
            this._size = size;
            _gridGenerator = gridGenerator;
            Initialize();
        }

        public void Initialize()
        {
            _isWin = false;
            _cells = _gridGenerator.Generate(_size);
            if (!IsGridValid()) throw new Exception("Grid generator returns not valid grid!");
            OnInitialized?.Invoke();
        }

        public void Initialize(IGridGenerator gridGenerator, int size)
        {
            _gridGenerator = gridGenerator;
            _size = size;
            Initialize();
        }

        public void ClickOnCell(int i, int j)
        {
            if (_isWin) return;
            if (i > 0 && _cells[i - 1, j] == 0) { MoveCell(i, j, i - 1, j); }
            else if (i < _size - 1 && _cells[i + 1, j] == 0) { MoveCell(i, j, i + 1, j); }
            else if (j > 0 && _cells[i, j - 1] == 0) { MoveCell(i, j, i, j - 1); }
            else if (j < _size - 1 && _cells[i, j + 1] == 0) { MoveCell(i, j, i, j + 1); }

            if (IsWin()) OnWon?.Invoke();
        }

        private void MoveCell(int cell_i, int cell_j, int to_i, int to_j)
        {
            (_cells[to_i, to_j], _cells[cell_i, cell_j]) = (_cells[cell_i, cell_j], _cells[to_i, to_j]);
            OnCellMoved?.Invoke(new(cell_i, cell_j, to_i, to_j));
        }

        private bool IsWin()
        {
            for (int i = _size - 1; i > 0; i--)
            {
                for (int j = _size - 1; j > 0; j--)
                {
                    if (i == _size - 1) j--;
                    if (_cells[i, j] != j + (i * _size) + 1) return false;
                }
            }
            _isWin = true;
            return true;
        }

        private bool IsGridValid()
        {
            int totalCells = _size * _size;
            bool[] found = new bool[totalCells];
            int[] flatGrid = new int[totalCells];
            int emptyRow = 0;

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    int value = _cells[i, j];

                    if (value < 0 || value >= totalCells)
                        return false;

                    if (found[value])
                        return false;

                    found[value] = true;
                    flatGrid[i * _size + j] = value;

                    if (value == 0)
                        emptyRow = i + 1;
                }
            }

            for (int i = 0; i < totalCells; i++)
            {
                if (!found[i])
                    return false;
            }

            int inversions = 0;
            for (int i = 0; i < totalCells; i++)
            {
                int currentValue = flatGrid[i];
                if (currentValue == 0) continue;

                for (int j = i + 1; j < totalCells; j++)
                {
                    int nextValue = flatGrid[j];
                    if (nextValue == 0) continue;

                    if (currentValue > nextValue)
                        inversions++;
                }
            }

            if (_size % 2 == 1)
            {
                return inversions % 2 == 0;
            }
            else
            {
                int emptyRowFromBottom = _size - emptyRow + 1;
                return (inversions % 2 == 0) == (emptyRowFromBottom % 2 == 1);
            }
        }
    }
}