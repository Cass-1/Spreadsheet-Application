// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the multiplication operator.
/// </summary>
[SuppressMessage(
    "StyleCop.CSharp.MaintainabilityRules",
    "SA1401:Fields should be private",
    Justification = "<The static variables need to be public so they can be accessed by other classes>")]
public class MultiplicationOperatorNode : OperatorNode
{
    /// <summary>
    /// The character representation of the operation.
    /// </summary>
    public static char Operator { get; } = '*';

    /// <summary>
    /// The operator's precedence.
    /// </summary>
    public static int Precedence = 2;

    /// <summary>
    /// The oporator's assosiativity.
    /// </summary>
    public static string Assosiativity = "Left";

    /// <summary>
    /// Evaluates the product of the two child nodes.
    /// </summary>
    /// <returns>The product of the two child nodes.</returns>
    public override double Evaluate()
    {
        return this.LeftChild.Evaluate() * this.RightChild.Evaluate();
    }
}
