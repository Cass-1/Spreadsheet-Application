// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// A command to set a cell's text.
/// </summary>
public class SetCellTextCommand : ICommand
{
    private Cell cell;
    private string? oldText;
    private string? newText;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetCellTextCommand"/> class.
    /// </summary>
    /// <param name="cell">The cell that is having its text changed.</param>
    /// <param name="newText">The new text.</param>
    public SetCellTextCommand(Cell cell, string newText)
    {
        this.cell = cell;
        this.oldText = cell.Text;
        this.newText = newText;
    }

    /// <inheritdoc/>
    public void Execute()
    {
        this.cell.Text = this.newText;
    }

    /// <inheritdoc/>
    public void Undo()
    {
        this.cell.Text = this.oldText;
    }

    /// <inheritdoc/>
    public void Redo()
    {
        this.Execute();
    }
}
