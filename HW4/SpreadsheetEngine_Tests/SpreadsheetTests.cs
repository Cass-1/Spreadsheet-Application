// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Xml;
using System.Xml.Linq;
using SpreadsheetEngine;

namespace SpreadsheetEngine_Tests;

/// <summary>
///     Tests for the spreadsheet class.
/// </summary>
public class SpreadsheetTests
{
    /// <summary>
    ///     For the method GetCell()
    ///     Does a simple test to make sure GetCell can get a cell.
    /// </summary>
    [Test]
    public void GetCellBasicTest()
    {
        var spreadsheet = new Spreadsheet(10, 10);
        var cell = spreadsheet.GetCell(2, 2);

        Assert.NotNull(cell);
    }

    /// <summary>
    ///     For the method GetCell()
    ///     Tests the edge of the 2D array to make sure that the indexes are working properly.
    /// </summary>
    [Test]
    public void GetCellEdgeTest()
    {
        var spreadsheet = new Spreadsheet(10, 10);
        var cell = spreadsheet.GetCell(9, 9);

        Assert.NotNull(cell);
    }

    /// <summary>
    ///     For the method GetCell()
    ///     Tests if the error is caught when the rowIndex is out of range.
    /// </summary>
    [Test]
    public void GetCellRowCountOutOfBoundsTest()
    {
        var spreadsheet = new Spreadsheet(1, 1);

        try
        {
            spreadsheet.GetCell(1, 0);
        }
        catch (Exception e)
        {
            Assert.True(e is ArgumentOutOfRangeException);
        }
    }

    /// <summary>
    ///     For the method GetCell()
    ///     Tests if the error is caught when the columnIndex is out of range.
    /// </summary>
    [Test]
    public void GetCellColumnCountOutOfBoundsTest()
    {
        var spreadsheet = new Spreadsheet(1, 1);

        try
        {
            spreadsheet.GetCell(0, 1);
        }
        catch (Exception e)
        {
            Assert.That(e is ArgumentOutOfRangeException);
        }
    }

    /// <summary>
    ///     Tests if a cell's value is updated when the text is.
    /// </summary>
    [Test]
    public void CellPropertyChangedBasicTest()
    {
        var spreadsheet = new Spreadsheet(10, 10);
        var cell = spreadsheet.GetCell(1, 1);
        cell.Text = "testing";

        Assert.That(cell.Value, Is.EqualTo(cell.Text));
    }

