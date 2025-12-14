using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle15
{
    public class GridView : MonoBehaviour
    {
        public event Action<int, int> OnCellClicked;

        [SerializeField] private Transform _cellsParentTransfom;
        [SerializeField] private CellView _cellViewPrefab;

        private int _gridSize;
        private CellView[,] _cellViews;
        private Dictionary<RectTransform, (int, int)> _cellsPositions;
        private Dictionary<(int, int), RectTransform> _cellsIndices;

        public void Initialize(int size, int[,] values)
        {
            if (_cellsPositions == null)
            {
                _gridSize = size;
                InitializeCellsParents(values);
            }
            else
            {
                if (size != _gridSize)
                {
                    foreach(var cellViewRect in _cellsPositions)
                    {
                        Destroy(cellViewRect.Key.gameObject);
                    }
                    InitializeCellsParents(values);
                }
            }
            
            if (_cellViews != null)
            {
                foreach (var cellView in _cellViews)
                {
                    if(cellView != null)
                    {
                        Destroy(cellView.gameObject);
                    }
                }
            }
            InitializeCells(values);
        }

        private void InitializeCellsParents(int[,] values)
        {
            var cellsRect = GetComponent<RectTransform>();
            _cellsPositions = new Dictionary<RectTransform, (int, int)>();
            _cellsIndices = new Dictionary<(int, int), RectTransform>();

            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    var cell = new GameObject("Cell[" + i + ", " + j + "]");
                    cell.transform.SetParent(_cellsParentTransfom);
                    cell.transform.localScale = Vector3.one;

                    var cellRect = cell.AddComponent<RectTransform>();
                    cellRect.sizeDelta = new Vector2(cellsRect.rect.width / _gridSize,
                                                     cellsRect.rect.height / _gridSize);
                    cellRect.anchorMin = new Vector2(0, 1);
                    cellRect.anchorMax = new Vector2(0, 1);
                    cellRect.anchoredPosition = new Vector2(j * cellRect.rect.width + cellRect.rect.width / 2,
                                                            -i * cellRect.rect.height - cellRect.rect.height / 2);
                    _cellsPositions.Add(cellRect, (i, j));
                    _cellsIndices.Add((i, j), cellRect);
                }
            }
        }

        private void InitializeCells(int[,] values)
        {
            _cellViews = new CellView[_gridSize, _gridSize];

            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    if (values[i, j] == 0)
                    {
                        continue;
                    }

                    _cellsIndices.TryGetValue((i, j), out var cellRect);

                    var cellView = Instantiate(_cellViewPrefab, cellRect);

                    _cellViews[i, j] = cellView;

                    cellView.ParentRectTransform = cellRect;
                    var cellViewRect = cellView.RectTransform;

                    cellViewRect.anchorMin = Vector2.zero;
                    cellViewRect.anchorMax = Vector2.one;

                    cellViewRect.offsetMin = Vector2.zero;
                    cellViewRect.offsetMax = Vector2.zero;

                    cellView.SetDigitText(values[i, j].ToString());
                    cellView.OnCellClicked += ClickOnCell;
                }
            }
        }

        public void MoveCell(int i, int j, int ni, int nj)
        {
            if (_cellsIndices.TryGetValue((ni, nj), out var targetRect))
            {
                _cellViews[i, j].transform.SetParent(targetRect);
                _cellViews[i, j].ParentRectTransform = targetRect;

                var cellViewRect = _cellViews[i, j].RectTransform;
                cellViewRect.offsetMin = Vector2.zero;
                cellViewRect.offsetMax = Vector2.zero;

                _cellViews[ni, nj] = _cellViews[i, j];
            } else
            {
                throw new Exception("Indices not found in dictinary!");
            }
        }

        private void ClickOnCell(CellView cellView)
        {
            if (_cellsPositions.TryGetValue(cellView.ParentRectTransform, out var indices))
            {
                OnCellClicked?.Invoke(indices.Item1, indices.Item2);
            } else
            {
                throw new Exception("Rect transfom not found in dictinary!");
            }
        }

        private void OnDisable()
        {
            if (_cellViews != null)
            {
                foreach (var cell in _cellViews)
                {
                    if (cell != null)
                    {
                        cell.OnCellClicked -= ClickOnCell;
                    }
                }
            }
        }
    }
}