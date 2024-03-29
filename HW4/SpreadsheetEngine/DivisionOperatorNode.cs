// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A node that represents the division operator.
/// </summary>
[SuppressMessage(
    "StyleCop.CSharp.MaintainabilityRules",
    "SA1401:Fields should be private",
    Justification = "<The static variables need to be public so they can be accessed by other classes>")]
public class DivisionOperatorNode : OperatorNode
{
    /// <summary>
    /// The operator's precedence.
    /// </summary>
    public static int Precedence = 2;

    /// <summary>
    /// The oporator's assosiativity.
    /// </summary>
    public static string Assosiativity = "Left";

    /// <summary>
    /// Gets the character representation of the operation.
    /// </summary>
    public static char Operator { get; } = '/';

    /// <summary>
    /// Evaluates the quotient of the two child nodes.
    /// </summary>
    /// <returns>The quotient of the two child nodes.</returns>
    public override double Evaluate()
    {
        return this.LeftChild.Evaluate() / this.RightChild.Evaluate();
    }
}
