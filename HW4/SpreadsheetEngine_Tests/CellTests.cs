// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Text;
using System.Xml;
using System.Xml.Linq;
using SpreadsheetEngine;

namespace SpreadsheetEngine_Tests;

/// <summary>
///     Tests methods in the Cell class
///     Note: Getter and Setter tests are included because I was trying to figure out how to implement them.
/// </summary>
public class CellTests
{
    /// <summary>
    ///     Tests the getter for RowIndex.
    /// </summary>
    [Test]
    public void RowIndexGetterTest()
    {
        var spreadsheet = new Spreadsheet(10, 10);
        var cell = spreadsheet.GetCell(0, 0);
        var rowIndex = cell.RowIndex;

        Assert.That(rowIndex, Is.EqualTo(0));
    }

    /// <summary>
    ///     Tests the getter for ColumnIndex.
    /// </summary>
    [Test]
    public void ColumnIndexGetterTest()
    {
        var spreadsheet = new Spreadsheet(10, 10);
        var cell = spreadsheet.GetCell(0, 0);
        var columnIndex = cell.ColumnIndex;

        Assert.That(columnIndex, Is.EqualTo(0));
    }

    /// <summary>
    ///     Tests the getter for Text.
    /// </summary>
    [Test]
    public void TextGetterTest()
    {
        var cell = new TestingCell(0, 0);
        var text = cell.GetText();

        Assert.That(text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    ///     Tests the setter for Text.
    /// </summary>
    [Test]
    public void TextSetterTest()
    {
        var cell = new TestingCell(0, 0);
        cell.SetText("hello");

        Assert.That(cell.GetText(), Is.EqualTo("hello"));
    }

    /// <summary>
    ///     Tests the getter for Value.
    /// </summary>
    [Test]
    public void ValueGetterTest()
    {
        var cell = new TestingCell(0, 0);
        var text = cell.GetValue();

        Assert.That(text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    ///     A basic test for the WriteXml method.
    /// </summary>
    [Test]
    public void WriteXmlBasicTest()
    {
        // Arrange
        var cell = new TestingCell(0, 0);
        cell.SetText("hello");
        var sb = new StringBuilder();
        var settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.OmitXmlDeclaration = true; // Add this line

        // Act
        using (var writer = XmlWriter.Create(sb, settings))
        {
            cell.WriteXml(writer);
        }

        // Assert
        var expectedXml =
            "<Cell>\n  <Name>A1</Name>\n  <Text>hello</Text>\n  <Expression />\n  <Value />\n  <BackgroundColor>4294967295</BackgroundColor>\n</Cell>";
        Assert.That(sb.ToString(), Is.EqualTo(expectedXml));
    }

    /// <summary>
    ///     A test for the WriteXml method when the cell is given no values.
    /// </summary>
    [Test]
    public void WriteXmlEmptyTest()
    {
        // Arrange
        var cell = new TestingCell(0, 0);
        var sb = new StringBuilder();
        var settings = new XmlWriterSettings();

        // makes the xml output easier to read for testing
        settings.Indent = true;
        settings.OmitXmlDeclaration = true;

        // Act
        using (var writer = XmlWriter.Create(sb, settings))
        {
            cell.WriteXml(writer);
        }

        // Assert
        var expectedXml =
            "<Cell>\n  <Name>A1</Name>\n  <Text />\n  <Expression />\n  <Value />\n  <BackgroundColor>4294967295</BackgroundColor>\n</Cell>";
        Assert.That(sb.ToString(), Is.EqualTo(expectedXml));
    }

    /// <summary>
    ///     A test for the WriteXml method when the cell is actually in a spreadsheet.
    /// </summary>
    [Test]
    public void WriteXmlInSpreadsheetTest()
    {
        // Arrange
        var spreadsheet = new Spreadsheet(5, 5);
        var cell = spreadsheet.GetCell(0, 0);
        cell.Text = "=5+9";
        var sb = new StringBuilder();
        var settings = new XmlWriterSettings();

        // makes the xml output easier to read for testing
        settings.Indent = true;
        settings.OmitXmlDeclaration = true;

        // Act
        using (var writer = XmlWriter.Create(sb, settings))
        {
            cell.WriteXml(writer);
        }

        // Assert
        var expectedXml =
            "<Cell>\n  <Name>A1</Name>\n  <Text>=5+9</Text>\n  <Expression>5+9</Expression>\n  <Value>14</Value>\n  <BackgroundColor>4294967295</BackgroundColor>\n</Cell>";
        Assert.That(sb.ToString(), Is.EqualTo(expectedXml));
    }

    /// <summary>
    ///     Basic test for ReadXml.
    /// </summary>
    [Test]
    public void ReadXmlBasicTest()
    {
        // Arrange
        var cell = new TestingCell(0, 0);
        var xmlData =
            "<Cell>\n  <Name>A1</Name>\n  <Text>hello</Text>\n  <Expression>=5+9</Expression>\n  <Value>14</Value>\n  <BackgroundColor>4294967295</BackgroundColor>\n</Cell>";
        var settings = new XmlReaderSettings();
        var sr = new StringReader(xmlData);
        var reader = XmlReader.Create(sr, settings);
        var xElement = XDocument.Load(reader).Element("Cell");

        // Act
        cell.ReadXml(xElement!);

        // Assert
        Assert.That(cell.Name, Is.EqualTo("A1"));
        Assert.That(cell.Text, Is.EqualTo("hello"));
        Assert.That(cell.Expression, Is.EqualTo("=5+9"));
        Assert.That(cell.GetValue(), Is.EqualTo("14"));
        Assert.That(cell.BackgroundColor, Is.EqualTo(4294967295));
    }

    /// <summary>
    ///     Tests ReadXml when the cell is empty. If no errors are thrown, the test passes.
    /// </summary>
    [Test]
    public void ReadXmlEmptyTest()
    {
        // Arrange
        var cell = new TestingCell(0, 0);
        var xmlData = "<Cell>\n  <Name />\n  <Text />\n  <Expression />\n  <Value />\n  <BackgroundColor />\n</Cell>";
        var settings = new XmlReaderSettings();
        var sr = new StringReader(xmlData);
        var reader = XmlReader.Create(sr, settings);
        var xElement = XDocument.Load(reader).Element("Cell");

        // Act
        cell.ReadXml(xElement!);

        // throw new NotImplementedException();
        Assert.Pass();
    }

    /// <summary>
    /// Tests ReadXml when the XML is out of order. If no errors are thrown, the test passes.
    /// </summary>
    [Test]
    public void ReadXmlOutOfOrderTest()
    {
        // Arrange
        var cell = new TestingCell(0, 0);
        var xmlData =
            "<Cell>\n  <Text />  \n<Name>A1</Name>\n  <Expression />\n  <BackgroundColor>4294967295</BackgroundColor>\n  <Value />\n</Cell>";
        var settings = new XmlReaderSettings();
        var sr = new StringReader(xmlData);
        var reader = XmlReader.Create(sr, settings);
        var xElement = XDocument.Load(reader).Element("Cell");

        // Act
        cell.ReadXml(xElement!);

        // Assert
        Assert.That(cell.Name, Is.EqualTo("A1"));
        Assert.That(cell.GetText(), Is.EqualTo(string.Empty));
        Assert.That(cell.BackgroundColor, Is.EqualTo(4294967295));
    }

    /// <summary>
    ///     Tests the ChangedFromDefaults method.
    /// </summary>
    [Test]
    public void TestChangedFromDefaults()
    {
        // Arrange
        var cell = new TestingCell(0, 0);

        // Assert that ChangedFromDefaults returns false for a new cell
        Assert.IsFalse(cell.ChangedFromDefaults());

        // Change Text and assert that ChangedFromDefaults returns true
        cell.Text = "Test";
        Assert.IsTrue(cell.ChangedFromDefaults());

        // Reset Text and assert that ChangedFromDefaults returns false
        cell.Text = string.Empty;
        Assert.IsFalse(cell.ChangedFromDefaults());

        // Change Value and assert that ChangedFromDefaults returns true
        cell.SetValue("Test");
        Assert.IsTrue(cell.ChangedFromDefaults());

        // Reset Value and assert that ChangedFromDefaults returns false
        cell.SetValue(string.Empty);
        Assert.IsFalse(cell.ChangedFromDefaults());

        // Change Expression and assert that ChangedFromDefaults returns true
        cell.Expression = "Test";
        Assert.IsTrue(cell.ChangedFromDefaults());

        // Reset Expression and assert that ChangedFromDefaults returns false
        cell.Expression = string.Empty;
        Assert.IsFalse(cell.ChangedFromDefaults());

        // Change BackgroundColor and assert that ChangedFromDefaults returns true
        cell.BackgroundColor = 0;
        Assert.IsTrue(cell.ChangedFromDefaults());

        // Reset BackgroundColor and assert that ChangedFromDefaults returns false
        cell.BackgroundColor = uint.MaxValue;
        Assert.IsFalse(cell.ChangedFromDefaults());
    }

    /// <summary>
    ///   Tests the Clear method.
    /// </summary>
    [Test]
    public void ClearBasicTest()
    {
        var cell = new TestingCell(0, 0);

        cell.Text = "=5+7";
        cell.BackgroundColor = 45;

        cell.Clear();

        Assert.That(cell.Text, Is.EqualTo(string.Empty));
        Assert.That(cell.Value, Is.EqualTo(string.Empty));
        Assert.That(cell.Expression, Is.EqualTo(string.Empty));
        Assert.That(cell.BackgroundColor, Is.EqualTo(uint.MaxValue));
    }

    /// <summary>
    /// Tests for if a cell references itself.
    /// </summary>
    [Test]
    public void SelfReferenceBasicTest()
    {
        var cell = new TestingCell(0, 0);
        try
        {
            cell.Text = "=A1";
            Assert.Fail();
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    /// <summary>
    /// Tests for if a cell references itself.
    /// </summary>
    [Test]
    public void SelfReferenceExpressionTest()
    {
        var cell = new TestingCell(0, 0);
        try
        {
            cell.Text = "=7+A1*9";
            Assert.Fail();
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
}
