using NUnit.Framework;
using UnityEngine;

public class GameControllerTest
{
    [Test]
    public void GetCellValueTest()
    {
        //
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

        //
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

        //
        Assert.True(isCorrect);
    }

    private bool isWin;
    [Test]
    public void WinTest_GridSize_3()
    {
        //
        isWin = false;
        int gridSize = 3;

        GameController gameController = new(gridSize, GenerateGridForWinTest(gridSize));
        gameController.OnWin += SetWin;

        //
        gameController.ClickOnCell(2, 2);
        gameController.OnWin -= SetWin;

        //
        Assert.True(isWin);
    }

    [Test]
    public void WinTest_GridSize_4()
    {
        //
        isWin = false;
        int gridSize = 4;

        GameController gameController = new(gridSize, GenerateGridForWinTest(gridSize));
        gameController.OnWin += SetWin;

        //
        gameController.ClickOnCell(3, 3);
        gameController.OnWin -= SetWin;

        //
        Assert.True(isWin);
    }

    [Test]
    public void WinTest_GridSize_5()
    {
        //
        isWin = false;
        int gridSize = 5;

        GameController gameController = new(gridSize, GenerateGridForWinTest(gridSize));
        gameController.OnWin += SetWin;

        //
        gameController.ClickOnCell(4, 4);
        gameController.OnWin -= SetWin;

        //
        Assert.True(isWin);
    }

    [Test]
    public void ClickOnCellBoundariesTest_LeftUp()
    {
        //
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

        //
        gameController.ClickOnCell(0, 0);

        //
        Assert.AreEqual(prev1, gameController.GetCellValue(0, 1));
        Assert.AreEqual(prev2, gameController.GetCellValue(0, 0));
    }

    [Test]
    public void ClickOnCellBoundariesTest_LeftDown()
    {
        //
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

        //
        gameController.ClickOnCell(gridSize - 1, 0);

        //
        Assert.AreEqual(prev1, gameController.GetCellValue(gridSize - 1, 1));
        Assert.AreEqual(prev2, gameController.GetCellValue(gridSize - 1, 0));
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
