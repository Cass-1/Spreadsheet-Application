// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace HW4.Views;

using Avalonia.Controls;
using Avalonia.ReactiveUI;

// ReSharper disable once RedundantNameQualifier
using HW4.ViewModels;

/// <summary>
/// The maid window of the UI.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        // basic initalization
        this.InitializeComponent();

        // add headers for the rows
        this.SpreadsheetDataGrid.HeadersVisibility = DataGridHeadersVisibility.All;

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
        this.SpreadsheetDataGrid.LoadingRow += (_, args) =>
        {
            args.Row.Header = args.Row.GetIndex() + 1;
        };
    }
}
