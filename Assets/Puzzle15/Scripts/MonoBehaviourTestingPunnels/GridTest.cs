using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class GridTest : MonoBehaviour
    {
        private PuzzleGrid _grid;

        [Inject]
        public void Construct(PuzzleGrid grid)
        {
            _grid = grid;
        }

        [Button]
        public void InitializeNewGrid()
        {
            _grid.Initialize();
        }
    }
}
