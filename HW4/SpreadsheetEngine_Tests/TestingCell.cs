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

    public string? GetText() => this.text;

    public void SetText(string? str)
    {
        this.text = str;
    }

    public string? GetValue()
    {
        return this.value;
    }

    public void SetValue(string? str)
    {
        this.value = str;
    }
}
