namespace SpreadsheetEngine;

using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

public abstract class Cell : INotifyPropertyChanged
{

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="rowIndex">The cell's row index in a spreadsheet.</param>
    /// <param name="columnIndex">The cell's column index in a spreadsheet.</param>
    public Cell(int rowIndex, int columnIndex)
    {
        this.RowIndex = rowIndex;
        this.ColumnIndex = columnIndex;
        this.text = string.Empty;
        this.value = string.Empty;
    }

    public int RowIndex { get; }
    public int ColumnIndex { get; }

    protected string text;
    protected string value;
    
    public string Text
    {
        get => text;
        set
        {
            this.SetandNotifyIfChanged(ref this.text, value);
        }
    }

    public virtual string Value { get; protected internal set; }

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetandNotifyIfChanged<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }
}