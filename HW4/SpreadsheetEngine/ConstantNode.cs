// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

// this is because i don't want to overload Object.GetHashCode()
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()

namespace SpreadsheetEngine;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a constant in an experssion.
/// </summary>
[SuppressMessage("ReSharper", "BaseObjectEqualsIsObjectEquals", Justification = "<I need to call base.Equals()>")]
public class ConstantNode : Node
{
    /// <summary>
    /// The value of the node.
    /// </summary>
    private double value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstantNode"/> class.
    /// </summary>
    /// <param name="val">The value of the node.</param>
    public ConstantNode(double val)
    {
        this.value = val;
    }

    /// <summary>
    /// Returns the evaluated value of the node.
    /// </summary>
    /// <returns>The value of the node.</returns>
    public override double Evaluate()
    {
        return this.value;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        var other = obj as ConstantNode;
        if (other == null)
        {
            return false;
        }

        return base.Equals(obj);
    }

    /// <summary>
    /// Compares two ConstantNode's and determines if they are equal.
    /// </summary>
    /// <param name="other">The other object being compated to.</param>
    /// <returns>Whether the two objects are equal.</returns>
    public bool Equals(ConstantNode other)
    {
        return Math.Abs(this.value - other.value) < .005;
    }
}
