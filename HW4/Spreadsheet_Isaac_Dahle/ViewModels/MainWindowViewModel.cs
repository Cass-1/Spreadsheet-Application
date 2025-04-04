﻿// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using ReactiveUI;
using SpreadsheetEngine;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace HW4.ViewModels;

/// <summary>
///     The MainWindowViewModel for avalonia.
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
#pragma warning restore CA1822 // Mark members as static
#pragma warning disable CA1822 // Mark members as static

    /// <summary>
    ///     A manager for all commands.
    /// </summary>
    internal readonly CommandManager CommandManager = new();

    private readonly List<CellViewModel> selectedCells = new();
    private bool isInitialized;
    private int rowCount;
    private int columnCount;
    private Spreadsheet spreadsheet;

    private IImmutableSolidColorBrush elementBrush = new ImmutableSolidColorBrush(Colors.Aqua);

    // public Interaction<MainWindowViewModel, ColorPickerWindow?> ShowDialog { get; }

    // ReSharper disable once CollectionNeverQueried.Local
    private List<List<Cell>>? spreadsheetData;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
    ///     Constructor.
    /// </summary>
    public MainWindowViewModel()
    {
        this.ElementBrush = Brushes.Aqua;
        this.RedoCommand = ReactiveCommand.Create(this.Redo);
        this.UndoCommand = ReactiveCommand.Create(this.Undo);

        // Create an interaction between the view model and the view for the file to be loaded:
        this.AskForFileToLoad = new Interaction<Unit, string?>();

        // Similarly to load, there is a need to create an interaction for saving into a file:
        this.AskForFileToSave = new Interaction<Unit, string?>();

        // initalize the spreadsheet
        this.InitializeSpreadsheet();

        // for row for col loop over
        for (var row = 0; row < this.rowCount; row++)
        {
            var listOfCellModels = new List<CellViewModel>();
            for (var col = 0; col < this.columnCount; col++)
            {
                listOfCellModels.Add(
                    new CellViewModel(this.spreadsheet?.GetCell(row, col) ?? throw new InvalidOperationException()));
            }

            var rowViewModel = new RowViewModel(listOfCellModels);
            this.Rows.Add(rowViewModel);
        }
    }

    /// <summary>
    /// Gets a reactive command for redo.
    /// </summary>
    public ReactiveCommand<Unit, Unit> RedoCommand { get; }

    /// <summary>
    /// Gets a reactive command for undo.
    /// </summary>
    public ReactiveCommand<Unit, Unit> UndoCommand { get; }

    /// <summary>
    /// Gets a reactive command for asking for a file to load.
    /// </summary>
    // interactions
    public Interaction<Unit, string?> AskForFileToLoad { get; }

    /// <summary>
    /// Gets a reactive command for asking for a file to save to.
    /// </summary>
    public Interaction<Unit, string?> AskForFileToSave { get; }

    /// <summary>
    ///     Gets a 2D array of Cells that is populated with the cells from the Spreadsheet.
    /// </summary>
    public ObservableCollection<RowViewModel> Rows { get; } = new();

    /// <summary>
    ///     Gets or sets the brush for a cell or cells.
    /// </summary>
    public IImmutableSolidColorBrush ElementBrush
    {
        get => this.elementBrush;
        set
        {
            this.RaiseAndSetIfChanged(ref this.elementBrush, value);
            foreach (var cell in this.selectedCells)
            {
                this.CommandManager.ExecuteCommand(new SetCellColorCommand(cell.Cell, value.Color.ToUInt32()));
            }
        }
    }

    /// <summary>
    ///     This method will be executed when the user wants to load content from a file.
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
        this.spreadsheet.Load(textReader);
        textReader.Close();
        this.CommandManager.ClearUndoStack();
    }

    /// <summary>
    ///     This method will be executed when a user wants to save content to a file.
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
        var streamWriter = new StreamWriter(filePath);
        this.spreadsheet.Save(streamWriter);
        streamWriter.Close();
    }

    /// <summary>
    ///     Initalizes the datagrid.
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
                        (_, _) =>
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
                        (_, _) =>
                            new TextBox()),
            };
            dataGrid.Columns.Add(columnTemplate);
        }

        dataGrid.ItemsSource = this.Rows;
        dataGrid.LoadingRow += (_, args) => { args.Row.Header = (args.Row.GetIndex() + 1).ToString(); };

        // add events to update cell value
        dataGrid.PreparingCellForEdit += (_, args) =>
        {
            if (args.EditingElement is not TextBox textInput)
            {
                return;
            }

            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;
            var o = this.spreadsheet;
            if (o != null)
            {
                textInput.Text = o.GetCell(rowIndex, columnIndex).Text;
            }
            else
            {
                throw new NullReferenceException();
            }
        };
        dataGrid.CellEditEnding += (_, args) =>
        {
            var rowIndex = args.Row.GetIndex();
            var colIndex = args.Column.DisplayIndex;
            var cell = this.spreadsheet.GetCell(rowIndex, colIndex);
            if (args.EditingElement is TextBox textBox)
            {
                if (textBox.Text != null)
                {
                    var result = textBox.Text.Trim();
                    if (result.Length == 0)
                    {
                        this.CommandManager.ExecuteCommand(new SetCellTextCommand(cell, null!));
                    }

                    this.CommandManager.ExecuteCommand(new SetCellTextCommand(cell, result));
                }
            }
        };

        dataGrid.BeginningEdit += (_, args) =>
        {
            // get the pressed cell
            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;
            var cell = this.GetCell(rowIndex, columnIndex);
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
        dataGrid.CellPointerPressed += (_, args) =>
        {
            // get the pressed cell
            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;

            // are we selected multiple cells
            var multipleSelection = args.PointerPressedEventArgs.KeyModifiers != KeyModifiers.None;
            if (multipleSelection == false)
            {
                this.SelectCell(rowIndex, columnIndex);
            }
            else
            {
                this.ToggleCellSelection(rowIndex, columnIndex);
            }
        };

        this.isInitialized = true;
    }

    /// <summary>
    ///     Selects a cell.
    /// </summary>
    /// <param name="rowIndex">The row index of a cell.</param>
    /// <param name="columnIndex">The column index of a cell.</param>
    public void SelectCell(int rowIndex, int columnIndex)
    {
        var clickedCell = this.GetCell(rowIndex, columnIndex);
        var shouldEditCell = clickedCell.IsSelected;
        this.ResetSelection();

        // add the pressed cell back to the list
        this.selectedCells.Add(clickedCell);
        clickedCell.IsSelected = true;
        if (shouldEditCell)
        {
            clickedCell.CanEdit = true;
        }
    }

    /// <summary>
    ///     Toggles the selection of a cell.
    /// </summary>
    /// <param name="rowIndex">The index of a cell.</param>
    /// <param name="columnIndex">A column of a cell.</param>
    public void ToggleCellSelection(int rowIndex, int columnIndex)
    {
        var clickedCell = this.GetCell(rowIndex, columnIndex);
        if (clickedCell.IsSelected == false)
        {
            this.selectedCells.Add(clickedCell);
            clickedCell.IsSelected = true;
        }
        else
        {
            this.selectedCells.Remove(clickedCell);
            clickedCell.IsSelected = false;
        }
    }

    /// <summary>
    ///     Resets the selection.
    /// </summary>
    public void ResetSelection()
    {
// clear current selection
        foreach (var cell in this.selectedCells)
        {
            cell.IsSelected = false;
            cell.CanEdit = false;
        }

        this.selectedCells.Clear();
    }

    /// <summary>
    /// Calls redo.
    /// </summary>
    private void Redo()
    {
        this.CommandManager.Redo();
    }

    /// <summary>
    /// Calls undo.
    /// </summary>
    private void Undo()
    {
        this.CommandManager.Undo();
    }

    /// <summary>
    ///     Initializes the spreadsheet.
    /// </summary>
    /// <param name="row">The number of rows the spreadsheet should have.</param>
    /// <param name="column">The number of columns the spreadsheet should have.</param>
    private void InitializeSpreadsheet(int row = 50, int column = 'Z' - 'A' + 1)
    {
        this.rowCount = row;
        this.columnCount = column;
        this.spreadsheet = new Spreadsheet(this.rowCount, this.columnCount);
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

    private CellViewModel GetCell(int rowIndex, int columnIndex)
    {
        return this.Rows[rowIndex][columnIndex];

        // [(rowIndex - 1) * 10 + columnIndex];
    }
}
