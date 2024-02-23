namespace SpreadsheetEngine_Tests;

using SpreadsheetEngine;

public class CellTests
{
    
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Tests the getter for RowIndex
    /// </summary>
    [Test]
    public void RowIndexGetterCellTest()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
        Cell cell = _spreadsheet.GetCell(0, 0);
        var rowIndex = cell.RowIndex;

        Assert.AreEqual(0, rowIndex);
    }

    /// <summary>
    /// Tests the getter for ColumnIndex
    /// </summary>
    [Test]
    public void ColumnIndexGetterCellTest()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
        Cell cell = _spreadsheet.GetCell(0, 0);
        var columnIndex = cell.ColumnIndex;

        Assert.AreEqual(0, columnIndex);
    }
    
    
}