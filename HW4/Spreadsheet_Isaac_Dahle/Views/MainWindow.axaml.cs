// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Immutable;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using AvaloniaColorPicker;
using HW4.ViewModels;
using ReactiveUI;

namespace HW4.Views;

// ReSharper disable once RedundantNameQualifier

/// <summary>
///     The maid window of the UI.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
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

        // watchers for when interactions are called
        this.WhenActivated(d => d(this.ViewModel!.AskForFileToLoad.RegisterHandler(this.DoOpenFile)));
        this.WhenActivated(d => d(this.ViewModel!.AskForFileToSave.RegisterHandler(this.DoSaveFile)));

    }

    /// <summary>
    /// Opens a dialog to select a file which will be used to load content.
    /// </summary>
    /// <param name="interaction">Defines the Input/Output necessary for this workflow to complete successfully.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    private async Task DoOpenFile(InteractionContext<Unit, string?> interaction)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // List of filtered types
        var fileType = new FilePickerFileType("txt");
        var fileTypes = new List<FilePickerFileType>();
        fileTypes.Add(fileType);

        // Start async operation to open the dialog.
        var filePath = await topLevel.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
        {
          Title = "Open Text File",
          AllowMultiple = false,
          FileTypeFilter = fileTypes,
        });

        // return the file's absolute path
        interaction.SetOutput(filePath.Count == 1 ? filePath[0].Path.AbsolutePath : null);
    }

    /// <summary>
    /// Opens a dialog to select a file or create a new file in which content will be saved.
    /// </summary>
    /// <param name="interaction">Defines the Input/Output necessary for this workflow to complete successfully.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    private async Task DoSaveFile(InteractionContext<Unit, string?> interaction)
    {
        // this code is from Avalonia's website https://docs.avaloniaui.net/docs/basics/user-interface/file-dialogs
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog. This opens your sysem's default file explorer
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(
            new FilePickerSaveOptions
        {
            Title = "Save Text File",
        });

        // return the file's absolute path
        interaction.SetOutput(file.Path.AbsolutePath);
    }


    private async void OpenColorPickerDialog()
    {
        var colorPickerDialog = new ColorPickerWindow();
        var result = await colorPickerDialog.ShowDialog(this); // Show the dialog window as a modal dialog

        if (result.HasValue)
        {
            // OK button was clicked, handle the selected color
            var selectedColor = colorPickerDialog.Color;

            // Do something with the selected color
            ((MainWindowViewModel)this.DataContext!).ElementBrush = new ImmutableSolidColorBrush(selectedColor);
        }
    }

    private void Button_Click(object? sender, RoutedEventArgs routedEventArgs)
    {
        this.OpenColorPickerDialog();
    }
}
