namespace SpreadsheetEngine_Tests;

using SpreadsheetEngine;

public class CellTests
{

    private Spreadsheet _spreadsheet;
    
    /// <summary>
    /// Initalizes a 10 by 10 spreadsheet.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
    }

    /// <summary>
    /// Tests the getter for RowIndex
    /// </summary>
    [Test]
    public void RowIndexGetterCellTest()
    {
        Cell cell = _spreadsheet.GetCell(0, 0);
        var rowIndex = cell.RowIndex;

        Assert.Equals(0, rowIndex);
    }

    /// <summary>
    /// Tests the getter for ColumnIndex
    /// </summary>
    [Test]
    public void ColumnIndexGetterCellTest()
    {
        Cell cell = _spreadsheet.GetCell(0, 0);
        var columnIndex = cell.ColumnIndex;

        Assert.Equals(0, columnIndex);
    }
}