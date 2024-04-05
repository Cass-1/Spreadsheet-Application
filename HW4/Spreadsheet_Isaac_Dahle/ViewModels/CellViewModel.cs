// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using ReactiveUI;
using SpreadsheetEngine;

namespace HW4.ViewModels;

/// <summary>
/// A view model for a cell.
/// </summary>
public class CellViewModel : ViewModelBase
{
    /// <summary>
    /// A cell.
    /// </summary>
    private readonly Cell cell;

    private bool canEdit;

    /// <summary>
    ///     Indicates if a cell is selected.
    /// </summary>
    private bool isSelected;

    /// <summary>
    /// Initializes a new instance of the <see cref="CellViewModel"/> class.
    /// </summary>
    /// <param name="cell">A cell.</param>
    public CellViewModel(Cell cell)
    {
        this.cell = cell;
        this.isSelected = false;
        this.canEdit = false;

        // We forward the notifications from the cell model to the view model
        // note that we forward using args.PropertyName because Cell and CellViewModel properties have the same
        // names to simplify the procedure. If they had different names, we would have a more complex implementation to
        // do the property names matching
        this.cell.PropertyChanged += (_, args) => { this.RaisePropertyChanged(args.PropertyName); };
    }

    /// <summary>
    /// Gets the cell.
    /// </summary>
    public Cell Cell => this.cell;

    /// <summary>
    /// Gets or sets a value indicating whether property that tracks if a cell is selected.
    /// </summary>
    public bool IsSelected
    {
        get => this.isSelected;
        set => this.RaiseAndSetIfChanged(ref this.isSelected, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether property that checks if a cell can be editied.
    /// </summary>
    public bool CanEdit
    {
        get => this.canEdit;
        set => this.RaiseAndSetIfChanged(ref this.canEdit, value);
    }

    /// <summary>
    /// Gets or sets the text of a cell.
    /// </summary>
    public virtual string? Text
    {
        get => this.cell.Text;
        set
        {
            if (value != null)
            {
                this.cell.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets the evaluated value of the text in a cell.
    /// </summary>
    public virtual string? Value => this.cell.Value;

    /// <summary>
    /// Gets or sets the background color of a cell.
    /// </summary>
    public virtual uint BackgroundColor
    {
        get => this.cell.BackgroundColor;
        set => this.cell.BackgroundColor = value;
    }
}
