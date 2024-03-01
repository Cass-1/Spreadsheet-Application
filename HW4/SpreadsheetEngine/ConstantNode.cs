// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// Represents a constant in an experssion.
/// </summary>
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
}
