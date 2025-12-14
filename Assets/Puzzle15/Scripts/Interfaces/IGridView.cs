using System;

namespace Puzzle15
{
    public interface IGridView
    {
        public event Action<int, int> OnCellClicked;

        public void SwapCells(CellMoveData swapData);
    }
}
