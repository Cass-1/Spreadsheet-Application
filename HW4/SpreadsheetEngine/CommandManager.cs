// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

public class CommandManager
{
    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    public bool CanUndo => this.undoStack.Count > 0;
    public bool CanRedo => this.redoStack.Count > 0;

    /// <summary>
    /// Executes a Command.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        this.undoStack.Push(command);

    }

    /// <summary>
    /// Undoes the command on the top of the undo stack.
    /// </summary>
    public void Undo()
    {
        if (this.undoStack.Count > 0)
        {
            ICommand command = this.undoStack.Pop();
            command.Undo();
            this.redoStack.Push(command);
        }
    }

    /// <summary>
    /// Redo's the command on the top of the redo stack.
    /// </summary>
    public void Redo()
    {
        if (this.redoStack.Count > 0)
        {
            ICommand command = this.redoStack.Pop();
            command.Redo();
            this.undoStack.Push(command);
        }
    }
}
