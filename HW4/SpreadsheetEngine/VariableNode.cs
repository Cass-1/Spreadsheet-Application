using System.Runtime.CompilerServices;

namespace SpreadsheetEngine;

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
    
    // TODO: pass a reference into the constructor to the variableDatabase in expressionTree
    public VariableNode(string name, ref Dictionary<string, double> dictionary)
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
        return this.dictionary[this.name];
    }
}