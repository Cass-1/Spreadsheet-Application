namespace SpreadsheetEngine;
using System.Reflection;

public class OperatorNodeFactory
{
    // TODO: singleton design pattern

    private Dictionary<char, Type> operatorTypes= new Dictionary<char, Type>();
    // TODO: initalize dictionary
    
    
    // TODO: make an isOperator method so that ExpressionTree can parse the expression
    
    public OperatorNode CreateOperatorNode(char op)
    {
        return (OperatorNode)System.Activator.CreateInstance(operatorTypes[op]);
    }
}