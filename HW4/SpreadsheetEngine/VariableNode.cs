// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

// this is because i don't want to overload Object.GetHashCode()
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()

namespace SpreadsheetEngine;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a node that is a variable.
/// </summary>
[SuppressMessage("ReSharper", "BaseObjectEqualsIsObjectEquals", Justification = "<I need to call base.Equals()>")]
public class VariableNode : Node
{
    /// <summary>
    /// The variable's name.
    /// </summary>
    private string name;

    /// <summary>
    /// A reference to the ExpressionTree's dictionary of all the variables.
    /// </summary>
    private Dictionary<string, double>? dictionary;

    /// <summary>
    /// Initializes a new instance of the <see cref="VariableNode"/> class.
    /// </summary>
    /// <param name="name">The name of the variable.</param>
    /// <param name="dictionary">A reference to the variable dictionary in the spreadsheet class.</param>
    public VariableNode(string name, ref Dictionary<string, double>? dictionary)
    {
        this.name = name;
        this.dictionary = dictionary;
    }

    /// <summary>
    /// Returns the variable's value.
    /// </summary>
    /// <returns>A double representing the variable's value.</returns>
    public override double Evaluate()
    {
        if (this.dictionary == null)
        {
            throw new ArgumentNullException();
        }

        if(!this.dictionary.ContainsKey(this.name))
        {
            throw new Exception();

        }
        return this.dictionary[this.name];
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        var other = obj as VariableNode;
        if (other == null)
        {
            return false;
        }

        return base.Equals(obj);
    }

    /// <summary>
    /// Compares two VariableNode's and determines if they are equal.
    /// </summary>
    /// <param name="other">The other object being compated to.</param>
    /// <returns>Whether the two objects are equal.</returns>
    public bool Equals(VariableNode other)
    {
        return this.name == other.name;
    }
}
