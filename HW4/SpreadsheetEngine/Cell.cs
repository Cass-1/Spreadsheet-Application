using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpreadsheetEngine;

public abstract class Cell : INotifyPropertyChanged
{
    
    public Cell(int rowIndex, int columnIndex)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
    }
    // TODO: implement getters
    public int RowIndex { get; set; }
    public int ColumnIndex { get; set; }

    protected string Text { get; }
    protected string Value { get; }
    
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