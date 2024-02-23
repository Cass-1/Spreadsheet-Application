namespace SpreadsheetEngine_Tests;

using SpreadsheetEngine;

public class GetCellTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    /// <summary>
    /// For the method GetCell()
    /// Does a simple test to make sure GetCell can get a cell.
    /// </summary>
    [Test]
    public void GetCellBasicTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(10, 10);
        Cell cell = spreadsheet.GetCell(2, 2);

        Assert.NotNull(cell);
    }
    
    /// <summary>
    /// For the method GetCell()
    /// Tests the edge of the 2D array to make sure that the indexes are working properly.
    /// </summary>
    [Test]
    public void GetCellEdgeTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(10, 10);
        Cell cell = spreadsheet.GetCell(9, 9);

        Assert.NotNull(cell);
    }

    /// <summary>
    /// For the method GetCell()
    /// Tests if the error is caught when the rowIndex is out of range.
    /// </summary>
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
    
    /// <summary>
    /// For the method GetCell()
    /// Tests if the error is caught when the columnIndex is out of range.
    /// </summary>
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