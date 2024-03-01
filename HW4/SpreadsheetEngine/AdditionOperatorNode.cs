// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// A class to represent the addtition operator.
/// </summary>
public class AdditionOperatorNode : OperatorNode
{
     /// <summary>
    /// The left child node.
    /// </summary>
    private Node leftChild;

    /// <summary>
    /// The right child node.
    /// </summary>
    private Node rightChild;

    /// <summary>
    /// Evaluates the sum of the two child nodes.
    /// </summary>
    /// <returns>The sum of the two child nodes.</returns>
    public override double Evaluate()
    {
        return this.leftChild.Evaluate() + this.rightChild.Evaluate();
    }
}
