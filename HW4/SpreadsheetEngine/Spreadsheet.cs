using System.ComponentModel;
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
        this.CellGrid = new SpreadsheetCell[this.RowCount,this.ColumnCount];

        // populate cell grid
        foreach (var rowIndex in Enumerable.Range(0, this.RowCount))
        {
            foreach (var columnIndex in Enumerable.Range(0, this.ColumnCount))
            {
                // set the cell to an actual instance of a cell
                this.CellGrid[rowIndex, columnIndex] = new SpreadsheetCell(rowIndex,columnIndex);

                // subscribe to each cell
                this.CellGrid[rowIndex, columnIndex].PropertyChanged += this.CellPropertyChanged;
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
    private SpreadsheetCell[,] CellGrid;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void CellPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // TODO: imlement this method
    }

    /// <summary>
    /// Returns the cell at a given row and column index.
    /// </summary>
    /// <param name="rowIndex">the row index of the cell.</param>
    /// <param name="colInxex">the column index of the cell.</param>
    /// <returns>A cell</returns>
    public Cell GetCell(int rowIndex, int colInxex)
    {
        if (rowIndex >= this.RowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex) + " is out of bounds");
        }
        
        if (colInxex >= this.ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(colInxex) + " is out of bounds");
        }
        
        return this.CellGrid[rowIndex, colInxex];
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