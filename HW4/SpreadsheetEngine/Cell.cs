// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SpreadsheetEngine;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

/// <summary>
/// Represents a cell in a spreadsheet.
/// </summary>
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<both the value and text property need to be protected and not private to comply with assignment requirements.>")]
[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "<both the value and text property need to be protected and not private to comply with assignment requirements.>")]
public abstract class Cell : INotifyPropertyChanged, IXmlSerializable
{
    /// <summary>
    /// The cell's name, eg "C10".
    /// </summary>
    public string Name;

    /// <summary>
    /// The mathematical expression in a cell.
    /// </summary>
    public string Expression = null!;

    /// <summary>
    /// The cell's expression tree.
    /// </summary>
    protected ExpressionTree expressionTree = null!;

    /// <summary>
    /// A list of the referenced cells in the expressionTree.
    /// </summary>
    protected List<Cell> referencedCells = new List<Cell>();

    /// <summary>
    /// Represents the actual text put into the cell.
    /// </summary>
    protected string? text;

    /// <summary>
    /// Represents the evaluated result of a cell.
    /// </summary>
    protected string? value;

    /// <summary>
    /// The color of the cell.
    /// </summary>
    private uint backgroundColor;

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="rowIndex">The cell's row index in a spreadsheet.</param>
    /// <param name="columnIndex">The cell's column index in a spreadsheet.</param>
    public Cell(int rowIndex, int columnIndex)
    {
        this.RowIndex = rowIndex;
        this.ColumnIndex = columnIndex;
        this.text = string.Empty;
        this.value = string.Empty;
        // set the default color to white
        this.backgroundColor = uint.MaxValue;
        this.Expression = string.Empty;

        string str = string.Empty;
        char col = (char)this.ColumnIndex;
        col += 'A';
        str += col;
        str += (this.RowIndex + 1).ToString();
        this.Name = str;

    }

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets the cell's row index.
    /// </summary>
    public int RowIndex { get; }

    /// <summary>
    /// Gets the cell's column index.
    /// </summary>
    public int ColumnIndex { get; }

    /// <summary>
    /// Gets or sets text.
    /// </summary>
    public string? Text
    {
        get => this.text;
        set
        {
            this.SetandNotifyIfChanged(ref this.text, value);
        }
    }

    /// <summary>
    /// Gets or sets the background color of a cell.
    /// </summary>
    public uint BackgroundColor
    {
        get => this.backgroundColor;
        set
        {
            this.SetandNotifyIfChanged(ref this.backgroundColor, value);
        }
    }

    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public virtual string? Value
    {
        get => this.value;
        protected internal set => this.value = value;
    }

    /// <summary>
    /// Calls the PropertyChangedEvent when a property is changed.
    /// </summary>
    /// <param name="propertyName">The name of the property that was changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Sets a value and then calls OnPropertyChanged.
    /// </summary>
    /// <param name="field">A reference to the field that will be updated.</param>
    /// <param name="newValue">The value to update the field to.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <typeparam name="T">The type of the value to update the field to.</typeparam>
    /// <returns>If the setting of the field was successful.</returns>
    protected bool SetandNotifyIfChanged<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue))
        {
            return false;
        }

        field = newValue;
        this.OnPropertyChanged(propertyName);
        return true;
    }

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Cell")
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.NodeType != XmlNodeType.Whitespace)
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            this.Name = reader.ReadElementContentAsString();
                            break;
                        case "Text":
                            this.Text = reader.ReadElementContentAsString();
                            break;
                        case "Expression":
                            this.Expression = reader.ReadElementContentAsString();
                            break;
                        case "Value":
                            this.Value = reader.ReadElementContentAsString();
                            break;
                        case "BackgroundColor":
                            if (reader.NodeType == XmlNodeType.Whitespace || reader.ReadElementContentAsString() == "")
                            {
                                this.BackgroundColor = uint.MaxValue;
                                break;
                            }

                            try
                            {
                                this.BackgroundColor = uint.Parse(reader.ReadElementContentAsString());
                            }
                            catch (System.InvalidOperationException e)
                            {
                                this.BackgroundColor = uint.MaxValue;
                            }
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Cell")
                {
                    return;
                }
            }
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("Cell");

        writer.WriteElementString("Name", this.Name);
        writer.WriteElementString("Text", this.Text);
        writer.WriteElementString("Expression", this.Expression);
        writer.WriteElementString("Value", this.Value);
        writer.WriteElementString("BackgroundColor", this.BackgroundColor.ToString());

        writer.WriteEndElement();
    }

    /// <summary>
    /// Detects if the cell has been changed from its default values.
    /// Used when determining if a cell needs to be written to Xml.
    /// </summary>
    /// <returns>If the cell has been modified from defaults.</returns>
    public bool ChangedFromDefaults()
    {
        if (this.text != string.Empty || this.value != string.Empty || this.Expression != string.Empty ||
            this.BackgroundColor != uint.MaxValue)
        {
            return true;
        }

        return false;
    }

}
