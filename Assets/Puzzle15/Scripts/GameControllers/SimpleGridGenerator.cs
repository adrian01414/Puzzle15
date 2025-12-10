using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle15
{
    public sealed class SimpleGridGenerator : IGridGenerator
    {
        public int[,] Generate(int gridSize)
        {
            int[,] resultGrid = new int[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    resultGrid[i, j] = j + (i * gridSize);
                }
            }

            System.Random rng = new System.Random();
            for (int i = gridSize * gridSize - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);

                int currentRow = i / gridSize;
                int currentCol = i % gridSize;
                int randomRow = j / gridSize;
                int randomCol = j % gridSize;

                (resultGrid[randomRow, randomCol], resultGrid[currentRow, currentCol]) = 
                    (resultGrid[currentRow, currentCol], resultGrid[randomRow, randomCol]);
            }

            return resultGrid;
        }
    }
}
