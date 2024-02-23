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
        public string Value
        {
            get => this.Value;
            private set => this.Value = value;
        }

        /// <summary>
        /// Gets or sets the actual text entered into the cell.
        /// </summary>
        protected string Text
        {
            get => this.Text;
            set
            {
                // only update if the value is different from this.Text
                if (value != this.Text)
                {
                    // check if the text starts with =
                    if (this.Text.StartsWith('='))
                    {
                        // TODO: evaluate value
                    }
                    else
                    {
                        this.Value = this.Text;
                    }
                    
                    // update text
                    this.Text = value;
    
                    // broadcast change
                    this.OnPropertyChanged(this.Text);
                }
            }
        }
    }

}