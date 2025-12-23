using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class GridTestPanel : MonoBehaviour
    {
        private PuzzleGrid _grid;

        [Inject]
        public void Construct(PuzzleGrid grid)
        {
            _grid = grid;
        }

        [Button]
        public void InitializeNewWinGrid()
        {
            IGridGenerator winGridGenerator = new WinGridGenerator();
            _grid.Initialize(winGridGenerator, _grid.Size);
        }
        
        [Button]
        public void InitializeNewRandomGrid()
        {
            IGridGenerator winGridGenerator = new SimpleGridGenerator();
            _grid.Initialize(winGridGenerator, _grid.Size);
        }
    }
}
