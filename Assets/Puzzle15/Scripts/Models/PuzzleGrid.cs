using System;

namespace Puzzle15
{
    public class PuzzleGrid
    {
        public event Action OnInitialized;
        public event Action<CellMoveData> OnCellMoved;
        public event Action OnWon;

        public int[,] Cells => (int[,])_cells.Clone();
        public int Size => size;

        private IGridGenerator _gridGenerator;
        private readonly int size;
        private int[,] _cells;

        public PuzzleGrid(IGridGenerator gridGenerator, int size)
        {
            if (size < 2) throw new Exception("Grid size must be more then 2!");
            this.size = size;
            SetGridGenerator(gridGenerator);
        }

        public void Initialize()
        {
            _cells = _gridGenerator.Generate(size);
            if (!IsGridValid()) throw new Exception("Grid generator returns not valid grid!");
            OnInitialized?.Invoke();
        }

        public void SetGridGenerator(IGridGenerator gridGenerator) => _gridGenerator = gridGenerator;

        public void ClickOnCell(int i, int j)
        {
            if (i > 0 && _cells[i - 1, j] == 0) { MoveCell(i, j, i - 1, j); }
            else if (i < size - 1 && _cells[i + 1, j] == 0) { MoveCell(i, j, i + 1, j); }
            else if (j > 0 && _cells[i, j - 1] == 0) { MoveCell(i, j, i, j - 1); }
            else if (j < size - 1 && _cells[i, j + 1] == 0) { MoveCell(i, j, i, j + 1); }

            if (IsWin()) OnWon?.Invoke();
        }

        private void MoveCell(int cell_i, int cell_j, int to_i, int to_j)
        {
            (_cells[to_i, to_j], _cells[cell_i, cell_j]) = (_cells[cell_i, cell_j], _cells[to_i, to_j]);
            OnCellMoved?.Invoke(new(cell_i, cell_j, to_i, to_j));
        }

        private bool IsWin()
        {
            for (int i = size - 1; i > 0; i--)
            {
                for (int j = size - 1; j > 0; j--)
                {
                    if (i == size - 1) j--;
                    if (_cells[i, j] != j + (i * size) + 1) return false;
                }
            }
            return true;
        }

        private bool IsGridValid()
        {
            int totalCells = size * size;
            bool[] found = new bool[totalCells];
            int[] flatGrid = new int[totalCells];
            int emptyRow = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int value = _cells[i, j];

                    if (value < 0 || value >= totalCells)
                        return false;

                    if (found[value])
                        return false;

                    found[value] = true;
                    flatGrid[i * size + j] = value;

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

            if (size % 2 == 1)
            {
                return inversions % 2 == 0;
            }
            else
            {
                int emptyRowFromBottom = size - emptyRow + 1;
                return (inversions % 2 == 0) == (emptyRowFromBottom % 2 == 1);
            }
        }
    }
}