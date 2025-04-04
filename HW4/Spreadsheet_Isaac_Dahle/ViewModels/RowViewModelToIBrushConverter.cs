// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace HW4.ViewModels;

/// <summary>
/// Converts a RowViewModel to a IBrush.
/// </summary>
public class RowViewModelToIBrushConverter : IValueConverter
{
    /// <summary>
    /// A static instance of a RowViewModelToIBrushConverter.
    /// </summary>
    public static readonly RowViewModelToIBrushConverter Instance = new();

    private RowViewModel? currentRow;
    private int cellCounter;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // if the converter used for the wrong type throw an exception
        if (value is not RowViewModel row)
        {
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        // NOTE: Rows are rendered from column 0 to n and in order
        if (this.currentRow != row)
        {
            this.currentRow = row;
            this.cellCounter = 0;
        }

        var brush = this.currentRow.Cells[this.cellCounter].IsSelected
            ? new SolidColorBrush(0xff3393df)
            : new SolidColorBrush(this.currentRow.Cells[this.cellCounter].BackgroundColor);
        this.cellCounter++;
        if (this.cellCounter >= this.currentRow.Cells.Count)
        {
            this.currentRow = null;
        }

        return brush;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
