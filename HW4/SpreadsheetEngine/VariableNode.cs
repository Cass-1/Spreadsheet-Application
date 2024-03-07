// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

/// <summary>
/// Represents a node that is a variable.
/// </summary>
public class VariableNode : Node
{
    /// <summary>
    /// The variable's name.
    /// </summary>
    private string name;

    /// <summary>
    /// A reference to the ExpressionTree's dictionary of all the variables.
    /// </summary>
    private Dictionary<string, double> dictionary;

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

    public VariableNode(string name)
    {
        this.name = name;
    }

    /// <summary>
    /// Returns the variable's value.
    /// </summary>
    /// <returns>A double representing the variable's value.</returns>
    public override double Evaluate()
    {
        return this.dictionary[this.name];
    }
    
    public override bool Equals(object? obj)
    {
        var other = obj as VariableNode;
        if (other == null)
        {
            return false;
        }
        
        return base.Equals(obj);
    }

    public bool Equals(VariableNode other)
    {
        return this.name == other.name;
    }
}
