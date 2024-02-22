namespace SpreadsheetEngine;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class SpreadsheetCell : Cell, INotifyPropertyChanged
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
    /// </summary>
    /// <param name="rowIndex">The cell's row position in the spreadsheet</param>
    /// <param name="columnIndex">The cell's column position in the spreadsheet</param>
    public SpreadsheetCell(int rowIndex, int columnIndex)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
    }

    /// <summary>
    /// The cell's row position.
    /// </summary>
    public int RowIndex { get; }

    public int ColumnIndex { get; }
    protected string Text
    {
        get => Text;
        set
        {
            if (value != Text)
            {
                Text = value;
                this.OnPropertyChanged(Text);
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Function that broadcasts nessisary information when a property is changed.
    /// </summary>
    /// <param name="propertyName">The name of the property that was changed</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    // {
    //     if (EqualityComparer<T>.Default.Equals(field, value))
    //     {
    //         return false;
    //     }
    //
    //     field = value;
    //     this.OnPropertyChanged(propertyName);
    //     return true;
    // }
}