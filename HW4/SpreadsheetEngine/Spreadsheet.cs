// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace SpreadsheetEngine;

/// <summary>
///     A class that represents a spreadsheet.
/// </summary>
public class Spreadsheet
{
    /// <summary>
    ///     A command manager.
    /// </summary>
    public CommandManager SpreadsheetCommandManager = new();

    /// <summary>
    ///     The 2D grid of cells in the spreadsheet.
    /// </summary>
    private readonly SpreadsheetCell[,] cellGrid;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Spreadsheet" /> class.
    /// </summary>
    /// <param name="rowCount">The number of rows to have.</param>
    /// <param name="columnCount">the number of columns to have.</param>
    public Spreadsheet(int rowCount, int columnCount)
    {
        // check if arguments are out of range
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rowCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(columnCount);

        // initalize row and column count
        this.RowCount = rowCount;
        this.ColumnCount = columnCount;

        // initalize all cells as null
        this.cellGrid = new SpreadsheetCell[this.RowCount, this.ColumnCount];

        // populate cell grid
        foreach (var rowIndex in Enumerable.Range(0, this.RowCount))
        {
            foreach (var columnIndex in Enumerable.Range(0, this.ColumnCount))
            {
                // set the cell to an actual instance of a cell
                this.cellGrid[rowIndex, columnIndex] = new SpreadsheetCell(rowIndex, columnIndex);

                // subscribe to each cell
                this.cellGrid[rowIndex, columnIndex].PropertyChanged += this.CellPropertyChanged;
            }
        }
    }

    /// <summary>
    ///     Gets the number of rows.
    /// </summary>
    public int RowCount { get; }

    /// <summary>
    ///     Gets the number of comumns.
    /// </summary>
    public int ColumnCount { get; }

