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
    public void RowIndexGetterTest()
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
    public void ColumnIndexGetterTest()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
        Cell cell = _spreadsheet.GetCell(0, 0);
        var columnIndex = cell.ColumnIndex;

        Assert.AreEqual(0, columnIndex);
    }
    
    /// <summary>
    /// Tests the getter for Text when no value has been initalized yet.
    /// </summary>
    [Test]
    public void TextGetterEmptyTest()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
        Cell cell = _spreadsheet.GetCell(0, 0);
        string text = cell.Text;

        Assert.AreEqual(String.Empty, text);
    }
    
    /// <summary>
    /// Tests the setter for Text.
    /// </summary>
    [Test]
    public void TextSetterTest()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
        Cell cell = _spreadsheet.GetCell(0, 0);
        cell.Text = "hello";

        Assert.AreEqual("hello", cell.Text);
    }
    
    /// <summary>
    /// Tests the getter for Text when a value has been initalized.
    /// </summary>
    [Test]
    public void TextGetterTest()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
        Cell cell = _spreadsheet.GetCell(0, 0);
        cell.Text = "hello";
        string text = cell.Text;

        Assert.AreEqual("hello", text);
    }
    
    /// <summary>
    /// Tests the getter for Value when no value has been instantiated.
    /// </summary>
    [Test]
    public void ValueGetterEmptyTest()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
        Cell cell = _spreadsheet.GetCell(0, 0);
        string text = cell.Value;

        Assert.AreEqual(String.Empty, text);
    }
    
    /// <summary>
    /// Tests if value is updated when text is updated.
    /// </summary>
    [Test]
    public void SetValueTest()
    {
        Spreadsheet _spreadsheet = new Spreadsheet(10,10);
        Cell cell = _spreadsheet.GetCell(0, 0);
        cell.Text = "test";

        Assert.AreEqual(cell.Text,Cell.Value);
    }
}