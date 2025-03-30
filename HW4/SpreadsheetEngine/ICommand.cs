// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// An interface for commands.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Executes the command.
    /// </summary>
    public void Execute()
    {
    }

    /// <summary>
    /// Undoes a command.
    /// </summary>
    public void Undo()
    {
    }

    /// <summary>
    /// Redoes a command.
    /// </summary>
    public void Redo()
    {
    }
}
