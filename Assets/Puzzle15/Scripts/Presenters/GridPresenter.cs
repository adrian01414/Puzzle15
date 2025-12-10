using System;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public sealed class GridPresenter: IInitializable, IDisposable
    {
        private readonly Grid _gridModel;
        private readonly GridView _gridView;

        public GridPresenter(Grid gridModel, GridView gridView)
        {
            _gridModel = gridModel;
            _gridView = gridView;
        }

        void IInitializable.Initialize()
        {
            _gridModel.OnCellSwapped += SwapCell;
            _gridModel.OnWin += FinishGame;
            _gridView.OnCellClicked += ClickOnCell;
        }

        private void ClickOnCell(int i, int j)
        {
            _gridModel.ClickOnCell(i, j);
        }

        private void SwapCell(int i, int j, int ni, int nj)
        {
            _gridView.SwapCells(i, j, ni, nj);
        }

        private void FinishGame()
        {
            Debug.Log("Finish game!"); //
        }

        void IDisposable.Dispose()
        {
            _gridModel.OnCellSwapped -= SwapCell;
            _gridModel.OnWin -= FinishGame;
        }
    }
}