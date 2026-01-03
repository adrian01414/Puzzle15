using System;
using System.Collections.Generic;

namespace Puzzle15
{
    public sealed class SimpleGridGenerator : IGridGenerator
    {
        public int[,] Generate(int gridSize)
        {
            if (gridSize < 2)
                throw new ArgumentException("Grid size must be more than 2!", nameof(gridSize));

            int totalCells = gridSize * gridSize;
            int[,] resultGrid = new int[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    resultGrid[i, j] = i * gridSize + j + 1;
                }
            }
            resultGrid[gridSize - 1, gridSize - 1] = 0;

            System.Random rng = new System.Random();
            int emptyRow = gridSize - 1;
            int emptyCol = gridSize - 1;

            int shuffleMoves = gridSize * gridSize * gridSize * 10;

            for (int move = 0; move < shuffleMoves; move++)
            {
                var possibleMoves = new List<(int dr, int dc)>();

                if (emptyRow > 0) possibleMoves.Add((-1, 0));
                if (emptyRow < gridSize - 1) possibleMoves.Add((1, 0));
                if (emptyCol > 0) possibleMoves.Add((0, -1));
                if (emptyCol < gridSize - 1) possibleMoves.Add((0, 1));

                var (dr, dc) = possibleMoves[rng.Next(possibleMoves.Count)];

                int swapRow = emptyRow + dr;
                int swapCol = emptyCol + dc;

                resultGrid[emptyRow, emptyCol] = resultGrid[swapRow, swapCol];
                resultGrid[swapRow, swapCol] = 0;

                emptyRow = swapRow;
                emptyCol = swapCol;
            }

            return resultGrid;
        }
    }
}
