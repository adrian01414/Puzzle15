using System;
using Zenject;

namespace Puzzle15
{
    public sealed class GridPresenter: IInitializable, IDisposable
    {
        private readonly IGridModel _gridModel;
        private readonly IGridView _gridView;

        public GridPresenter(IGridModel gridModel, IGridView gridView)
        {
            _gridModel = gridModel;
            _gridView = gridView;
        }

        void IInitializable.Initialize()
        {
            _gridModel.OnCellSwapped += SwapCell;
            _gridModel.OnWin += FinishGame;
        }

        private void SwapCell(int i, int j, int ni, int nj)
        {
            
        }

        private void FinishGame()
        {
            
        }

        void IDisposable.Dispose()
        {
            _gridModel.OnCellSwapped -= SwapCell;
            _gridModel.OnWin -= FinishGame;
        }
    }
}