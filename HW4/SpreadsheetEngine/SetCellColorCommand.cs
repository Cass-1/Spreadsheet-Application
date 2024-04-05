// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// A command that changes the color of a cell.
/// </summary>
public class SetCellColorCommand : ICommand
{
    private Cell cell;
    private uint oldColor;
    private uint newColor;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetCellColorCommand"/> class.
    /// </summary>
    /// <param name="cell">The cell that color's being set.</param>
    /// <param name="newColor">The new color.</param>
    public SetCellColorCommand(Cell cell, uint newColor)
    {
        this.cell = cell;
        this.oldColor = cell.BackgroundColor;
        this.newColor = newColor;
    }

    /// <inheritdoc/>
    public void Execute()
    {
        if (this.cell == null)
        {
            throw new ArgumentNullException();
        }

        this.cell.BackgroundColor = this.newColor;
    }

    /// <inheritdoc/>
    public void Undo()
    {
        this.cell.BackgroundColor = this.oldColor;
    }

    /// <inheritdoc/>
    public void Redo()
    {
        this.Execute();
    }
}
