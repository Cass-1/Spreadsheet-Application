// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using SpreadsheetEngine;

namespace SpreadsheetEngine_Tests;

/// <summary>
/// This allows testing of protected methods.
/// </summary>
public class TestingCell : Cell
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

    /// <summary>
    /// Gets the cell's text.
    /// </summary>
    /// <returns>The cell's text.</returns>
    public string? GetText() => this.text;

    /// <summary>
    /// Sets the cell's text.
    /// </summary>
    /// <param name="str">The string to set the text to.</param>
    public void SetText(string? str)
    {
        this.text = str;
    }

    /// <summary>
    /// Gets the cell's value.
    /// </summary>
    /// <returns>The cell's value.</returns>
    public string? GetValue()
    {
        return this.value;
    }

    /// <summary>
    /// Sets the cell's value.
    /// </summary>
    /// <param name="str">The string to set the value to.</param>
    public void SetValue(string? str)
    {
        this.value = str;
    }
}
