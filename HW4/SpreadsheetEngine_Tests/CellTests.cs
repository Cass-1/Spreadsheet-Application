// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine_Tests;

using SpreadsheetEngine;

/// <summary>
/// Tests methods in the Cell class
/// Note: Getter and Setter tests are included because I was trying to figure out how to implement them.
/// </summary>
public class CellTests
{
    /// <summary>
    /// Tests the getter for RowIndex.
    /// </summary>
    [Test]
    public void RowIndexGetterTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(10, 10);
        Cell cell = spreadsheet.GetCell(0, 0);
        var rowIndex = cell.RowIndex;

        Assert.That(rowIndex, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests the getter for ColumnIndex.
    /// </summary>
    [Test]
    public void ColumnIndexGetterTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(10, 10);
        Cell cell = spreadsheet.GetCell(0, 0);
        var columnIndex = cell.ColumnIndex;

        Assert.That(columnIndex, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests the getter for Text.
    /// </summary>
    [Test]
    public void TextGetterTest()
    {
        TestingCell cell = new TestingCell(0, 0);
        string? text = cell.GetText();

        Assert.That(text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests the setter for Text.
    /// </summary>
    [Test]
    public void TextSetterTest()
    {
        TestingCell cell = new TestingCell(0, 0);
        cell.SetText("hello");

        Assert.That(cell.GetText(), Is.EqualTo("hello"));
    }

    /// <summary>
    /// Tests the getter for Value.
    /// </summary>
    [Test]
    public void ValueGetterTest()
    {
        TestingCell cell = new TestingCell(0, 0);
        string? text = cell.GetValue();

        Assert.That(text, Is.EqualTo(string.Empty));
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
        public TestingCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
        }

        public string? GetText() => this.text;

        public void SetText(string? str)
        {
            this.text = str;
        }

        public string? GetValue()
        {
            return this.value;
        }
    }
}
