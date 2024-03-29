// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine_Tests;

using SpreadsheetEngine;

/// <summary>
/// Tests for the spreadsheet class.
/// </summary>
public class SpreadsheetTests
{
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
            spreadsheet.GetCell(1, 0);
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
            spreadsheet.GetCell(0, 1);
        }
        catch (Exception e)
        {
            Assert.That(e is ArgumentOutOfRangeException);
        }
    }

    /// <summary>
    /// Tests if a cell's value is updated when the text is.
    /// </summary>
    [Test]
    public void CellPropertyChangedBasicTest()
    {
        Spreadsheet spreadsheet = new Spreadsheet(10, 10);
        var cell = spreadsheet.GetCell(1, 1);
        cell.Text = "testing";

        Assert.That(cell.Value, Is.EqualTo(cell.Text));
    }

    /// <summary>
    /// Tests if when the text is a expression to another cell if value is set to the other cell's value.
    /// </summary>
    [Test]
    public void CellPropertyChangedExpressionIsOtherCellTest1()
    {
        Spreadsheet spreadsheet = new Spreadsheet(10, 10);
        var referenceCell = spreadsheet.GetCell(1, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        referenceCell.Text = "testing";
        testCell.Text = "=B2";

        Assert.That(referenceCell.Value, Is.EqualTo(testCell.Value));
    }

    /// <summary>
    /// Tests if when the text is a expression to another cell if value is set to the other cell's value.
    /// </summary>
    [Test]
    public void CellPropertyChangedExpressionIsOtherCellTest2()
    {
        Spreadsheet spreadsheet = new Spreadsheet(20, 20);
        var referenceCell = spreadsheet.GetCell(9, 1);
        var testCell = spreadsheet.GetCell(2, 2);

        referenceCell.Text = "testing";
        testCell.Text = "=B10";

        Assert.That(referenceCell.Value, Is.EqualTo(testCell.Value));
    }

    /// <summary>
    /// TEests when a cell references a cell that doesn't exist.
    /// </summary>
    [Test]
    public void CellPropertyChangedExpressionIsOtherCellTestOutOfBounds()
    {
        Spreadsheet spreadsheet = new Spreadsheet(20, 20);
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
}
