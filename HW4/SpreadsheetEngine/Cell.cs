using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace SpreadsheetEngine;

public abstract class Cell : INotifyPropertyChanged
{

    private string _text;
    private string _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="rowIndex">The cell's row index in a spreadsheet.</param>
    /// <param name="columnIndex">The cell's column index in a spreadsheet.</param>
    public Cell(int rowIndex, int columnIndex)
    {
        this.RowIndex = rowIndex;
        this.ColumnIndex = columnIndex;
        this._text = String.Empty;
        this._value = String.Empty;
    }

    public int RowIndex { get; }
    public int ColumnIndex { get; }
    
    // TODO: implement text after TDD
    protected virtual string Text
    {
        get => _text;
        set
        {
            if (this.Text != value)
            {
                _text = value;
                OnPropertyChanged(this.Text);
            }
        }
    }

    // TODO: implement value after TDD
    protected virtual string Value
    {
        get => _value;
        set
        {
            throw new ReadOnlyException(nameof(Value));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}