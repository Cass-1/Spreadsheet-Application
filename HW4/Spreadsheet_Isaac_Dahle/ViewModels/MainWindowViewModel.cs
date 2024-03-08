// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace HW4.ViewModels;

using System;
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

    // ReSharper disable once CollectionNeverQueried.Local
    private List<List<Cell>>? spreadsheetData;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// Constructor.
    /// </summary>
    public MainWindowViewModel()
    {
        // initalize the spreadsheet
        this.InitializeSpreadsheet();

        // initalize the rows
        this.Rows = Enumerable.Range(0, this.rowCount)
            .Select(
                row => Enumerable.Range(0, this.columnCount)
                .Select(column => this.spreadsheet?.GetCell(row, column)).ToArray())
            .ToArray();
    }

    /// <summary>
    /// Gets a 2D array of Cells that is populated with the cells from the Spreadsheet.
    /// </summary>
    public Cell?[][] Rows { get; }

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
                CellTemplate = new FuncDataTemplate<IEnumerable<Cell>>(
                    (_, _) =>
                    new TextBlock
                    {
                        [!TextBlock.TextProperty] = new Binding($"[{columnIndex}].Value"),
                        TextAlignment = TextAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Padding = Thickness.Parse("5,0,5,0"),
                    }),
                CellEditingTemplate = new FuncDataTemplate<IEnumerable<Cell>>(
                    (_, _) =>
                    new TextBox
                    {
                        [!TextBox.TextProperty] = new Binding($"[{columnIndex}].Text"),
                    }),
            };
            dataGrid.Columns.Add(columnTemplate);
        }

        dataGrid.ItemsSource = this.Rows;
        dataGrid.LoadingRow += (_, args) => { args.Row.Header = (args.Row.GetIndex() + 1).ToString(); };

        this.isInitialized = true;
    }

    /// <summary>
    /// Runs the demo for presenting the spreadsheets to TA's.
    /// </summary>
    public void Demo()
    {
        int rowIndex;

        // add text to 50 random cells
        for (int i = 0; i < 50; i++)
        {
            rowIndex = Random.Shared.Next(0, 50);
            var columnIndex = Random.Shared.Next(0, 26);
            var o = this.spreadsheet;
            if (o != null)
            {
                o.GetCell(rowIndex, columnIndex).Text = "Hell0";
            }
        }

        for (int i = 0; i < 50; i++)
        {
            var strA = "=B" + (i + 1);
            var strB = "This is cell B" + (i + 1);
            var o = this.spreadsheet;
            if (o != null)
            {
                o.GetCell(i, 1).Text = strB;
                o.GetCell(i, 0).Text = strA;
            }
        }
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
}
