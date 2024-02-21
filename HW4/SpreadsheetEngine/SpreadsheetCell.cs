namespace SpreadsheetEngine;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class SpreadsheetCell : Cell, INotifyPropertyChanged
{
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