    /// <summary>
    ///     Returns the cell at a given row and column index.
    /// </summary>
    /// <param name="rowIndex">the row index of the cell.</param>
    /// <param name="colIndex">the column index of the cell.</param>
    /// <returns>A cell.</returns>
    public Cell GetCell(int rowIndex, int colIndex)
    {
        if (rowIndex >= this.RowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex) + " is out of bounds");
        }

        if (colIndex >= this.ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(colIndex) + @" is out of bounds");
        }

        return this.cellGrid[rowIndex, colIndex];
    }

    /// <summary>
    ///   Returns the cell at a specified location, using the spreadsheet naming convention.
    /// </summary>
    /// <param name="col">The column character.</param>
    /// <param name="row">The row number.</param>
    /// <returns>The cell at the specified location.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Trown when the row or col are out of bounds.</exception>
    public Cell GetCell(char col, int row)
    {
        var rowIndex = row - 1;
        var colIndex = col - 'A';
        if (rowIndex >= this.RowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex) + " is out of bounds");
        }

        if (colIndex >= this.ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(colIndex) + " is out of bounds");
        }

        return this.cellGrid[rowIndex, colIndex];
    }

    /// <summary>
    ///     Loads the spreadsheet from a stream.
    /// </summary>
    /// <param name="stream">The stream to load informatino from.</param>
    public void Load(StreamReader stream)
    {
        // Clear the spreadsheet
        this.Clear();

        // Create XmlReaderSettings and
        var settings = new XmlReaderSettings();

        // Create an XmlReader and xdoc
        var reader = XmlReader.Create(stream, settings);
        var xdoc = XDocument.Load(reader);

        // Get the elements
        if (xdoc.Root != null)
        {
            var elements = xdoc.Root.Elements("Cell");

            // Read the elements
            foreach (var xElement in elements)
            {
                // Get the cell name
                var cellName = xElement.Element("Name")?.Value;

                // Get the cell
                if (cellName != null)
                {
                    var rowIndex = int.Parse(cellName.Substring(1)) - 1;
                    var colIndex = cellName[0] - 'A';
                    var cell = this.cellGrid[rowIndex, colIndex];

                    // Read the cell in from the xml
                    cell.ReadXml(xElement);

                    // If the cell has an expression, evaluate it
                    cell.EvaluateExpression();
                }
            }
        }
    }

    /// <summary>
    ///     Clears the spreadsheet.
    /// </summary>
    public void Clear()
    {
        for (var row = 0; row < this.RowCount; row++)
        {
            for (var col = 0; col < this.ColumnCount; col++)
            {
                var cell = this.GetCell(row, col);
                if (cell.ChangedFromDefaults())
                {
                    cell.Clear();
                }
            }
        }
    }

    /// <summary>
    ///     Saves the spreadsheet to a stream.
    /// </summary>
    /// <param name="stream">The stream to save the spreadsheet to.</param>
    public void Save(StreamWriter stream)
    {
        // Create XmlWriterSettings
        var settings = new XmlWriterSettings();
        settings.Indent = true;

        // Create an XmlWriter
        using (var writer = XmlWriter.Create(stream, settings))
        {
            // Start the document
            writer.WriteStartDocument();

            // Start root element
            writer.WriteStartElement("Spreadsheet");

            for (var row = 0; row < this.RowCount; row++)
            {
                for (var col = 0; col < this.ColumnCount; col++)
                {
                    // Get the cell
                    var cell = this.GetCell(row, col);

                    // only write cell if it has been changed
                    if (cell.ChangedFromDefaults())
                    {
                        cell.WriteXml(writer);
                    }
                }
            }

            // End the root element
            writer.WriteEndElement();

            // End the document
            writer.WriteEndDocument();

            // Flush the writer to ensure all data is written to the MemoryStream
            writer.Flush();
        }

// Reset the position of the MemoryStream to the beginning
        // stream.Position = 0;
    }

    /// <summary>
    ///     Returns the cell at a specified location, using the spreadsheet naming convention.
    ///     This is needed as I need to get cells by their name in the method CellHasCircularReferences.
    /// </summary>
    /// <param name="name">The name of the cell.</param>
    /// <returns>A cell in the spreadsheet.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The argument(s) given are outside the bounds of
    ///     the spreadsheet.
    /// </exception>
    private SpreadsheetCell GetCell(string name)
    {
        var rowIndex = int.Parse(name.Substring(1)) - 1;
        var colIndex = name[0] - 'A';
        if (rowIndex >= this.RowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex) + " is out of bounds");
        }

        if (colIndex >= this.ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(colIndex) + " is out of bounds");
        }

        return this.cellGrid[rowIndex, colIndex];
    }

    /// <summary>
    ///     The function that is called whenever a cell has been changed.
    /// </summary>
    /// <param name="sender">The object that send the property changed event.</param>
    /// <param name="e">The event arguments.</param>
    private void CellPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // get the spreadsheet cell that sent the event
        var cell = (SpreadsheetCell)sender!;
        if (cell.Text == null)
        {
            return;
        }

        // if the cell's text is an expression
        if (cell.Text.StartsWith('='))
        {
            // get the expression
            cell.SetExpression(cell.Text.TrimStart('='));

            // get the referenced cells string names
            var variables = cell.GetReferencedCellNames();

            // check for a self reference
            foreach (var cellName in variables)
            {
                if (cell.Name == cellName)
                {
                    // self reference
                    cell.Text = "Self Reference";
                    cell.Value = cell.Text;
                    cell.SetExpression(string.Empty);
                    cell.ClearReferences();
                    return;
                }
            }

            // assignes the reference cells to the cell
            foreach (var variable in variables)
            {
                try
                {
                    var referencedCell = this.GetCell(variable);
                    cell.AddReferenceCell(referencedCell);
                }
                catch (ArgumentOutOfRangeException)
                {
                    cell.Text = "Invalid Reference";
                    return;
                }
                catch (ArgumentException)
                {
                    cell.Text = "Invalid Reference";
                    return;
                }
                catch (Exception)
                {
                    cell.Text = "Invalid Reference";
                    return;
                }
            }

            try
            {
                cell.EvaluateExpression();
            }
            catch (ArgumentException)
            {
                cell.ClearReferences();
                cell.Text = "Circular Reference";
                cell.Value = cell.Text;
                cell.SetExpression(string.Empty);
            }
        }

        // if the cell's text is not an expression
        else
        {
            cell.SetExpression(string.Empty);
            cell.Value = cell.Text;
        }

        // invoke the property changed event to update the UI
        // this.CellPropertyChangedEvent.Invoke(sender, e);
    }

    private class SpreadsheetCell : Cell
    {
        private new List<SpreadsheetCell> referencedCells = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpreadsheetCell" /> class.
        /// </summary>
        /// <param name="rowIndex">The cell's row position in the spreadsheet.</param>
        /// <param name="columnIndex">The cell's column position in the spreadsheet.</param>
        public SpreadsheetCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
        }

        /// <summary>
        ///     Gets or sets the value of Value.
        /// </summary>
        public override string? Value
        {
            get => this.value;
            protected internal set => this.SetandNotifyIfChanged(ref this.value, value);
        }

        /// <summary>
        ///     Evaluates the cell's expression.
        /// </summary>
        public void EvaluateExpression()
        {
            this.expressionTree = new ExpressionTree(this.Expression);

            if (this.ReferencesSelf())
            {
                throw new ArgumentException("A cell cannot reference itself.");
            }

            var dict = new Dictionary<string, int>();
            if (HasCircularReferences(this, dict))
            {
                throw new ArgumentException("Circular Reference");
            }

            if (this.Text == "Circular Reference" || this.Text == "Self Reference")
            {
                return;
            }

            // set the reference variables
            foreach (var cell in this.referencedCells)
            {
                if (cell.Value != string.Empty)
                {
                    if (cell.Value != null)
                    {
                        this.expressionTree.SetVariable(cell.Name, double.Parse(cell.Value));
                    }
                }
            }

            this.Value = this.expressionTree.Evaluate().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Sets the cell's expression.
        /// </summary>
        /// <param name="expression">The new expression.</param>
        public void SetExpression(string expression)
        {
            this.UnsubscribeFromReferencedCells();
            this.Expression = expression;
            this.expressionTree = null;
        }

        /// <summary>
        ///     Gets the names of all the cells this cell's expression references.
        /// </summary>
        /// <returns>A list of all the cells that this cell's expression references.</returns>
        public List<string> GetReferencedCellNames()
        {
            // this if is here so that my CircularReference method in Spreadsheet can get each cell's references
            if (this.expressionTree == null)
            {
                this.expressionTree = new ExpressionTree(this.Expression);
            }

            return this.expressionTree.GetVariableNames();
        }

        /// <summary>
        ///     Checks if a cell references itself.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool ReferencesSelf()
        {
            foreach (var name in this.GetReferencedCellNames())
            {
                if (this.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Adds a reference to a cell that this cell's expression references.
        /// </summary>
        /// <param name="cell">The new cell to reference.</param>
        public void AddReferenceCell(SpreadsheetCell cell)
        {
            if (this.Name == cell.Name)
            {
                throw new ArgumentException("A cell cannot reference itself.");
            }

            this.referencedCells.Add(cell);
            cell.PropertyChanged += this.OnReferenceChanged;
        }

        /// <summary>
        ///   Clears all references.
        /// </summary>
        public void ClearReferences()
        {
            this.referencedCells = new List<SpreadsheetCell>();
        }

        /// <summary>
        ///   Unsubscribe from all reference cells.
        /// </summary>
        /// <param name="cell">The cell to check for circular references.</param>
        /// <param name="dict">A dictionary to keep track of the cells that have been encountered.</param>
        /// <returns>Whether there is a circular reference or not.</returns>
        private static bool HasCircularReferences(SpreadsheetCell cell, Dictionary<string, int> dict)
        {
            foreach (var currentCell in cell.referencedCells)
            {
                if (dict.ContainsKey(currentCell.Name))
                {
                    return true;
                }

                dict.Add(currentCell.Name, 1);

                return HasCircularReferences(currentCell, dict);
            }

            return false;
        }

        /// <summary>
        ///     Called when a reference cell changes.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnReferenceChanged(object? sender, PropertyChangedEventArgs e)
        {
            // don't reevaluate if the cell has a circular reference
            var cell = (SpreadsheetCell)sender!;

            if (cell.Text != "Circular Reference" && cell.Text != "Self Reference" && cell.Text != "Invalid Reference")
            {
                this.EvaluateExpression();
            }
        }

        /// <summary>
        ///     Unsubscribe from all reference cells.
        /// </summary>
        private void UnsubscribeFromReferencedCells()
        {
            if (this.referencedCells.Count > 0)
            {
                foreach (var cell in this.referencedCells)
                {
                    cell.PropertyChanged -= this.OnReferenceChanged;
                }

                this.referencedCells.Clear();
            }
        }
    }
}
