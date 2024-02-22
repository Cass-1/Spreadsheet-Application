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

    /// <summary>
    /// The cell's column position.
    /// </summary>
    public int ColumnIndex { get; }

    /// <summary>
    /// Gets the evaluated value of the cell. Will be the same as this.Text if this.Text doesn't start with '='.
    /// </summary>
    public string Value
    {
        get => this.Value;
        protected internal set => this.Value = value;
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
                // update text
                this.Text = value;

                // TODO: maybe need to evaluate the expression here? Not sure
                // check if the text starts with =
                if (!this.Text.StartsWith('='))
                {
                    this.Value = this.Text;
                }

                // broadcast change
                this.OnPropertyChanged(this.Text);
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