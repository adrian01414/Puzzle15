using NUnit.Framework;
using Puzzle15;

namespace Puzzle15Tests
{
    public class GridTest
    {
        [Test]
        public void WhenGetCellValue_AndGridSize4_ThenValueIsCorrect()
        {
            // Arrange
            int gridSize = 4;
            int[,] gridIn = new int[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    gridIn[i, j] = j + (i * gridSize);
                }
            }
            Grid grid = new(gridSize, gridIn);

            // Act
            bool isCorrect = true;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] != gridIn[i, j])
                    {
                        isCorrect = false;
                    }
                }
            }

            // Assert
            Assert.True(isCorrect);
        }

        private bool isWin;
        [Test]
        public void WhenClickOnCell_AndSubscribeOnWin_GridSize3_ThenIsWinInvoked()
        {
            // Arrange
            isWin = false;
            int gridSize = 3;

            Grid grid = new(gridSize, GenerateGridForWinTest(gridSize));
            grid.OnWin += SetWin;

            // Act
            grid.ClickOnCell(2, 2);
            grid.OnWin -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        [Test]
        public void WhenClickOnCell_AndSubscribeOnWin_GridSize4_ThenIsWinInvoked()
        {
            // Arrange
            isWin = false;
            int gridSize = 4;

            Grid grid = new(gridSize, GenerateGridForWinTest(gridSize));
            grid.OnWin += SetWin;

            // Act
            grid.ClickOnCell(3, 3);
            grid.OnWin -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        [Test]
        public void WhenClickOnCell_AndSubscribeOnWin_GridSize5_ThenIsWinInvoked()
        {
            // Arrange
            isWin = false;
            int gridSize = 5;

            Grid grid = new(gridSize, GenerateGridForWinTest(gridSize));
            grid.OnWin += SetWin;

            // Act
            grid.ClickOnCell(4, 4);
            grid.OnWin -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        [Test]
        public void WhenClickOnBoundaryCell_AndSavePrevValues_ThenLeftUpValueSwapped()
        {
            // Arrange
            int gridSize = 4;
            int[,] gridIn =
            {
            { 1, 0, 2, 3 },
            { 4, 5, 6, 7 },
            { 8, 9, 10, 11 },
            { 12, 13, 14, 15 }
        };
            Grid grid = new(gridSize, gridIn);

            int prev1 = grid[0, 0];
            int prev2 = grid[0, 1];

            // Act
            grid.ClickOnCell(0, 0);

            // Assert
            Assert.AreEqual(prev1, grid[0, 1]);
            Assert.AreEqual(prev2, grid[0, 0]);
        }

        [Test]
        public void WhenClickOnBoundaryCell_AndSavePrevValues_ThenLeftDownValueSwapped()
        {
            // Arrange
            int gridSize = 4;
            int[,] gridIn =
            {
            { 1, 8, 2, 3 },
            { 4, 5, 6, 7 },
            { 13, 9, 10, 11 },
            { 12, 0, 14, 15 }
        };
            Grid grid = new(gridSize, gridIn);

            int prev1 = grid[gridSize - 1, 0];
            int prev2 = grid[gridSize - 1, 1];

            // Act
            grid.ClickOnCell(gridSize - 1, 0);

            // Assert
            Assert.AreEqual(prev1, grid[gridSize - 1, 1]);
            Assert.AreEqual(prev2, grid[gridSize - 1, 0]);
        }

        [Test]
        public void WhenClickOnBoundaryCell_AndSavePrevValues_ThenRightUpValueSwapped()
        {
            // Arrange
            int gridSize = 4;
            int[,] gridIn =
            {
            { 1, 8, 0, 3 },
            { 4, 5, 6, 7 },
            { 13, 9, 10, 11 },
            { 12, 2, 14, 15 }
        };
            Grid grid = new(gridSize, gridIn);

            int prev1 = grid[0, gridSize - 1];
            int prev2 = grid[0, gridSize - 2];

            // Act
            grid.ClickOnCell(0, gridSize - 1);

            // Assert
            Assert.AreEqual(prev1, grid[0, gridSize - 2]);
            Assert.AreEqual(prev2, grid[0, gridSize - 1]);
        }

        [Test]
        public void WhenClickOnBoundaryCell_AndSavePrevValues_ThenRightDownValueSwapped()
        {
            // Arrange
            int gridSize = 4;
            int[,] gridIn =
            {
            { 1, 8, 14, 3 },
            { 4, 5, 6, 7 },
            { 13, 9, 10, 11 },
            { 12, 2, 0, 15 }
        };
            Grid grid = new(gridSize, gridIn);

            int prev1 = grid[gridSize - 1, gridSize - 1];
            int prev2 = grid[gridSize - 1, gridSize - 2];

            // Act
            grid.ClickOnCell(gridSize - 1, gridSize - 1);

            // Assert
            Assert.AreEqual(prev1, grid[gridSize - 1, gridSize - 2]);
            Assert.AreEqual(prev2, grid[gridSize - 1, gridSize - 1]);
        }

        private int[,] GenerateGridForWinTest(int gridSize)
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

        private void SetWin()
        {
            isWin = true;
        }
    }
}