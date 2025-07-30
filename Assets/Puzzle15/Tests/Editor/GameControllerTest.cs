using NUnit.Framework;
using Puzzle15;

namespace Puzzle15Tests
{
    public class GameControllerTest
    {
        [Test]
        public void WhenGetCellValue_AndGridSize4_ThenValueIsCorrect()
        {
            // Arrange
            int gridSize = 4;
            int[,] grid = new int[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = j + (i * gridSize);
                }
            }
            GameController gameController = new(gridSize, grid);

            // Act
            bool isCorrect = true;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (gameController.GetCellValue(i, j) != grid[i, j])
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

            GameController gameController = new(gridSize, GenerateGridForWinTest(gridSize));
            gameController.OnWin += SetWin;

            // Act
            gameController.ClickOnCell(2, 2);
            gameController.OnWin -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        [Test]
        public void WhenClickOnCell_AndSubscribeOnWin_GridSize4_ThenIsWinInvoked()
        {
            // Arrange
            isWin = false;
            int gridSize = 4;

            GameController gameController = new(gridSize, GenerateGridForWinTest(gridSize));
            gameController.OnWin += SetWin;

            // Act
            gameController.ClickOnCell(3, 3);
            gameController.OnWin -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        [Test]
        public void WhenClickOnCell_AndSubscribeOnWin_GridSize5_ThenIsWinInvoked()
        {
            // Arrange
            isWin = false;
            int gridSize = 5;

            GameController gameController = new(gridSize, GenerateGridForWinTest(gridSize));
            gameController.OnWin += SetWin;

            // Act
            gameController.ClickOnCell(4, 4);
            gameController.OnWin -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        [Test]
        public void WhenClickOnBoundaryCell_AndSavePrevValues_ThenLeftUpValueSwapped()
        {
            // Arrange
            int gridSize = 4;
            int[,] grid =
            {
            { 1, 0, 2, 3 },
            { 4, 5, 6, 7 },
            { 8, 9, 10, 11 },
            { 12, 13, 14, 15 }
        };
            GameController gameController = new(gridSize, grid);

            int prev1 = gameController.GetCellValue(0, 0);
            int prev2 = gameController.GetCellValue(0, 1);

            // Act
            gameController.ClickOnCell(0, 0);

            // Assert
            Assert.AreEqual(prev1, gameController.GetCellValue(0, 1));
            Assert.AreEqual(prev2, gameController.GetCellValue(0, 0));
        }

        [Test]
        public void WhenClickOnBoundaryCell_AndSavePrevValues_ThenLeftDownValueSwapped()
        {
            // Arrange
            int gridSize = 4;
            int[,] grid =
            {
            { 1, 8, 2, 3 },
            { 4, 5, 6, 7 },
            { 13, 9, 10, 11 },
            { 12, 0, 14, 15 }
        };
            GameController gameController = new(gridSize, grid);

            int prev1 = gameController.GetCellValue(gridSize - 1, 0);
            int prev2 = gameController.GetCellValue(gridSize - 1, 1);

            // Act
            gameController.ClickOnCell(gridSize - 1, 0);

            // Assert
            Assert.AreEqual(prev1, gameController.GetCellValue(gridSize - 1, 1));
            Assert.AreEqual(prev2, gameController.GetCellValue(gridSize - 1, 0));
        }

        [Test]
        public void WhenClickOnBoundaryCell_AndSavePrevValues_ThenRightUpValueSwapped()
        {
            // Arrange
            int gridSize = 4;
            int[,] grid =
            {
            { 1, 8, 0, 3 },
            { 4, 5, 6, 7 },
            { 13, 9, 10, 11 },
            { 12, 2, 14, 15 }
        };
            GameController gameController = new(gridSize, grid);

            int prev1 = gameController.GetCellValue(0, gridSize - 1);
            int prev2 = gameController.GetCellValue(0, gridSize - 2);

            // Act
            gameController.ClickOnCell(0, gridSize - 1);

            // Assert
            Assert.AreEqual(prev1, gameController.GetCellValue(0, gridSize - 2));
            Assert.AreEqual(prev2, gameController.GetCellValue(0, gridSize - 1));
        }

        [Test]
        public void WhenClickOnBoundaryCell_AndSavePrevValues_ThenRightDownValueSwapped()
        {
            // Arrange
            int gridSize = 4;
            int[,] grid =
            {
            { 1, 8, 14, 3 },
            { 4, 5, 6, 7 },
            { 13, 9, 10, 11 },
            { 12, 2, 0, 15 }
        };
            GameController gameController = new(gridSize, grid);

            int prev1 = gameController.GetCellValue(gridSize - 1, gridSize - 1);
            int prev2 = gameController.GetCellValue(gridSize - 1, gridSize - 2);

            // Act
            gameController.ClickOnCell(gridSize - 1, gridSize - 1);

            // Assert
            Assert.AreEqual(prev1, gameController.GetCellValue(gridSize - 1, gridSize - 2));
            Assert.AreEqual(prev2, gameController.GetCellValue(gridSize - 1, gridSize - 1));
        }

        private int[,] GenerateGridForWinTest(int gridSize)
        {
            int[,] grid = new int[gridSize, gridSize];
            for (int j = 0; j < gridSize; j++)
            {
                for (int i = 0; i < gridSize; i++)
                {

                    grid[i, j] = (i == gridSize - 1) && (j == gridSize - 1) ? 0 : j + (i * gridSize) + 1;
                }
            }
            (grid[gridSize - 1, gridSize - 1], grid[gridSize - 1, gridSize - 2]) = (grid[gridSize - 1, gridSize - 2], grid[gridSize - 1, gridSize - 1]);
            return grid;
        }

        private void SetWin()
        {
            isWin = true;
        }
    }
}