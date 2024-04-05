// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

public interface ICommand
{
    public void Execute()
    {
    }

    public void Undo()
    {
    }

    public void Redo()
    {
    }
}
