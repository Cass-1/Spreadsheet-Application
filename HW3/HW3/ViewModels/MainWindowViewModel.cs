// Copyright (c) Cass Dahle. Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace HW3.ViewModels;

using System.ComponentModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using Models;
using ReactiveUI;

/// <summary>
/// The main view model for the program
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    // private attributes
    private string fibonacciNumbers;
    private TextBody textbody;

    // a wrapper for the textbody.Text
    public string TextBodyWrapper
    {
        get => this.textbody.Text;
        set => this.textbody.Text = value;
    }

    // interactions
    public Interaction<Unit, string?> AskForFileToLoad { get; }

    public Interaction<Unit, string?> AskForFileToSave { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        // Create an interaction between the view model and the view for the file to be loaded:
        this.AskForFileToLoad = new Interaction<Unit, string?>();

        // Similarly to load, there is a need to create an interaction for saving into a file:
        this.AskForFileToSave = new Interaction<Unit, string?>();

        // instantiate textbody
        this.textbody = new TextBody();

        // subscribe to the textbody broadcaster
        this.textbody.PropertyChanged += this.TextBody_PropertyChanged;
    }

    /// <summary>
    /// Signals for the UI to update whenever the textbody class is changed.
    /// </summary>
    /// <param name="sender">the object sending the signal.</param>
    /// <param name="e">any arguments.</param>
    private void TextBody_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        this.RaisePropertyChanged(nameof(this.TextBodyWrapper));
    }

    // other code...

    /// <summary>
    /// Loads text from a file and puts it into the UI.
    /// </summary>
    /// <param name="textReader">the file to read from.</param>
    private void LoadText(StreamReader textReader)
    {
        // TODO: make tests and implement
        this.TextBodyWrapper = textReader.ReadToEnd();
    }

    /// <summary>
    /// Reads text from the UI and saves it to a file.
    /// </summary>
    /// <param name="textWriter">the file to save to.</param>
    private void SaveText(StreamWriter textWriter)
    {
        // TODO: make tests and implement
        textWriter.Write(this.TextBodyWrapper);
    }

    /// <summary>
    /// Takes a string and turns it into a stream
    /// Code retrived from https://code-maze.com/dotnet-generate-stream-from-string/.
    /// </summary>
    /// <param name="str">the string to convert.</param>
    /// <param name="encoding">the encoding type.</param>
    /// <returns>a stream version of the inputted string.</returns>
    private StreamReader StringToStreamReader(string str, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        var byteArray = encoding.GetBytes(str);
        var memoryStream = new MemoryStream(byteArray);
        var streamReader = new StreamReader(memoryStream);
        return streamReader;
    }

    /// <summary>
    /// Gets or sets this is a property that will notify the user interface when changed.
    /// TODO: You need to bind this property in the .axaml file.
    /// </summary>
    private string FibonacciNumbers
    {
        get => this.fibonacciNumbers;
        set => this.RaiseAndSetIfChanged(ref this.fibonacciNumbers, value);
    }

    /// <summary>
    /// This method will be executed when the user wants to load content from a file.
    /// </summary>
    public async void LoadFromFile()
    {
        // Wait for the user to select the file to load from.
        var filePath = await this.AskForFileToLoad.Handle(default);
        if (filePath == null)
        {
            return;
        }

        // If the user selected a file, create the stream reader and load the text.
        var textReader = new StreamReader(filePath);
        this.LoadText(textReader);
        textReader.Close();
    }

    /// <summary>
    /// This method will be executed when a user wants to save content to a file.
    /// </summary>
    public async void SaveToFile()
    {
        // Wait for the user to select the file to save to.
        var filePath = await this.AskForFileToSave.Handle(default);
        if (filePath == null)
        {
            return;
        }

        // If the user selected a file create new stream writer and save the text
        var textWriter = new StreamWriter(filePath);
        this.SaveText(textWriter);
        textWriter.Close();
    }

    /// <summary>
    /// loads the first 50 numbers of the fibonacci sequence to the UI textbox.
    /// </summary>
    public void FibonacciFifty()
    {
        // get the fibonacci numbers in string form
        FibonacciTextReader fib = new FibonacciTextReader(50);
        this.FibonacciNumbers = fib.ReadToEnd();

        // convert the string to a streamreader
        var fibonacciStreamReader = this.StringToStreamReader(this.FibonacciNumbers);

        // load the text
        this.LoadText(fibonacciStreamReader);
    }

    /// <summary>
    /// loads the first 100 numbers of the fibonacci sequence to the UI textbox.
    /// </summary>
    public void FibonacciHundred()
    {
        // get the fibonacci numbers in string form
        FibonacciTextReader fib = new FibonacciTextReader(100);
        this.FibonacciNumbers = fib.ReadToEnd();

        // convert the string to a streamreader
        var fibonacciStreamReader = this.StringToStreamReader(this.FibonacciNumbers);

        // load the text
        this.LoadText(fibonacciStreamReader);
    }
}
