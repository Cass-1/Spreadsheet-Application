namespace HW4.ViewModels;

using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using SpreadsheetEngine;

/// <summary>
/// The MainWindowViewModel for avalonia.
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
#pragma warning restore CA1822 // Mark members as static
#pragma warning disable CA1822 // Mark members as static

    private bool _isInitialized;
    private int _rowCount;
    private int _columnCount;
    private Spreadsheet _spreadsheet;

    /// <summary>
    /// Gets a 2D array of Cells that is populated with the cells from the Spreadsheet.
    /// </summary>
    public Cell[][] Rows { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// Constructor.
    /// </summary>
    public MainWindowViewModel()
    {
        // initalize row and column count
        this._columnCount = 'Z' - 'A' + 1;
        this._rowCount = 50;

        this._spreadsheet = new Spreadsheet(_rowCount, _columnCount);

        // TODO: fix this
        this.Rows = Enumerable.Range(0, this._rowCount)
            .Select(row => Enumerable.Range(0, this._columnCount)
                .Select(column => _spreadsheet.GetCell(row,column)).ToArray())
            .ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataGrid"></param>
    public void InitializeDataGrid(DataGrid dataGrid)
    {
        if (this._isInitialized)
        {
            return;
        }

        // initialize A to Z columns headers since these are indexed this is not a behavior supported by default
        // var columnCount = 'Z' - 'A' + 1;
        foreach (var columnIndex in Enumerable.Range(0, this._columnCount))
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
                        Padding = Thickness.Parse("5,0,5,0"),
                    }),
                CellEditingTemplate = new FuncDataTemplate<IEnumerable<Cell>>((value, namescope) =>
                    new TextBox
                    {
                        [!TextBox.TextProperty] = new Binding($"[{columnIndex}].Text"),
                    }),
            };
            dataGrid.Columns.Add(columnTemplate);
        }

        dataGrid.ItemsSource = this.Rows;
        dataGrid.LoadingRow += (sender, args) => { args.Row.Header = (args.Row.GetIndex() + 1).ToString(); };

        this._isInitialized = true;
    }
 }