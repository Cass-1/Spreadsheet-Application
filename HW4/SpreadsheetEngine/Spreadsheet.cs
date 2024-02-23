using System.ComponentModel;
using System.Transactions;

namespace SpreadsheetEngine;

public class Spreadsheet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// The Constructor
    /// </summary>
    public Spreadsheet(int rowCount, int columnCount)
    {
        // initalize row and column count
        this.RowCount = rowCount;
        this.ColumnCount = columnCount;
        
        // initalize cell grid, starting with index of 1
        foreach (var rowIndex in Enumerable.Range(0, this.RowCount))
        {
            foreach (var columnIndex in Enumerable.Range(0, this.ColumnCount))
            {
                // set each cell's row and column index
                cellGrid[rowIndex][columnIndex].RowIndex = rowIndex + 1;
                cellGrid[rowIndex][columnIndex].ColumnIndex = columnIndex + 1;
                
                // TODO: subscribe to each cell
                cellGrid[rowIndex][columnIndex].PropertyChanged += this.CellPropertyChanged;
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
    /// The grid of cells in the spreadsheet.
    /// </summary>
    public Cell[][] cellGrid;

    private void CellPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the cell at a given row and column index
    /// </summary>
    /// <param name="rowIndex">the row index of the cell, starting at 1</param>
    /// <param name="colInxex">the column index of the cell, starting at 1</param>
    /// <returns>A cell</returns>
    private Cell GetCell(int rowIndex, int colInxex)
    {
        return cellGrid[rowIndex - 1][colInxex - 1];
    }
    
    private class SpreadsheetCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
        /// </summary>
        /// <param name="rowIndex">The cell's row position in the spreadsheet</param>
        /// <param name="columnIndex">The cell's column position in the spreadsheet</param>
        public SpreadsheetCell(int rowIndex, int columnIndex) : base(rowIndex, columnIndex)
        {
        }

        /// <summary>
        /// Gets the evaluated value of the cell. Will be the same as this.Text if this.Text doesn't start with '='.
        /// </summary>
        public string Value { get; set; }
    }

}