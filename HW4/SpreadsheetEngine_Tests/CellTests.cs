namespace SpreadsheetEngine_Tests;

using SpreadsheetEngine;

/// <summary>
/// Tests methods in the Cell class
/// Note: Getter and Setter tests are included because I was trying to figure out how to implement them.
/// </summary>
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
    /// Tests the getter for Text.
    /// </summary>
    [Test]
    public void TextGetterTest()
    {
        TestingCell cell = new TestingCell(0, 0);
        string text = cell.GetText();

        Assert.AreEqual(String.Empty, text);
    }
    
    /// <summary>
    /// Tests the setter for Text.
    /// </summary>
    [Test]
    public void TextSetterTest()
    {
        TestingCell cell = new TestingCell(0, 0);
        cell.SetText("hello");

        Assert.AreEqual("hello", cell.GetText());
    }
    
    
    /// <summary>
    /// Tests the getter for Value.
    /// </summary>
    [Test]
    public void ValueGetterTest()
    {
        TestingCell cell = new TestingCell(0, 0);
        string text = cell.GetValue();

        Assert.AreEqual(String.Empty, text);
    }
    
    /// <summary>
    /// Tests the setter for Value.
    /// </summary>
    [Test]
    public void SetValueTest()
    {
        TestingCell cell = new TestingCell(0, 0);
        cell.SetValue("hello");
        Assert.AreEqual("hello", cell.GetValue());
        
    }
    
    /// <summary>
    /// This allows testing of protected methods.
    /// </summary>
    private class TestingCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestingCell"/> class. This is done so that protected
        /// methods can be tested.
        /// </summary>
        /// <param name="rowIndex">The cell's row index in a spreadsheet.</param>
        /// <param name="columnIndex">The cell's column index in a spreadsheet.</param>
        public TestingCell(int rowIndex, int columnIndex) : base(rowIndex, columnIndex)
        {
        }
        public string GetText()
        {
            return base.Text;
        }

        public void SetText(string str)
        {
            base.Text = str;
        }

        public string GetValue()
        {
            return base.Value;
        }

        public void SetValue(string str)
        {
            base.Value = str;
        }

    }
}