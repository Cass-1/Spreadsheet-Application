// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// A class that manages all the commands.
/// </summary>
public class CommandManager
{
    private readonly Stack<ICommand> undoStack = new();
    private readonly Stack<ICommand> redoStack = new();

    /// <summary>
    /// Gets a value indicating whether checks if a command can be undone.
    /// </summary>
    public bool CanUndo => this.undoStack.Count > 0;

    /// <summary>
    /// Gets a value indicating whether checks if a command can be redone.
    /// </summary>
    public bool CanRedo => this.redoStack.Count > 0;

    /// <summary>
    ///     Executes a Command.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        this.undoStack.Push(command);
    }

    /// <summary>
    ///     Undoes the command on the top of the undo stack.
    /// </summary>
    public void Undo()
    {
        if (this.undoStack.Count > 0)
        {
            var command = this.undoStack.Pop();
            command.Undo();
            this.redoStack.Push(command);
        }
    }

    /// <summary>
    ///     Redo's the command on the top of the redo stack.
    /// </summary>
    public void Redo()
    {
        if (this.redoStack.Count > 0)
        {
            var command = this.redoStack.Pop();
            command.Redo();
            this.undoStack.Push(command);
        }
    }

    /// <summary>
    /// Clears the redo stack.
    /// </summary>
    public void ClearRedoStack()
    {
        this.redoStack.Clear();
    }

    /// <summary>
    /// Clears the undo stack.
    /// </summary>
    public void ClearUndoStack()
    {
        this.undoStack.Clear();
    }

    /// <summary>
    /// Clears both the undo and the redo stack.
    /// </summary>
    public void ClearAll()
    {
        this.ClearRedoStack();
        this.ClearUndoStack();
    }
}
