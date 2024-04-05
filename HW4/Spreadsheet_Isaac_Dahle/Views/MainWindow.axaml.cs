// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Immutable;
using Avalonia.ReactiveUI;
using AvaloniaColorPicker;
using HW4.ViewModels;

namespace HW4.Views;

// ReSharper disable once RedundantNameQualifier

/// <summary>
///     The maid window of the UI.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    private async void OpenColorPickerDialog()
    {
        var colorPickerDialog = new ColorPickerWindow();
        var result = await colorPickerDialog.ShowDialog(this); // Show the dialog window as a modal dialog

        if (result.HasValue)
        {
            // OK button was clicked, handle the selected color
            var selectedColor = colorPickerDialog.Color;

            // Do something with the selected color
            ((MainWindowViewModel)this.DataContext)!.ElementBrush = new ImmutableSolidColorBrush(selectedColor);
        }

        // Cancel button was clicked or dialog was closed, handle accordingly
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.OpenColorPickerDialog();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainWindow" /> class.
    /// </summary>
    public MainWindow()
    {
        // basic initalization
        this.InitializeComponent();

        // add headers for the rows
        this.SpreadsheetDataGrid.HeadersVisibility = DataGridHeadersVisibility.All;

        // DataContext = new MainWindowViewModel();
        // code for initalizing the SpreadsheetDataGrid
        this.DataContextChanged += (_, _) =>
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                // SpreadsheetDataGrid is defined in MainWindow.axaml
                viewModel.InitializeDataGrid(this.SpreadsheetDataGrid);
            }
        };

        // code for getting the row headers
        this.SpreadsheetDataGrid.LoadingRow += (_, args) => { args.Row.Header = args.Row.GetIndex() + 1; };
    }

    private void Undo(object? sender, RoutedEventArgs e)
    {
        ((MainWindowViewModel)this.DataContext).CommandManager.Undo();
    }

    private void Redo(object? sender, RoutedEventArgs e)
    {
        ((MainWindowViewModel)this.DataContext).CommandManager.Redo();
    }
}
