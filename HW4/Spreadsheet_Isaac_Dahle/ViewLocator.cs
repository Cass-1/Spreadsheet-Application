// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

// ReSharper disable RedundantNameQualifier
namespace HW4;

using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using HW4.ViewModels;

/// <inheritdoc />
public class ViewLocator : IDataTemplate
{
    /// <inheritdoc/>
    public Control? Build(object? data)
    {
        if (data is null)
        {
            return null;
        }

        var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;
            return control;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    /// <inheritdoc/>
    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
