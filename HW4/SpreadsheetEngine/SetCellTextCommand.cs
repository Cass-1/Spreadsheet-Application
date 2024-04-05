// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

public class SetCellTextCommand : ICommand
{
    private Cell cell;
    private string oldText;
    private string newText;

    public SetCellTextCommand(Cell cell, string newText)
    {
        this.oldText = cell.Text;
        this.newText = newText;
    }

    public void Execute()
    {
        this.cell.Text = this.newText;
    }

    public void Undo()
    {
        this.cell.Text = this.oldText;
    }

    public void Redo()
    {
        this.Execute();
    }
}
