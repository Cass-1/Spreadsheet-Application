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

}