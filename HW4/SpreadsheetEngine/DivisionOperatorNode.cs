// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// A node that represents the division operator.
/// </summary>
public class DivisionOperatorNode : OperatorNode
{
    
    public static char Character = '/';
    
    public static int Precedence = 2;

    public static string Assosiativity = "Left";
    
     /// <summary>
    /// The left child node.
    /// </summary>
    private Node leftChild;

    /// <summary>
    /// The right child node.
    /// </summary>
    private Node rightChild;

    /// <summary>
    /// Evaluates the quotient of the two child nodes.
    /// </summary>
    /// <returns>The quotient of the two child nodes.</returns>
    public override double Evaluate()
    {
        return this.leftChild.Evaluate() / this.rightChild.Evaluate();
    }
}
