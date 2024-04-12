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
    /// A command manager.
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
    ///     Returns the cell at a specified location, using the spreadsheet naming convention.
    /// </summary>
    /// <param name="col">A character that represents the column in the spreadsheet UI.</param>
    /// <param name="row">A number that represents the row in the spreadsheet UI.</param>
    /// <returns>A cell in the spreadsheet.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The argument(s) given are outside the bounds of
    ///     the spreadsheet.
    /// </exception>
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

    public void Load(StreamReader stream)
    {
        this.Clear();

            // -create XmlReader
            //
            // -create XDocument(reader)
            //
            // -create enumerable usring document.elements
            //
            // -for element (load cell)

        XmlReaderSettings settings = new XmlReaderSettings();

        XmlReader reader = XmlReader.Create(stream, settings);

        XDocument xdoc = XDocument.Load(reader);

        // XElement spread

        IEnumerable<XElement> elements = xdoc.Root.Elements("Cell");

        foreach (XElement xElement in elements)
        {
            string cellName = xElement.Element("Name").Value;

            // Get the cell
            int rowIndex = int.Parse(cellName.Substring(1)) - 1;
            int colIndex = cellName[0] - 'A';
            SpreadsheetCell cell = this.cellGrid[rowIndex, colIndex];
            cell.ReadXml(xElement);
            cell.EvaluateExpression();

        }//TODO: change ReadXml to take xElement





    }

    public void Clear()
    {
        for (int row = 0; row < this.RowCount; row++)
        {
            for (int col = 0; col < this.ColumnCount; col++)
            {
                var cell = GetCell(row, col);
                if (cell.ChangedFromDefaults())
                {
                    cell.Clear();
                }
            }
        }
    }

    public void Save(StreamWriter stream)
    {
        // Create XmlWriterSettings
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;

        // Create an XmlWriter
        using (XmlWriter writer = XmlWriter.Create(stream, settings))
        {
            // Start the document
            writer.WriteStartDocument();

            // Start root element
            writer.WriteStartElement("Spreadsheet");

            for (int row = 0; row < this.RowCount; row++)
            {
                for (int col = 0; col < this.ColumnCount; col++)
                {
                    // Get the cell
                    Cell cell = this.GetCell(row, col);

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
            // The location of the cell to get the value from.
            var expression = cell.Text.TrimStart('=');
            if (expression == cell.Expression)
            {
                cell.EvaluateExpression();
                return;
            }

            cell.SetExpression(expression);

            var variables = cell.GetReferencedCellNames();

            foreach (var variable in variables)
            {
                // this try catch block prevents errors from happening in the spreadsheet when we are still typing
                // and before we have finished entering a value.
                var rowCharacter = variable.Substring(1, variable.Length - 1);
                var columnCharacter = variable[0];
                if (rowCharacter == columnCharacter.ToString())
                {
                    return;
                }

                try
                {
                    // get the reference cell
                    var referencedCell = this.GetCell(int.Parse(rowCharacter) - 1, columnCharacter - 'A');

                    cell.AddReferenceCell(referencedCell);
                }
                catch (Exception)
                {
                    return;
                }
            }

            cell.EvaluateExpression();
        }

        // if the cell's text is not an expression
        else
        {
            cell.Value = cell.Text;
            cell.Expression = string.Empty;
        }

        // invoke the property changed event to update the UI
        // this.CellPropertyChangedEvent.Invoke(sender, e);
    }

    private class SpreadsheetCell : Cell
    {
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
            this.EvaluateExpression();
        }

        /// <summary>
        ///     Gets the names of all the cells this cell's expression references.
        /// </summary>
        /// <returns>A list of all the cells that this cell's expression references.</returns>
        public List<string> GetReferencedCellNames()
        {
            return this.expressionTree.GetVariableNames();
        }

        /// <summary>
        ///     Adds a reference to a cell that this cell's expression references.
        /// </summary>
        /// <param name="cell">The new cell to reference.</param>
        public void AddReferenceCell(Cell cell)
        {
            this.referencedCells.Add(cell);
            cell.PropertyChanged += this.OnReferenceChanged;
        }



        /// <summary>
        ///     Called when a reference cell changes.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnReferenceChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.EvaluateExpression();
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
