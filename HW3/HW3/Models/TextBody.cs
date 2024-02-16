using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HW3.Models;

/// <summary>
/// A class to store the text the user enters in the textbox
/// </summary>
public class TextBody : INotifyPropertyChanged
{

    // the text the user entered
    private string text;
    public string Text
    {
        get => text;
        set
        {
            text = value;
            OnPropertyChanged(Text);
        }
    }

    // constructor
    public TextBody(string text = "Enter text")
    {
        this.Text = text;
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