using NUnit.Framework;

namespace Puzzle15.Tests
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

            PuzzleGrid grid = new(winGridGenerator, gridSize);
            grid.Initialize();
            grid.OnWon += SetWin;

            // Act
            grid.ClickOnCell(2, 2);
            grid.OnWon -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        [Test]
        public void WhenClickOnCell_AndSubscribeOnWin_GridSize4_ThenIsWinInvoked()
        {
            // Arrange
            isWin = false;
            int gridSize = 4;

            PuzzleGrid grid = new(winGridGenerator, gridSize);
            grid.Initialize();
            grid.OnWon += SetWin;

            // Act
            grid.ClickOnCell(3, 3);
            grid.OnWon -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        [Test]
        public void WhenClickOnCell_AndSubscribeOnWin_GridSize5_ThenIsWinInvoked()
        {
            // Arrange
            isWin = false;
            int gridSize = 5;

            PuzzleGrid grid = new(winGridGenerator, gridSize);
            grid.Initialize();
            grid.OnWon += SetWin;

            // Act
            grid.ClickOnCell(4, 4);
            grid.OnWon -= SetWin;

            // Assert
            Assert.True(isWin);
        }

        private void SetWin()
        {
            isWin = true;
        }
    }
}