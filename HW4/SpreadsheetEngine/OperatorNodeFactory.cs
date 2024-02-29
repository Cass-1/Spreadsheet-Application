using System.Numerics;

namespace SpreadsheetEngine;
using System.Reflection;

/// <summary>
/// This class is a factory for OperatorNodes.
/// </summary>
public class OperatorNodeFactory
{
    
    /// <summary>
    /// A dictionary that keeps track of the supported operations and the class types that represent them.
    /// </summary>
    private Dictionary<char, Type> operatorTypes= new Dictionary<char, Type>();

    /// <summary>
    /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
    /// </summary>
    /// <param name="str">The section of a mathmatical expression to creeate a node from </param>
    public OperatorNodeFactory()
    {
        
        // TODO: add more types
        operatorTypes.Add('*', typeof(MultiplicationOperatorNode));
        operatorTypes.Add('+', typeof(AdditionOperatorNode));
        operatorTypes.Add('-', typeof(SubtractionOperatorNode));
    }

    /// <summary>
    /// Checks if a given operator is supported.
    /// </summary>
    /// <param name="op">The operator to check.</param>
    /// <returns>True or false</returns>
    public bool IsOperator(char op)
    {
        return operatorTypes.ContainsKey(op);
    }
    
    /// <summary>
    /// Returns an OperatorNode based on the operator token.
    /// </summary>
    /// <param name="op">The operator character.</param>
    /// <returns>An operator node.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the operator given is not supported.</exception>
    public OperatorNode? CreateOperatorNode(char op)
    {
        if(!IsOperator(op))
        {
            throw new InvalidOperationException();
        }

        return (OperatorNode)System.Activator.CreateInstance(operatorTypes[op]);
    }
}