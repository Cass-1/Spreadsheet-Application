// <copyright file="MainWindow.axaml.cs" company="CptS 321 Instructor">
// Copyright (c) CptS 321 Instructor. All rights reserved.
// </copyright>

using System.IO;
using Avalonia.Platform.Storage;

namespace HW3.Views;

using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using HW3.ViewModels;
using ReactiveUI;

/// <summary>
/// This class handles all necessary UI events to communicate with the view model and sub windows.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();

        // watchers for when interactions are called
        this.WhenActivated(d => d(ViewModel!.AskForFileToLoad.RegisterHandler(DoOpenFile)));
        this.WhenActivated(d => d(ViewModel!.AskForFileToSave.RegisterHandler(DoSaveFile)));
    }

    // Use the following version of DoOpenFile if you are using Avalonia 11
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
        var filePath = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
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
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Text File"
        });
        
        // return the file's absolute path
        interaction.SetOutput(file.Path.AbsolutePath);
    }
}