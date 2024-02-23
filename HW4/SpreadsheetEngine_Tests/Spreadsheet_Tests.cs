namespace SpreadsheetEngine_Tests;

using System.Reflection;
using SpreadsheetEngine;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void GetCellBasicTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(10, 10);
        Cell cell = spreadsheet.GetCell(2, 2);

        Assert.NotNull(cell);
    }
    
    [Test]
    public void GetCellEdgeTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(1, 1);
        Cell cell = spreadsheet.GetCell(0, 0);

        Assert.NotNull(cell);
    }

    [Test]
    public void GetCellRowCountOutOfBoundsTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(1, 1);
        
        try
        {
            Cell cell = spreadsheet.GetCell(1, 0);
        }
        catch (Exception e)
        {
            Assert.True(e is ArgumentOutOfRangeException);
        }
    }
    
    [Test]
    public void GetCellColumnCountOutOfBoundsTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(1, 1);
        
        try
        {
            Cell cell = spreadsheet.GetCell(0, 1);
        }
        catch (Exception e)
        {
            Assert.True(e is ArgumentOutOfRangeException);
        }
    }
}