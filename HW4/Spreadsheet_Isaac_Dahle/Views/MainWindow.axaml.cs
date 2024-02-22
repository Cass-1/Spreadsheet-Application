namespace HW4.Views;

using Avalonia.ReactiveUI;
using HW4.ViewModels;

/// <summary>
/// The maid window of the UI
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

        // code for initalizing the SpreadsheetDataGrid
        this.DataContextChanged += (sender, args) =>
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                // SpreadsheetDataGrid is defined in MainWindow.axaml
                viewModel.InitializeDataGrid(this.SpreadsheetDataGrid);
            }
        };
    }
}