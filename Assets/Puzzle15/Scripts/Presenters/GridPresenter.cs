using System;
using Zenject;

namespace Puzzle15
{
    public class GridPresenter: IInitializable, IDisposable
    {
        private readonly PuzzleGrid _gridModel;
        private readonly GridView _gridView;

        public GridPresenter(PuzzleGrid gridModel, GridView gridView)
        {
            _gridModel = gridModel;
            _gridView = gridView;
        }

        public void Initialize()
        {
            _gridModel.OnCellMoved += SwapCell;
            _gridView.OnCellClicked += ClickOnCell;
            _gridModel.OnInitialized += InitializeGridView;

            _gridModel.Initialize();
        }

        public void Dispose()
        {
            _gridModel.OnCellMoved -= SwapCell;
            _gridView.OnCellClicked -= ClickOnCell;
            _gridModel.OnInitialized -= InitializeGridView;
        }

        private void InitializeGridView() => _gridView.Initialize(_gridModel.Size, _gridModel.Cells);

        private void ClickOnCell(int i, int j) => _gridModel.ClickOnCell(i, j);

        private void SwapCell(CellMoveData cellSwapData) => _gridView.MoveCell(cellSwapData.Cell_i, cellSwapData.Cell_j, cellSwapData.To_i, cellSwapData.To_j);
    }
}
