namespace Puzzle15
{
    public class WinGridGenerator : IGridGenerator
    {
        public int[,] Generate(int gridSize)
        {
            int[,] gridIn = new int[gridSize, gridSize];
            for (int j = 0; j < gridSize; j++)
            {
                for (int i = 0; i < gridSize; i++)
                {

                    gridIn[i, j] = (i == gridSize - 1) && (j == gridSize - 1) ? 0 : j + (i * gridSize) + 1;
                }
            }
            (gridIn[gridSize - 1, gridSize - 1], gridIn[gridSize - 1, gridSize - 2]) = (gridIn[gridSize - 1, gridSize - 2], gridIn[gridSize - 1, gridSize - 1]);
            return gridIn;
        }
    }
}