    /// <summary>
    ///     Tests if when the text is a expression to another cell if value is set to the other cell's value.
    /// </summary>
    [Test]
    public void CellPropertyChangedExpressionIsOtherCellTest1()
    {
        var spreadsheet = new Spreadsheet(10, 10);
        var referenceCell = spreadsheet.GetCell(1, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        referenceCell.Text = "78";
        testCell.Text = "=B2";

        Assert.That(referenceCell.Value, Is.EqualTo(testCell.Value));
    }

    /// <summary>
    ///     Tests if when the text is a expression to another cell if value is set to the other cell's value.
    /// </summary>
    [Test]
    public void CellPropertyChangedExpressionIsOtherCellTest2()
    {
        var spreadsheet = new Spreadsheet(20, 20);
        var referenceCell = spreadsheet.GetCell(9, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        referenceCell.Text = "56";
        testCell.Text = "=B10";

        Assert.That(referenceCell.Value, Is.EqualTo(testCell.Value));
    }

    /// <summary>
    ///     Tests when a cell references a cell that doesn't exist.
    /// </summary>
    [Test]
    public void CellPropertyChangedExpressionIsOtherCellTestOutOfBounds()
    {
        var spreadsheet = new Spreadsheet(20, 20);
        var testCell = spreadsheet.GetCell(2, 2);
        try
        {
            testCell.Text = "=B60";
        }
        catch (Exception e)
        {
            Assert.True(e is ArgumentOutOfRangeException);
        }
    }

    /// <summary>
    ///     Tests cell evaluating expression when the expression is dependent on two other cells.
    /// </summary>
    [Test]
    public void ExpressionDependentOnTwoCellsBasicTest()
    {
        var spreadsheet = new Spreadsheet(20, 20);
        var referenceCell = spreadsheet.GetCell(9, 1);
        var referenceCellTwo = spreadsheet.GetCell(8, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        referenceCell.Text = "5";
        referenceCellTwo.Text = "11";
        testCell.Text = "=B10+B9";

        Assert.That(testCell.Value, Is.EqualTo("16"));
    }

    /// <summary>
    ///     Tests cell evaluating expression when the expression is dependent on two other cells.
    /// </summary>
    [Test]
    public void ExpressionDependentOnTwoCellsTest()
    {
        var spreadsheet = new Spreadsheet(20, 20);
        var b10 = spreadsheet.GetCell(9, 1);
        var b9 = spreadsheet.GetCell(8, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        b10.Text = "5";
        b9.Text = "11";
        testCell.Text = "=B10+B9/B9*(22/B9)";

        Assert.That(testCell.Value, Is.EqualTo("7"));
    }

    /// <summary>
    ///     Tests a cell evaluating an expression when a reference cell changes.
    /// </summary>
    [Test]
    public void ReferenceCellChangedBasicTest()
    {
        var spreadsheet = new Spreadsheet(20, 20);
        var b10 = spreadsheet.GetCell(9, 1);
        var b9 = spreadsheet.GetCell(8, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        b10.Text = "5";
        b9.Text = "10";
        testCell.Text = "=B10+B9";
        b10.Text = "6";

        Assert.That(testCell.Value, Is.EqualTo("16"));
    }

    /// <summary>
    ///     Tests a cell evaluating an expression when a reference cell changes.
    /// </summary>
    [Test]
    public void ReferenceCellChangedTwoCellsTest()
    {
        var spreadsheet = new Spreadsheet(20, 20);
        var b10 = spreadsheet.GetCell(9, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        b10.Text = "5";
        testCell.Text = "=B10";
        b10.Text = "6";

        Assert.That(testCell.Value, Is.EqualTo(b10.Value));
    }

    /// <summary>
    ///     Tests a cell evaluating an expression when a reference cell changes.
    /// </summary>
    [Test]
    public void ReferenceCellChangedTwoCellsComplexTest()
    {
        var spreadsheet = new Spreadsheet(20, 20);
        var b10 = spreadsheet.GetCell(9, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        b10.Text = "=5+7";
        testCell.Text = "=B10";
        b10.Text = "=12+1";

        Assert.That(testCell.Value, Is.EqualTo(b10.Value));
    }

    /// <summary>
    ///    Tests a cell evaluating an expression when a reference cell changes.
    /// </summary>
    [Test]
    public void SaveBasicTest()
    {
        // Arrange
        var spreadsheet = new Spreadsheet(2, 2);
        var cellA1 = spreadsheet.GetCell(0, 0);
        var cellA2 = spreadsheet.GetCell(1, 0);

        cellA1.Text = "=5+7";
        cellA1.BackgroundColor = 45;

        cellA2.Text = "=2*8";
        cellA2.BackgroundColor = 23;

        // Set up
        var filePath = Path.GetTempFileName();
        var streamWriter = new StreamWriter(filePath);
        spreadsheet.Save(streamWriter);
        var streamReader = new StreamReader(filePath);

        // Assert
        var doc = XDocument.Load(XmlReader.Create(streamReader));
        var cells = doc.Root!.Elements("Cell").ToList();

        Assert.That(cells.Count, Is.EqualTo(2));

        var cell1 = cells[0];
        var cell2 = cells[1];

        File.Delete(filePath);

        Assert.That(cell1.Element("Name")!.Value, Is.EqualTo("A1"));
        Assert.That(cell1.Element("Text")!.Value, Is.EqualTo("=5+7"));
        Assert.That(cell1.Element("Expression")!.Value, Is.EqualTo("5+7"));
        Assert.That(cell1.Element("Value")!.Value, Is.EqualTo("12"));
        Assert.That(cell1.Element("BackgroundColor")!.Value, Is.EqualTo("45"));

        Assert.That(cell2.Element("Name")!.Value, Is.EqualTo("A2"));
        Assert.That(cell2.Element("Text")!.Value, Is.EqualTo("=2*8"));
        Assert.That(cell2.Element("Expression")!.Value, Is.EqualTo("2*8"));
        Assert.That(cell2.Element("Value")!.Value, Is.EqualTo("16"));
        Assert.That(cell2.Element("BackgroundColor")!.Value, Is.EqualTo("23"));
    }

    /// <summary>
    ///   Tests saving a spreadsheet with multiple cells.
    /// </summary>
    [Test]
    public void SaveOnlyModifiedTest()
    {
        // Arrange
        var spreadsheet = new Spreadsheet(3, 3);
        var cellA1 = spreadsheet.GetCell(0, 0);
        var cellA2 = spreadsheet.GetCell(1, 0);
        var cellB2 = spreadsheet.GetCell(1, 1);

        cellA1.Text = "=5+7";
        cellA1.BackgroundColor = 45;

        cellA2.Text = "=2*8";
        cellA2.BackgroundColor = 23;

        cellB2.Text = "=A1+A2";
        cellB2.BackgroundColor = 23;

        // Set up
        var filePath = Path.GetTempFileName();
        var streamWriter = new StreamWriter(filePath);
        spreadsheet.Save(streamWriter);
        var streamReader = new StreamReader(filePath);

        var doc = XDocument.Load(XmlReader.Create(streamReader));
        var cells = doc.Root!.Elements("Cell").ToList();

        Assert.That(cells.Count, Is.EqualTo(3));

        var cell1 = cells[0];
        var cell2 = cells[1];
        var cell3 = cells[2];

        File.Delete(filePath);

        Assert.That(cell1.Element("Name")!.Value, Is.EqualTo("A1"));
        Assert.That(cell1.Element("Text")!.Value, Is.EqualTo("=5+7"));
        Assert.That(cell1.Element("Expression")!.Value, Is.EqualTo("5+7"));
        Assert.That(cell1.Element("Value")!.Value, Is.EqualTo("12"));
        Assert.That(cell1.Element("BackgroundColor")!.Value, Is.EqualTo("45"));

        Assert.That(cell2.Element("Name")!.Value, Is.EqualTo("A2"));
        Assert.That(cell2.Element("Text")!.Value, Is.EqualTo("=2*8"));
        Assert.That(cell2.Element("Expression")!.Value, Is.EqualTo("2*8"));
        Assert.That(cell2.Element("Value")!.Value, Is.EqualTo("16"));
        Assert.That(cell2.Element("BackgroundColor")!.Value, Is.EqualTo("23"));

        Assert.That(cell3.Element("Name")!.Value, Is.EqualTo("B2"));
        Assert.That(cell3.Element("Text")!.Value, Is.EqualTo("=A1+A2"));
        Assert.That(cell3.Element("Expression")!.Value, Is.EqualTo("A1+A2"));
        Assert.That(cell3.Element("Value")!.Value, Is.EqualTo("28"));
        Assert.That(cell3.Element("BackgroundColor")!.Value, Is.EqualTo("23"));
    }

    /// <summary>
    ///   Tests saving a spreadsheet with multiple cells.
    /// </summary>
    [Test]
    public void ClearBasicTest()
    {
        var spreadsheet = new Spreadsheet(2, 2);
        var cellA1 = spreadsheet.GetCell(0, 0);
        var cellA2 = spreadsheet.GetCell(1, 0);

        cellA1.Text = "=5+9";
        cellA1.BackgroundColor = 10;

        cellA2.Text = "=2*16";
        cellA2.BackgroundColor = 20;

        spreadsheet.Clear();

        // Set up
        var filePath = Path.GetTempFileName();
        var streamWriter = new StreamWriter(filePath);
        spreadsheet.Save(streamWriter);
        var streamReader = new StreamReader(filePath);

        var doc = XDocument.Load(XmlReader.Create(streamReader));

        File.Delete(filePath);

        Assert.True(doc.Root!.IsEmpty);
    }

    /// <summary>
    ///  Tests loading a spreadsheet with a single cell.
    /// </summary>
    [Test]
    public void LoadBasicTest()
    {
        var spreadsheet = new Spreadsheet(2, 2);

        var xmlData = @"
        <Spreadsheet>
            <Cell>
                <Name>A1</Name>
                <Text>=2+3</Text>
                <Expression></Expression>
                <Value></Value>
                <BackgroundColor>34</BackgroundColor>
            </Cell>
        </Spreadsheet>";

        // Write the XML data to a temporary file
        var filePath = Path.GetTempFileName();
        File.WriteAllText(filePath, xmlData);

        using (var fileStream = File.OpenRead(filePath))
        using (var reader = new StreamReader(fileStream))
        {
            spreadsheet.Load(reader);
        }

        // delete the temp file
        File.Delete(filePath);

        var cell1 = spreadsheet.GetCell('A', 1);

        Assert.That(cell1.Text, Is.EqualTo("=2+3"));
        Assert.That(cell1.Value, Is.EqualTo("5"));
        Assert.That(cell1.Expression, Is.EqualTo("2+3"));
        Assert.That(cell1.BackgroundColor, Is.EqualTo(34));
    }

    /// <summary>
    ///  Tests loading a spreadsheet with multiple cells.
    /// </summary>
    [Test]
    public void LoadMultipleCellsTest()
    {
        var spreadsheet = new Spreadsheet(2, 2);

        var xmlData = @"
    <Spreadsheet>
        <Cell>
            <Name>A1</Name>
            <Text>=2+3</Text>
            <Expression>2+3</Expression>
            <Value>5</Value>
            <BackgroundColor>34</BackgroundColor>
        </Cell>
        <Cell>
            <Name>A2</Name>
            <Text>=3+4</Text>
            <Expression>3+4</Expression>
            <Value>7</Value>
            <BackgroundColor>45</BackgroundColor>
        </Cell>
    </Spreadsheet>";

        var filePath = Path.GetTempFileName();
        File.WriteAllText(filePath, xmlData);

        using (var fileStream = File.OpenRead(filePath))
        using (var reader = new StreamReader(fileStream))
        {
            spreadsheet.Load(reader);
        }

        File.Delete(filePath);

        var cell1 = spreadsheet.GetCell('A', 1);
        var cell2 = spreadsheet.GetCell('A', 2);

        Assert.That(cell1.Text, Is.EqualTo("=2+3"));
        Assert.That(cell1.Value, Is.EqualTo("5"));
        Assert.That(cell1.Expression, Is.EqualTo("2+3"));
        Assert.That(cell1.BackgroundColor, Is.EqualTo(34));

        Assert.That(cell2.Text, Is.EqualTo("=3+4"));
        Assert.That(cell2.Value, Is.EqualTo("7"));
        Assert.That(cell2.Expression, Is.EqualTo("3+4"));
        Assert.That(cell2.BackgroundColor, Is.EqualTo(45));
    }

    /// <summary>
    ///  Tests loading a spreadsheet with multiple cells.
    /// </summary>
    [Test]
    public void LoadEmptyCellTest()
    {
        var spreadsheet = new Spreadsheet(2, 2);

        var xmlData = @"
    <Spreadsheet>
        <Cell>
            <Name>A1</Name>
            <Text></Text>
            <Expression></Expression>
            <Value></Value>
            <BackgroundColor>0</BackgroundColor>
        </Cell>
    </Spreadsheet>";

        var filePath = Path.GetTempFileName();
        File.WriteAllText(filePath, xmlData);

        using (var fileStream = File.OpenRead(filePath))
        using (var reader = new StreamReader(fileStream))
        {
            spreadsheet.Load(reader);
        }

        File.Delete(filePath);

        var cell1 = spreadsheet.GetCell('A', 1);

        Assert.That(cell1.Text, Is.EqualTo(string.Empty));
        Assert.That(cell1.Value, Is.EqualTo(string.Empty));
        Assert.That(cell1.Expression, Is.EqualTo(string.Empty));
        Assert.That(cell1.BackgroundColor, Is.EqualTo(0));
    }

    /// <summary>
    ///  Tests loading a spreadsheet with multiple cells.
    /// </summary>
    [Test]
    public void LoadInvalidFormatTest()
    {
        var spreadsheet = new Spreadsheet(2, 2);

        var xmlData = "Invalid XML data";

        var filePath = Path.GetTempFileName();
        File.WriteAllText(filePath, xmlData);

        Assert.Throws<XmlException>(
            () =>
            {
                using (var fileStream = File.OpenRead(filePath))
                using (var reader = new StreamReader(fileStream))
                {
                    spreadsheet.Load(reader);
                }
            });

        File.Delete(filePath);
    }

    /// <summary>
    ///  Tests if the error is caught when the rowIndex is out of range.
    /// </summary>
    [Test]
    public void OutOfBoundsReferenceCellTest()
    {
        var spreadsheet = new Spreadsheet(20, 20);
        var testCell = spreadsheet.GetCell(2, 2);
        try
        {
            testCell.Text = "=B60";
        }
        catch (Exception e)
        {
            Assert.True(e is ArgumentOutOfRangeException);
        }
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
        catch
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
        var spreadsheet = new Spreadsheet(3, 3);
        var testCell = spreadsheet.GetCell(0, 0);

        testCell.Text = "=7+A1*9";

        Assert.That(testCell.Text, Is.EqualTo("Self Reference"));
    }

    /// <summary>
    /// Tests for if a cell references itself.
    /// </summary>
    [Test]
    public void ChangeAfterSelfReferenceTest()
    {
        var spreadsheet = new Spreadsheet(3, 3);
        var testCell = spreadsheet.GetCell(0, 0);

        testCell.Text = "=7+A1*9";
        testCell.Text = "6";

        Assert.That(testCell.Text, Is.EqualTo("6"));
    }

    [Test]
    public void SelfReferenceInMultipleCellsTest()
    {
        var spreadsheet = new Spreadsheet(3, 3);
        var cellA1 = spreadsheet.GetCell(0, 0);
        var cellA2 = spreadsheet.GetCell(1, 0);

        cellA1.Text = "5";
        cellA2.Text = "=A1+A2";

        Assert.That(cellA2.Text, Is.EqualTo("Self Reference"));
    }

    [Test]
    public void CellHasCircularReferencesTest_TwoCellsCircularReference()
    {
        var spreadsheet = new Spreadsheet(3, 3);
        var cellA1 = spreadsheet.GetCell(0, 0);
        var cellA2 = spreadsheet.GetCell(1, 0);

        cellA1.Text = "=A2+5";
        cellA2.Text = "=A1+5";

        bool result = (cellA1.Value == "5") && (cellA2.Text == "Circular Reference");

        Assert.IsTrue(result);
    }

    [Test]
    public void CellHasCircularReferencesTest_ThreeCellsCircularReference()
    {
        var spreadsheet = new Spreadsheet(3, 3);
        var cellA1 = spreadsheet.GetCell(0, 0);
        var cellA2 = spreadsheet.GetCell(1, 0);
        var cellA3 = spreadsheet.GetCell(2, 0);

        cellA1.Text = "=A2+5";
        cellA2.Text = "=A3+5";
        cellA3.Text = "=A1+5";


        bool result = (cellA3.Text == "Circular Reference" && cellA1.Value == "10" && cellA2.Value == "5");

        Assert.IsTrue(result);
    }

}
