using System.Runtime.CompilerServices;

namespace SpreadsheetEngine;

public class VariableNode : Node
{

    private string name;
    // TODO: pass a reference into the constructor to the variableDatabase in expressionTree
    public VariableNode(string name)
    {
        this.name = name;
    }
    
    public override double Evaluate()
    {
        
    }
}