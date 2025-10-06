using System;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class GridPresenter: IInitializable, IDisposable
    {
        private readonly IGridModel _gridModel;
        // private readonly IView _gridView;

        public GridPresenter(IGridModel gridModel) // view
        {
            _gridModel = gridModel;
            // view
        }

        public void Initialize()
        {
            _gridModel.OnCellSwapped += SwapCell;
            _gridModel.OnWin += FinishGame;

            // subscribe to view
        }

        private void SwapCell(int i, int j, int ni, int nj)
        {
            
        }

        private void FinishGame()
        {
            
        }

        public void Dispose()
        {
            _gridModel.OnCellSwapped -= SwapCell;
            _gridModel.OnWin -= FinishGame;
        }
    }
}