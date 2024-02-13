// <copyright file="MainWindow.axaml.cs" company="CptS 321 Instructor">
// Copyright (c) CptS 321 Instructor. All rights reserved.
// </copyright>

namespace HW3.ViewModels;

using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

public class MainWindowViewModel : ViewModelBase
{
    private string fibonacciNumbers;

    public MainWindowViewModel()
    {

        // Create an interaction between the view model and the view for the file to be loaded:
        AskForFileToLoad = new Interaction<Unit, string?>();

        // Similarly to load, there is a need to create an interaction for saving into a file:
        // TODO: Your code goes here.
        AskForFileToSave = new Interaction<Unit, string?>();

    }

    /// <summary>
    /// This is a property that will notify the user interface when changed.
    /// TODO: You need to bind this property in the .axaml file
    /// </summary>
    public string FibonacciNumbers
    {
        get => fibonacciNumbers;
        private set => this.RaiseAndSetIfChanged(ref fibonacciNumbers, value);
    }

    /// <summary>
    /// This method will be executed when the user wants to load content from a file.
    /// </summary>
    public async void LoadFromFile()
    {
        // Wait for the user to select the file to load from.
        var filePath = await AskForFileToLoad.Handle(default);
        if (filePath == null) return;

        // If the user selected a file, create the stream reader and load the text.
        var textReader = new StreamReader(filePath);
        LoadText(textReader);
        textReader.Close();
    }
    
    /// <summary>
    /// This method will be executed when a user wants to save content to a file.
    /// </summary>
    public async void SaveToFile()
    {
        // Wait for the user to select the file to save to.
        var filePath = await AskForFileToSave.Handle(default);
        if (filePath == null) return;
        
        // If the user selected a file create new stream writer and save the text
        var textWriter = new StreamWriter(filePath);
        SaveText(textWriter);
        textWriter.Close();
    }

    public Interaction<Unit, string?> AskForFileToLoad { get; }
    public Interaction<Unit, string?> AskForFileToSave { get; }

    // other code...
}