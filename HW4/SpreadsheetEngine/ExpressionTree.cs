// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine;

using NotImplementedException = System.NotImplementedException;

/// <summary>
/// Represents an expression as a binary tree. Used to evaluate expressions in the spreadsheet.
/// </summary>
public class ExpressionTree
{
    // TODO: make this private. It is currently public for testing but i should use refelction to test it
    /// <summary>
    /// A list of all the tokens in an expression.
    /// </summary>
    public List<string> TokenizedExpression = new List<string>();

    /// <summary>
    /// The actual expression tree.
    /// </summary>
    private Node root;

    /// <summary>
    /// The expression as a string.
    /// </summary>
    private string expression;

    /// <summary>
    /// A dictionary of all the variables.
    /// </summary>
    private Dictionary<string, double> variableDatabase;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
    /// </summary>
    /// <param name="expression">The expression to evaluate.</param>
    public ExpressionTree(string expression)
    {
        // set the _expression
        this.expression = expression;

        // allocate variable database
        this.variableDatabase = new Dictionary<string, double>();
    }

    /// <summary>
    /// Converts a list of tokens from infix to postfix.
    /// </summary>
    private void ConvertExpressionToPostfix()
    {
        // TODO: implement shunting yard algorithm
    }

    // TODO: make this private. It is currently public for testing but i should use refelction to test it
    /// <summary>
    /// Takes this.expression and populates this.tokenizedExpression.
    /// </summary>
    private List<string> TokenizeExpression(string expr)
    {
        OperatorNodeFactory operatorNodeFactory = new OperatorNodeFactory();
        List<string> tokens = new List<string>();

        // keeps track of any multicharacter token
        var buffer = string.Empty;

        // loop through the expressoin and tokenize it
        foreach (char c in expr)
        {
            // first check the one character tokens
            if (c == '(' || c == ')' || operatorNodeFactory.IsOperator(c))
            {
                // check if there is a multiCharacterToken that needs to be added
                if (buffer != string.Empty)
                {
                    tokens.Add(buffer);
                    buffer = string.Empty;
                }

                tokens.Add(c.ToString());
            }

            // either a constant or a variable
            else
            {
                // add the character onto the multicharacter token
                buffer += c;
            }
        }

        // check if there is a multiCharacterToken that needs to be added
        if (buffer != string.Empty)
        {
            tokens.Add(buffer);
        }

        return tokens;
    }

    /// <summary>
    /// Generates the expression tree from a list of tokenized strings.
    /// </summary>
    /// <returns>The root of the generated expression tree.</returns>
    /// <exception cref="NotImplementedException">Not implemented.</exception>
    private Node GenerateExpressionTree()
    {
        // TODO: Loop through postfix tokenized expression and generate nodes.
        throw new NotImplementedException();
    }

    /// <summary>
    /// Evaluates a postfix expression tree.
    /// </summary>
    /// <returns>The value of the evaluated expression tree.</returns>
    /// <exception cref="NotImplementedException">Not implemented.</exception>
    private double EvaluateExpressionTree()
    {
        // TODO: Recursively evaluate the expression tree.
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sets of value of a variable. Creates a variable if it doesn't already exist.
    /// </summary>
    /// <param name="variableName">The name of the variable to create/update.</param>
    /// <param name="variableValue">The value to set the variable to.</param>
    public void SetVariable(string variableName, double variableValue)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the value of a specified variable.
    /// </summary>
    /// <param name="varName">The name of the variable to get.</param>
    /// <returns>The value of the variable.</returns>
    public double GetVariable(string varName)
    {
        return this.variableDatabase[varName];
    }

    /// <summary>
    /// Evaluates the expression tree.
    /// </summary>
    /// <returns>The result of the evaluated tree.</returns>
    public double Evaluate()
    {
        // evaluate the expression
        double result;

        this.TokenizeExpression();

        this.ConvertExpressionToPostfix();

        this.root = this.GenerateExpressionTree();

        result = this.EvaluateExpressionTree();

        return result;
    }
}
