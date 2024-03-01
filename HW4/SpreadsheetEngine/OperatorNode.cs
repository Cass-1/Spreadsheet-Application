// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// Represents a node that is an operator.
/// </summary>
public abstract class OperatorNode : Node
{
    /// <summary>
    /// The character representation of the operator.
    /// </summary>
    protected char op;
}
