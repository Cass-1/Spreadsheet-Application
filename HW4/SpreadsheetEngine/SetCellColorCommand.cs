// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

public class SetCellColorCommand : ICommand
{
    private Cell cell;
    private uint oldColor;
    private uint newColor;

    public SetCellColorCommand(Cell cell, uint newColor)
    {
        this.cell = cell;
        this.oldColor = cell.BackgroundColor;
        this.newColor = newColor;
    }

    public void Execute()
    {
        if(this.cell == null)
        {
            throw new ArgumentNullException();
        }
        this.cell.BackgroundColor = this.newColor;
    }

    public void Undo()
    {
        this.cell.BackgroundColor = this.oldColor;
    }

    public void Redo()
    {
        this.Execute();
    }
}
