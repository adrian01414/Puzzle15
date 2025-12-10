using NUnit.Framework;
using Puzzle15;

namespace Puzzle15Tests
{
    public sealed class GridTest
    {
        IGridGenerator winGridGenerator = new WinGridGenerator();

        private bool isWin;
        [Test]
        public void WhenClickOnCell_AndSubscribeOnWin_GridSize3_ThenIsWinInvoked()
        {
            // Arrange
            isWin = false;
            int gridSize = 3;

            Grid grid = new(winGridGenerator, gridSize);
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

            Grid grid = new(winGridGenerator, gridSize);
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

            Grid grid = new(winGridGenerator, gridSize);
            grid.OnWin += SetWin;

            // Act
            grid.ClickOnCell(4, 4);
            grid.OnWin -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        private void SetWin()
        {
            isWin = true;
        }
    }

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