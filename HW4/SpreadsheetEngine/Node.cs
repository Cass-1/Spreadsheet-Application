// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// A node.
/// </summary>
public abstract class Node
{
    /// <summary>
    /// Evaluates the value of the node.
    /// </summary>
    /// <returns>The value of the node.</returns>
    public abstract double Evaluate();
}
