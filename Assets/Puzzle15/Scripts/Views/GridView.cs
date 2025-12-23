using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle15
{
    [RequireComponent(typeof(Image))]
    public class GridView : MonoBehaviour
    {
        public event Action<int, int> OnCellClicked;

        [SerializeField] private CellView _cellViewPrefab;

        private Transform _transform;
        private RectTransform _rectTransform;

        private int _gridSize;
        private CellView[,] _cellViews;
        private Dictionary<RectTransform, (int, int)> _cellPositionRects;
        private Dictionary<(int, int), RectTransform> _cellIndices;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rectTransform = GetComponent<RectTransform>();

            _rectTransform.SetStretch();
        }

        public void Initialize(int size, int[,] values)
        {
            if (_cellPositionRects == null)
            {
                _gridSize = size;
                InitializeCellsParents(values);
            }
            else
            {
                if (size != _gridSize)
                {
                    foreach(var cellViewRect in _cellPositionRects)
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

        public void SetCellPrefabs(CellView cellViewPrefab)
        {
            _cellViewPrefab = cellViewPrefab;
        }

        public void MoveCell(int i, int j, int ni, int nj)
        {
            if (_cellIndices.TryGetValue((ni, nj), out var targetRect))
            {
                _cellViews[i, j].transform.SetParent(targetRect);
                _cellViews[i, j].ParentRectTransform = targetRect;

                var cellViewRect = _cellViews[i, j].RectTransform;
                cellViewRect.SetStretch();

                _cellViews[ni, nj] = _cellViews[i, j];

                CellMoveAnimation(cellViewRect);
            }
            else
            {
                throw new Exception("Indices not found in dictinary!");
            }
        }

        private void CellMoveAnimation(RectTransform rect)
        {
            rect.DOKill();
            rect.DOScale(Vector3.one * 0.6f, 0.1f)
                .SetEase(Ease.OutCubic);
            rect.DOScale(Vector3.one, 0.25f)
                .SetEase(Ease.OutBack, 1.5f)
                .OnComplete(() => rect.localScale = Vector3.one);
        }

        private void InitializeCellsParents(int[,] values)
        {
            _cellPositionRects = new Dictionary<RectTransform, (int, int)>();
            _cellIndices = new Dictionary<(int, int), RectTransform>();

            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    var cell = new GameObject("Cell[" + i + ", " + j + "]");
                    cell.transform.SetParent(_transform);
                    cell.transform.localScale = Vector3.one;

                    var cellRect = cell.AddComponent<RectTransform>();
                    cellRect.sizeDelta = new Vector2(_rectTransform.rect.width / _gridSize,
                                                     _rectTransform.rect.height / _gridSize);
                    cellRect.anchorMin = new Vector2(0, 1);
                    cellRect.anchorMax = new Vector2(0, 1);
                    cellRect.anchoredPosition = new Vector2(j * cellRect.rect.width + cellRect.rect.width / 2,
                                                            -i * cellRect.rect.height - cellRect.rect.height / 2);
                    _cellPositionRects.Add(cellRect, (i, j));
                    _cellIndices.Add((i, j), cellRect);
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

                    _cellIndices.TryGetValue((i, j), out var cellRect);

                    var cellView = Instantiate(_cellViewPrefab, cellRect);

                    _cellViews[i, j] = cellView;

                    cellView.ParentRectTransform = cellRect;
                    cellView.RectTransform.SetStretch();

                    cellView.SetDigitText(values[i, j].ToString());
                    cellView.OnCellClicked += ClickOnCell;
                }
            }
        }

        private void ClickOnCell(CellView cellView)
        {
            if (_cellPositionRects.TryGetValue(cellView.ParentRectTransform, out var indices))
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