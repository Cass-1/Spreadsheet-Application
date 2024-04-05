// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Input;
using AvaloniaColorPicker;
using HW4.Views;
using ReactiveUI;
using Spreadsheet_GettingStarted.ViewModels;

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

    private bool isInitialized;
    private int rowCount;
    private int columnCount;
    private Spreadsheet? spreadsheet;
    private List<RowViewModel> _spreadsheetData = null;
    private readonly List<CellViewModel> _selectedCells = new();



    // public Interaction<MainWindowViewModel, ColorPickerWindow?> ShowDialog { get; }

    // ReSharper disable once CollectionNeverQueried.Local
    private List<List<Cell>>? spreadsheetData;
    private Color _selectedColor;

    // public int Number { get; set; }
    // public IBrush mycolor { get; set; } = Brushes.Aqua;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// Constructor.
    /// </summary>
    public MainWindowViewModel()
    {
        this.ElementBrush = Brushes.Aqua;
        ShowDialog = new Interaction<MainWindowViewModel, ColorPickerWindow>();

        // initalize the spreadsheet
        this.InitializeSpreadsheet();

        // for row for col loop over
        for (int row = 0; row < this.rowCount; row++)
        {
            List<CellViewModel> listOfCellModels = new List<CellViewModel>();
            for (int col = 0; col < this.columnCount; col++)
            {
                listOfCellModels.Add(new CellViewModel(this.spreadsheet.GetCell(row,col)));
            }

            RowViewModel rowViewModel = new RowViewModel(listOfCellModels);
            this.Rows.Add(rowViewModel);
        }

    }


    public Interaction<MainWindowViewModel, ColorPickerWindow> ShowDialog { get; }

    private IBrush elementBrush;
    public IBrush ElementBrush
    {
        get => this.elementBrush;
        set => this.RaiseAndSetIfChanged(ref this.elementBrush, value);
    }

    /// <summary>
    /// Gets a 2D array of Cells that is populated with the cells from the Spreadsheet.
    /// </summary>
    public ObservableCollection<RowViewModel> Rows { get; } = new ObservableCollection<RowViewModel>();

    /// <summary>
    /// Initalizes the datagrid.
    /// </summary>
    /// <param name="dataGrid">The datagrid to inialize.</param>
    public void InitializeDataGrid(DataGrid dataGrid)
    {
        if (this.isInitialized)
        {
            return;
        }

        // initialize A to Z columns headers since these are indexed this is not a behavior supported by default
        // var columnCount = 'Z' - 'A' + 1;
        foreach (var columnIndex in Enumerable.Range(0, this.columnCount))
        {
            // for each column we will define the header text and
            // the binding to use
            var columnHeader = (char)('A' + columnIndex);
            var columnTemplate = new DataGridTemplateColumn
            {
                Header = columnHeader,
                CellStyleClasses = { "SpreadsheetCellClass" },
                CellTemplate = new
                    FuncDataTemplate<RowViewModel>(
                        (value, namescope) =>
                            new TextBlock
                            {
                                [!TextBlock.TextProperty] =
                                    new
                                        Binding($"[{columnIndex}].Value"),
                                TextAlignment = TextAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Center,
                                Padding = Thickness.Parse("5,0,5,0"),
                            }),
                CellEditingTemplate = new
                    FuncDataTemplate<RowViewModel>(
                        (value, namescope) =>
                            new TextBox()),
            };
            dataGrid.Columns.Add(columnTemplate);
        }

        dataGrid.ItemsSource = this.Rows;
            dataGrid.LoadingRow += (_, args) => { args.Row.Header = (args.Row.GetIndex() + 1).ToString(); };

            // add events to update cell value
            dataGrid.PreparingCellForEdit += (sender, args) =>
            {
                if (args.EditingElement is not TextBox textInput) return;
                var rowIndex = args.Row.GetIndex();
                var columnIndex = args.Column.DisplayIndex;
                textInput.Text = this.spreadsheet.GetCell(rowIndex, columnIndex).Text;
            };
            dataGrid.CellEditEnding += (sender, args) =>
            {
                var rowIndex = args.Row.GetIndex();
                var colIndex = args.Column.DisplayIndex;
                var cell = this.spreadsheet.GetCell(rowIndex, colIndex);
                if (args.EditingElement is TextBox textBox)
                {
                    var result = textBox.Text.Trim();
                    if (result.Length == 0)
                    {
                        cell.Text = null;
                    }

                    cell.Text = result;
                }
            };

            dataGrid.BeginningEdit += (sender, args) =>
            {
                // get the pressed cell
                var rowIndex = args.Row.GetIndex();
                var columnIndex = args.Column.DisplayIndex;
                var cell = GetCell(rowIndex, columnIndex, dataGrid);
                if (cell.CanEdit == false)
                {
                    args.Cancel = true;
                }
                else
                {
                    this.ResetSelection();
                }
            };

            // we use the following event to write our own selection logic
            dataGrid.CellPointerPressed += (sender, args) =>
            {
                // get the pressed cell
                var rowIndex = args.Row.GetIndex();
                var columnIndex = args.Column.DisplayIndex;
                // are we selected multiple cells
                var multipleSelection = args.PointerPressedEventArgs.KeyModifiers != KeyModifiers.None;
                if (multipleSelection == false)
                {
                    this.SelectCell(rowIndex, columnIndex, dataGrid);
                }
                else
                {
                    this.ToggleCellSelection(rowIndex, columnIndex, dataGrid);
                }
            };

            this.isInitialized = true;

    }

    /// <summary>
    /// Initializes the spreadsheet.
    /// </summary>
    /// <param name="row">The number of rows the spreadsheet should have.</param>
    /// <param name="column">The number of columns the spreadsheet should have.</param>
    private void InitializeSpreadsheet(int row = 50, int column = 'Z' - 'A' + 1)
    {
        this.rowCount = row;
        this.columnCount = column;
        this.spreadsheet = new Spreadsheet(rowCount: this.rowCount, columnCount: this.columnCount);
        this.spreadsheetData = new List<List<Cell>>(this.rowCount);
        foreach (var rowIndex in Enumerable.Range(0, this.rowCount))
        {
            var columns = new List<Cell>(this.columnCount);
            foreach (var columnIndex in Enumerable.Range(0, this.columnCount))
            {
                columns.Add(this.spreadsheet.GetCell(rowIndex, columnIndex));
            }

            this.spreadsheetData.Add(columns);
        }
    }

    public void SelectCell(int rowIndex, int columnIndex, DataGrid dataGrid)
    {
        var clickedCell = GetCell(rowIndex, columnIndex, dataGrid);
        var shouldEditCell = clickedCell.IsSelected;
        ResetSelection();
// add the pressed cell back to the list
        _selectedCells.Add(clickedCell);
        clickedCell.IsSelected = true;
        if (shouldEditCell)
            clickedCell.CanEdit = true;
    }

    private CellViewModel GetCell(int rowIndex, int columnIndex, DataGrid dataGrid)
    {
        return this.Rows[rowIndex][columnIndex];

        //[(rowIndex - 1) * 10 + columnIndex];
    }

    public void ToggleCellSelection(int rowIndex, int columnIndex, DataGrid dataGrid)
    {
        var clickedCell = GetCell(rowIndex, columnIndex, dataGrid);
        if (clickedCell.IsSelected == false)
        {
            _selectedCells.Add(clickedCell);
            clickedCell.IsSelected = true;
        }
        else
        {
            this._selectedCells.Remove(clickedCell);
            clickedCell.IsSelected = false;
        }
    }

    public void ResetSelection()
    {
// clear current selection
        foreach (var cell in _selectedCells)
        {
            cell.IsSelected = false;
            cell.CanEdit = false;
        }
        _selectedCells.Clear();
    }
}
