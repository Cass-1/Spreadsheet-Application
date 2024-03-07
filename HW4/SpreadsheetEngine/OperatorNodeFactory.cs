// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Reflection;

namespace SpreadsheetEngine;

/// <summary>
/// This class is a factory for OperatorNodes.
/// </summary>
public class OperatorNodeFactory
{
    /// <summary>
    /// A dictionary that keeps track of the supported operations and the class types that represent them.
    /// </summary>
    private readonly Dictionary<char, Type> operatorTypes = new Dictionary<char, Type>();

    /// <summary>
    /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
    /// </summary>
    public OperatorNodeFactory()
    {
        // TODO: add more types
        this.operatorTypes.Add('*', typeof(MultiplicationOperatorNode));
        this.operatorTypes.Add('+', typeof(AdditionOperatorNode));
        this.operatorTypes.Add('-', typeof(SubtractionOperatorNode));
        this.operatorTypes.Add('/', typeof(DivisionOperatorNode));
    }

    /// <summary>
    /// Checks if a given operator is supported.
    /// </summary>
    /// <param name="op">The operator to check.</param>
    /// <returns>True or false.</returns>
    public bool IsOperator(char op)
    {
        return this.operatorTypes.ContainsKey(op);
    }

    /// <summary>
    /// Returns an OperatorNode based on the operator token.
    /// </summary>
    /// <param name="op">The operator character.</param>
    /// <returns>An operator node.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the operator given is not supported.</exception>
    public OperatorNode CreateOperatorNode(char op)
    {
        if (!this.IsOperator(op))
        {
            throw new InvalidOperationException();
        }

        return (OperatorNode)Activator.CreateInstance(this.operatorTypes[op])!;
    }

    /// <summary>
    /// Gets the precedence of an operator
    /// </summary>
    /// <param name="op">A character representing the operation</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Is thrown in the inputted operation is not valid</exception>
    public int GetOperatorPrecedence(char op)
    {
        if (this.IsOperator(op))
        {
            object field = this.operatorTypes[op]?.GetField("Precedence")?.GetValue(null);
            if (field != null && field is int) {
                return (int)field;
            }
        }

        throw new InvalidOperationException();
    }
}
