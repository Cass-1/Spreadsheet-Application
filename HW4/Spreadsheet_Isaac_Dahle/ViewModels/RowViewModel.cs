// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel;
using ReactiveUI;

namespace HW4.ViewModels;

/// <summary>
///     A view model for a row.
/// </summary>
public class RowViewModel : ViewModelBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RowViewModel" /> class.
    /// </summary>
    /// <param name="cells">A list of CellViewModesl.</param>
    public RowViewModel(List<CellViewModel> cells)
    {
        this.Cells = cells;
        foreach (var cell in this.Cells)
        {
            cell.PropertyChanged += this.CellOnPropertyChanged;
        }

        this.SelfReference = this;
    }

    /// <summary>
    ///     Gets or sets a list of CellViewModels.
    /// </summary>
    public List<CellViewModel> Cells { get; set; }

    /// <summary>
    ///     Gets this property provides a way to notify the value converter that it needs to update.
    /// </summary>
    public RowViewModel SelfReference { get; private set; }

    /// <summary>
    ///     An indexer for CellViewModel.
    /// </summary>
    /// <param name="index">The index.</param>
    public CellViewModel this[int index] => this.Cells[index];

    /// <summary>
    ///     Event that fires the RaisePropertyChangedEvent.
    /// </summary>
    public void FireChangedEvent()
    {
        this.RaisePropertyChanged(nameof(this.SelfReference));
    }

    private void CellOnPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        var styleImpactingProperties = new List<string>
        {
            nameof(CellViewModel.IsSelected),
            nameof(CellViewModel.CanEdit),
            nameof(CellViewModel.BackgroundColor),
        };
        if (args.PropertyName != null && styleImpactingProperties.Contains(args.PropertyName))
        {
            this.FireChangedEvent();
        }
    }
}
