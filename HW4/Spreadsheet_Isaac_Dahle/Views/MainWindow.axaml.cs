namespace HW4.Views;

using System;
using System.Reactive.Linq;
using Avalonia.ReactiveUI;
using HW4.ViewModels;
using ReactiveUI;

/// <summary>
/// The maid window of the UI
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    /// <summary>
    /// A function to initialize the data grid.
    /// </summary>
    private void InitializeDataGrid()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();
        this.WhenAnyValue(x => x.DataContext)
            .Where(dataContext => dataContext != null)
            .Subscribe(
                dataContext =>
            {
                if (dataContext is MainWindowViewModel)
                {
                    this.InitializeDataGrid();
                }
            });
    }

}