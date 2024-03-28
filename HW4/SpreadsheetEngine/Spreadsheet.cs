// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Runtime.CompilerServices;

namespace SpreadsheetEngine;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A class that represents a spreadsheet.
/// </summary>
public class Spreadsheet
{
    /// <summary>
    /// The 2D grid of cells in the spreadsheet.
    /// </summary>
    private SpreadsheetCell[,] cellGrid;

    /// <summary>
    /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// </summary>
    /// <param name="rowCount">The number of rows to have.</param>
    /// <param name="columnCount">the number of columns to have.</param>
    public Spreadsheet(int rowCount, int columnCount)
    {
        // check if arguments are out of range
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rowCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(columnCount);

        // initalize row and column count
        this.RowCount = rowCount;
        this.ColumnCount = columnCount;

        // initalize all cells as null
        this.cellGrid = new SpreadsheetCell[this.RowCount, this.ColumnCount];

        // populate cell grid
        foreach (var rowIndex in Enumerable.Range(0, this.RowCount))
        {
            foreach (var columnIndex in Enumerable.Range(0, this.ColumnCount))
            {
                // set the cell to an actual instance of a cell
                this.cellGrid[rowIndex, columnIndex] = new SpreadsheetCell(rowIndex, columnIndex);

                // subscribe to each cell
                this.cellGrid[rowIndex, columnIndex].PropertyChanged += this.CellPropertyChanged;
            }
        }
    }

    /// <summary>
    /// An event that occurs whenever a cell's property is changed. Used to tell the UI to update.
    /// </summary>
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global", Justification = "<This event is needed>")]
    public event PropertyChangedEventHandler CellPropertyChangedEvent = (_, _) => { };

    /// <summary>
    /// Gets the number of rows.
    /// </summary>
    public int RowCount { get; }

    /// <summary>
    /// Gets the number of comumns.
    /// </summary>
    public int ColumnCount { get; }

    /// <summary>
    /// Returns the cell at a given row and column index.
    /// </summary>
    /// <param name="rowIndex">the row index of the cell.</param>
    /// <param name="colIndex">the column index of the cell.</param>
    /// <returns>A cell.</returns>
    public Cell GetCell(int rowIndex, int colIndex)
    {
        if (rowIndex >= this.RowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex) + " is out of bounds");
        }

        if (colIndex >= this.ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(colIndex) + " is out of bounds");
        }

        return this.cellGrid[rowIndex, colIndex];
    }

    /// <summary>
    /// Returns the cell at a specified location, using the spreadsheet naming convention.
    /// </summary>
    /// <param name="col">A character that represents the column in the spreadsheet UI.</param>
    /// <param name="row">A number that represents the row in the spreadsheet UI.</param>
    /// <returns>A cell in the spreadsheet.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The argument(s) given are outside the bounds of
    /// the spreadsheet.</exception>
    public Cell GetCell(char col, int row)
    {
        var rowIndex = row - 1;
        var colIndex = col - 'A';
        if (rowIndex >= this.RowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex) + " is out of bounds");
        }

        if (colIndex >= this.ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(colIndex) + " is out of bounds");
        }

        return this.cellGrid[rowIndex, colIndex];
    }

    /// <summary>
    /// The function that is called whenever a cell has been changed.
    /// </summary>
    /// <param name="sender">The object that send the property changed event.</param>
    /// <param name="e">The event arguments.</param>
    private void CellPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // get the spreadsheet cell that sent the event
        var cell = (SpreadsheetCell)sender!;

        // if the cell's text is an expression
        if (cell.Text.StartsWith('='))
        {
            // The location of the cell to get the value from.
            var expression = cell.Text.TrimStart('=');
            if (expression == cell.expression)
            {
                cell.EvaluateExpression();
                return;
            }

            cell.SetExpression(expression);

            List<string> variables = cell.GetReferencedCellNames();

            foreach (var variable in variables)
            {
                // this try catch block prevents errors from happening in the spreadsheet when we are still typing
                // and before we have finished entering a value.
                var rowCharacter = variable.Substring(1, variable.Length - 1);
                var columnCharacter = variable[0];
                if (rowCharacter == columnCharacter.ToString())
                {
                    return;
                }
                try
                {
                    // get the reference cell
                    Cell referencedCell = this.GetCell(int.Parse(rowCharacter) - 1, columnCharacter - 'A');

                    cell.AddReferenceCell(referencedCell);
                }
                catch (Exception exception)
                {
                    return;
                }
            }
            cell.EvaluateExpression();
        }

        // if the cell's text is not an expression
        else
        {
            cell.Value = cell.Text;
        }

        // invoke the property changed event to update the UI
        // this.CellPropertyChangedEvent.Invoke(sender, e);
    }

    private class SpreadsheetCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
        /// </summary>
        /// <param name="rowIndex">The cell's row position in the spreadsheet.</param>
        /// <param name="columnIndex">The cell's column position in the spreadsheet.</param>
        public SpreadsheetCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
        }

        public override string Value
        {
            get => this.value;
            protected internal set
            {


                this.SetandNotifyIfChanged(ref this.value, value);


            }
        }
        public void EvaluateExpression()
        {
            this.expressionTree = new ExpressionTree(this.expression);

            // TODO: set the reference variables
            foreach (var cell in this.referencedCells)
            {
                if (cell.Value != string.Empty)
                {
                    this.expressionTree.SetVariable(cell.name, double.Parse(cell.Value));
                }

            }

            this.Value = this.expressionTree.Evaluate().ToString();
            // SetandNotifyIfChanged(ref this.value, this.expressionTree.Evaluate().ToString());
            // this.Value = this.expressionTree.Evaluate().ToString();
        }

        protected void OnReferenceChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.EvaluateExpression();
        }

        protected void UnsubscribeFromReferencedCells()
        {
            if (this.referencedCells.Count > 0)
            {
                foreach (Cell cell in this.referencedCells)
                {
                    cell.PropertyChanged -= this.OnReferenceChanged;
                }

                this.referencedCells.Clear();
            }
        }

        public void SetExpression(string expression)
        {
            this.UnsubscribeFromReferencedCells();
            this.expression = expression;
            this.EvaluateExpression();
        }

        public List<string> GetReferencedCellNames()
        {
            return this.expressionTree.GetVariableNames();
        }

        public void AddReferenceCell(Cell cell)
        {
            this.referencedCells.Add(cell);
            cell.PropertyChanged += this.OnReferenceChanged;
        }
    }
}
