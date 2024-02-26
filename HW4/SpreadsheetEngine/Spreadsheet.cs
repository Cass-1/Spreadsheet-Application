using System.ComponentModel;
using System.Linq.Expressions;
using System.Transactions;

namespace SpreadsheetEngine;

public class Spreadsheet
{

    public event PropertyChangedEventHandler CellPropertyChangedEvent = (sender, e) => { };

    /// <summary>
    /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// </summary>
    /// <param name="rowCount">The number of rows to have.</param>
    /// <param name="columnCount">the number of columns to have.</param>
    public Spreadsheet(int rowCount, int columnCount)
    {
        // check if arguments are out of range
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero<int>(rowCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero<int>(columnCount);

        // initalize row and column count
        this.RowCount = rowCount;
        this.ColumnCount = columnCount;

        // initalize all cells as null
        this._cellGrid = new SpreadsheetCell[this.RowCount, this.ColumnCount];

        // populate cell grid
        foreach (var rowIndex in Enumerable.Range(0, this.RowCount))
        {
            foreach (var columnIndex in Enumerable.Range(0, this.ColumnCount))
            {
                // set the cell to an actual instance of a cell
                this._cellGrid[rowIndex, columnIndex] = new SpreadsheetCell(rowIndex,columnIndex);

                // subscribe to each cell
                this._cellGrid[rowIndex, columnIndex].PropertyChanged += this.CellPropertyChanged;
            }
        }
    }

    /// <summary>
    /// Gets the number of rows.
    /// </summary>
    public int RowCount { get; }

    /// <summary>
    /// Gets the number of comumns.
    /// </summary>
    public int ColumnCount { get; }

    /// <summary>
    /// The 2D grid of cells in the spreadsheet.
    /// </summary>
    private SpreadsheetCell[,] _cellGrid;

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
            string rowCharacter = string.Empty;
            char columnCharacter = '\0';

            try
            {
                rowCharacter = expression.Substring(1, expression.Length - 1);
                columnCharacter = expression[0];

                // evaluate the expression to 
                cell.Value = this.GetCell(int.Parse(rowCharacter) - 1, columnCharacter - 'A').Text;

            }
            catch (Exception exception)
            {
                
                // if (exception is ArgumentOutOfRangeException)
                // {
                //     // inputted location is outside the bounds of the _cellGrid
                //     cell.Text = "error";
                //     cell.Value = "error";
                // }
                // else if (exception is IndexOutOfRangeException)
                // {
                //     // inputted number is outside the expression array
                // }
            }
        }

        // if the cell's text is not an expression
        else
        {
            cell.Value = cell.Text;
        }

        // invoke the property changed event to update the UI
        this.CellPropertyChangedEvent.Invoke(sender, e);

    }

    private string GetValue(int row, char column)
    {
        int rowIndex = row - '1';
        int columnIndex = column - 'A';
        return string.Empty;
    }

    /// <summary>
    /// Returns the cell at a given row and column index.
    /// </summary>
    /// <param name="rowIndex">the row index of the cell.</param>
    /// <param name="colIndex">the column index of the cell.</param>
    /// <returns>A cell</returns>
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

        return this._cellGrid[rowIndex, colIndex];
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

        return this._cellGrid[rowIndex, colIndex];
    }

    
    private class SpreadsheetCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
        /// </summary>
        /// <param name="rowIndex">The cell's row position in the spreadsheet</param>
        /// <param name="columnIndex">The cell's column position in the spreadsheet</param>
        public SpreadsheetCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
        }

        public override string Value
        {
            get => this.value;
            protected internal set => SetandNotifyIfChanged(ref this.value, value);
        }
    }

}