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
    private Dictionary<char, Type> operators = new Dictionary<char, Type>();

    private delegate void OnOperator(char op, Type type);

    /// <summary>
    /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
    /// </summary>
    public OperatorNodeFactory()
    {
        TraverseAvailableOperators((op, type) => operators.Add(op, type));
    }

    private void TraverseAvailableOperators(OnOperator onOperator)
    {

        // get the type declaration of OperatorNode
        var operatorNodeType = typeof(OperatorNode);
        var two = typeof(MultiplicationOperatorNode);
        // Iterate over all loaded assemblies:
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // Get all types that inherit from our OperatorNode class using LINQ
            var operatorTypes =
                assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

            // Iterate over those subclasses of OperatorNode
            foreach (var type in operatorTypes)
            {
                // for each subclass, retrieve the Operator property
                var operatorField = type.GetProperty("Operator");
                if (operatorField != null)
                {
                    // Get the character of the Operator
                    var value = operatorField.GetValue(type);

                    // If “Operator” property is not static, you will need to create
                    // an instance first and use the following code instead (or similar):
                    // object value = operatorField.GetValue(Activator.CreateInstance(type,
                    // new ConstantNode(0)));
                    if (value is char)
                    {
                        var operatorSymbol = (char)value;

                        // And invoke the function passed as parameter
                        // with the operator symbol and the operator class
                        onOperator(operatorSymbol, type);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Checks if a given operator is supported.
    /// </summary>
    /// <param name="op">The operator to check.</param>
    /// <returns>True or false.</returns>
    public bool IsOperator(char op)
    {
        return this.operators.ContainsKey(op);
    }

    /// <summary>
    /// Checks if a given operator is supported.
    /// </summary>
    /// <param name="op">The operator to check.</param>
    /// <returns>True or false.</returns>
    public bool IsOperator(string op)
    {
        return this.operators.ContainsKey(op.ToCharArray()[0]);
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

        return (OperatorNode)Activator.CreateInstance(this.operators[op])!;
    }

    /// <summary>
    /// Returns an OperatorNode based on the operator token.
    /// </summary>
    /// <param name="op">The operator character.</param>
    /// <returns>An operator node.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the operator given is not supported.</exception>
    public OperatorNode CreateOperatorNode(string op)
    {
        if (!this.IsOperator(op))
        {
            throw new InvalidOperationException();
        }

        return (OperatorNode)Activator.CreateInstance(this.operators[op.ToCharArray()[0]])!;
    }

    /// <summary>
    /// Gets the precedence of an operator.
    /// </summary>
    /// <param name="op">A character representing the operation.</param>
    /// <returns>The operator's precedence.</returns>
    /// <exception cref="InvalidOperationException">Is thrown in the inputted operation is not valid.</exception>
    public int GetOperatorPrecedence(char op)
    {
        if (this.IsOperator(op))
        {
            object? field = this.operators[op].GetField("Precedence")?.GetValue(null);
            if (field != null && field is int)
            {
                return (int)field;
            }
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Gets the precedence of an operator.
    /// </summary>
    /// <param name="inputOp">A string representing the operation.</param>
    /// <returns>The operator's precedence.</returns>
    /// <exception cref="InvalidOperationException">Is thrown in the inputted operation is not valid.</exception>
    public int GetOperatorPrecedence(string inputOp)
    {
        char op = inputOp.ToCharArray()[0];
        if (this.IsOperator(op))
        {
            // var test = this.operators[op].GetFields();
            object? field = this.operators[op].GetField("Precedence")?.GetValue(null);
            if (field != null && field is int)
            {
                return (int)field;
            }
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Gets the assosiativity of an operator.
    /// </summary>
    /// <param name="op">The operator.</param>
    /// <returns>The assosiativity of the operator.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an invalid operator is given.</exception>
    public string GetOperatorAssosiativity(char op)
    {
        if (this.IsOperator(op))
        {
            object? field = this.operators[op].GetField("Assosiativity")?.GetValue(null);
            if (field != null && field is string)
            {
                return (string)field;
            }
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Gets the assosiativity of an operator.
    /// </summary>
    /// <param name="op">The operator.</param>
    /// <returns>The assosiativity of the operator.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an invalid operator is given.</exception>
    public string GetOperatorAssosiativity(string op)
    {
        if (this.IsOperator(op.ToCharArray()[0]))
        {
            object? field = this.operators[op.ToCharArray()[0]].GetField("Assosiativity")?.GetValue(null);
            if (field != null && field is string)
            {
                return (string)field;
            }
        }

        throw new InvalidOperationException();
    }
}
