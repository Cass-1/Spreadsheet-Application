// Copyright (c) Cass Dahle. Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace HW3.Models;

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// A class to store the text the user enters in the textbox.
/// </summary>
public class TextBody : INotifyPropertyChanged
{
    // the text the user entered
    private string text;

    // public facing text
    public string Text
    {
        get => this.text;
        set
        {
            this.text = value;
            this.OnPropertyChanged(this.Text);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TextBody"/> class.
    /// </summary>
    /// <param name="text">text to be stored.</param>
    public TextBody(string text = "Enter text")
    {
        this.Text = text;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
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
