// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Text;
using System.Xml;

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
    /// A basic test for the WriteXml method.
    /// </summary>
    [Test]
    public void WriteXmlBasicTest()
    {
        // Arrange
        TestingCell cell = new TestingCell(0, 0);
        cell.SetText("hello");
        StringBuilder sb = new StringBuilder();
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.OmitXmlDeclaration = true; // Add this line


        // Act
        using (XmlWriter writer = XmlWriter.Create(sb, settings))
        {
            cell.WriteXml(writer);
        }

        // Assert
        string expectedXml = "<Cell>\n  <Name>A1</Name>\n  <Text>hello</Text>\n  <Expression />\n  <Value />\n  <BackgroundColor>4294967295</BackgroundColor>\n</Cell>";
        Assert.That(sb.ToString(), Is.EqualTo(expectedXml));
    }

    /// <summary>
    /// A test for the WriteXml method when the cell is given no values.
    /// </summary>
    [Test]
    public void WriteXmlEmptyTest()
    {
        // Arrange
        TestingCell cell = new TestingCell(0, 0);
        StringBuilder sb = new StringBuilder();
        XmlWriterSettings settings = new XmlWriterSettings();

        // makes the xml output easier to read for testing
        settings.Indent = true;
        settings.OmitXmlDeclaration = true;


        // Act
        using (XmlWriter writer = XmlWriter.Create(sb, settings))
        {
            cell.WriteXml(writer);
        }

        // Assert
        string expectedXml = "<Cell>\n  <Name>A1</Name>\n  <Text />\n  <Expression />\n  <Value />\n  <BackgroundColor>4294967295</BackgroundColor>\n</Cell>";
        Assert.That(sb.ToString(), Is.EqualTo(expectedXml));
    }

    /// <summary>
    /// A test for the WriteXml method when the cell is actually in a spreadsheet.
    /// </summary>
    [Test]
    public void WriteXmlInSpreadsheetTest()
    {
        // Arrange
        Spreadsheet spreadsheet = new Spreadsheet(5, 5);
        Cell cell = spreadsheet.GetCell(0, 0);
        cell.Text = "=5+9";
        StringBuilder sb = new StringBuilder();
        XmlWriterSettings settings = new XmlWriterSettings();

        // makes the xml output easier to read for testing
        settings.Indent = true;
        settings.OmitXmlDeclaration = true;

        // Act
        using (XmlWriter writer = XmlWriter.Create(sb, settings))
        {
            cell.WriteXml(writer);
        }

        // Assert
        string expectedXml = "<Cell>\n  <Name>A1</Name>\n  <Text>=5+9</Text>\n  <Expression>5+9</Expression>\n  <Value>14</Value>\n  <BackgroundColor>4294967295</BackgroundColor>\n</Cell>";
        Assert.That(sb.ToString(), Is.EqualTo(expectedXml));
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
