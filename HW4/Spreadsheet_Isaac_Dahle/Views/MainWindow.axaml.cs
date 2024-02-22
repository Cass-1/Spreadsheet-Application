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
        this.InitializeComponent();
        this.WhenAnyValue(x => x.DataContext)
            .Where(dataContext => dataContext != null)
            .Subscribe(
                dataContext =>
            {
                if (dataContext is MainWindowViewModel)
                {
                    InitializeDataGrid(SpreadsheetDataGrid);
                }
            });
    }


    public void InitializeDataGrid(DataGrid dataGrid)
    {
        if (_isInitialized) return;

        // initialize A to Z columns headers since these are indexed this is not a behavior supported by default
        var columnCount = 'Z' - 'A' + 1;
        foreach (var columnIndex in Enumerable.Range(0, columnCount))
        {
            // for each column we will define the header text and
            // the binding to use
            var columnHeader = (char) ('A' + columnIndex);
            var columnTemplate = new DataGridTemplateColumn
            {
                Header = columnHeader,
                CellTemplate = new FuncDataTemplate<IEnumerable<Cell>>((value, namescope) =>
                    new TextBlock
                    {
                        [!TextBlock.TextProperty] = new Binding($"[{columnIndex}].Value"),
                        TextAlignment = TextAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Padding = Thickness.Parse("5,0,5,0")
                    }),
                CellEditingTemplate = new FuncDataTemplate<IEnumerable<Cell>>((value, namescope) =>
                    new TextBox
                    {
                        [!TextBox.TextProperty] = new Binding($"[{columnIndex}].Text")
                    }),
            };
            dataGrid.Columns.Add(columnTemplate);
        }


        _isInitialized = true;
    }


}