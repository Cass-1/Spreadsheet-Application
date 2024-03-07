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
    private List<string> TokenizedExpression = new List<string>();

    private List<string> PostFixTokenizedExpression = new List<string>();

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
    private List<string> ConvertExpressionToPostfix(List<string> infixTokens)
    {
        Stack<string> stack = new Stack<string>();
        List<string> postfixTokens = new List<string>();
        OperatorNodeFactory factory = new OperatorNodeFactory();

        foreach (var token in infixTokens)
        {
            // TODO: support operators with string symbols like nPr
            // step 1: If the incoming symbols is an operand, output it.
            if (!factory.IsOperator(token) && token != "(" && token != ")")
            {
                postfixTokens.Add(token);
            }
            
            // step 2: If the incoming symbol is a left parenthesis, push it on the stack.
            else if (token == "(")
            {
                stack.Push(token);
            }
            
            // step 3: If the incoming symbol is a right parenthesis...
            else if (token == ")")
            {
                // pop and output symbols until a left parenthsis is encountered 
                while (stack.Peek() != "(")
                {
                    postfixTokens.Add(stack.Pop());
                }

                stack.Pop();
            }
            
            // step 4: If the incoming symbol is an operator and the stack is empty or contains
            // a left parenthesis on top, push the incoming operator onto the stack.
            else if (factory.IsOperator(token) && (stack.Count == 0 || stack.Peek() == "("))
            {
                stack.Push(token);
            }
            
            // step 5.1: If the incoming symbol is an operator and has higher precedence
            // than the operator on the top of the stack -- push it on the stack.
            else if (factory.IsOperator(token) && (factory.GetOperatorPrecedence(token) >
                                                   factory.GetOperatorPrecedence(stack.Peek())))
            {
                stack.Push(token);
            }
            
            // step 5.2: If the incoming symbol is an operator and has the same precedence as
            // the operator on the top of the stack and is right associative -- push it on
            // the stack.
            else if(factory.IsOperator(token) && 
                    factory.GetOperatorPrecedence(token) == factory.GetOperatorPrecedence(stack.Peek()) &&
                    factory.GetOperatorAssosiativity(token) == "Right")

            {
                stack.Push(token);
            }
            
            /* step 6: 
               If the incoming symbol is an operator and has either lower precedence
               than the operator on the top of the stack, or has the same precedence as
               the operator on the top of the stack and is left associative -- continue to
               pop the stack until this is not true. Then, push the incoming operator.
            */ 
            else if (this.ShuntingYardStepSixCondition(ref stack, ref factory, token)){
                while (this.ShuntingYardStepSixCondition(ref stack, ref factory, token))
                {
                    postfixTokens.Add(stack.Pop());
                }
                stack.Push(token);
            }

        }
        
        // step 7: At the end of the expression, pop and print all operators on the stack.
        // (No parentheses should remain.)
        while (stack.Count != 0)
        {
            postfixTokens.Add(stack.Pop());
        }

        return postfixTokens;
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

        this.TokenizedExpression = this.TokenizeExpression(this.expression);

        var infixTokens = this.ConvertExpressionToPostfix(this.TokenizedExpression);

        this.root = this.GenerateExpressionTree();

        result = this.EvaluateExpressionTree();

        return result;
    }

    /// <summary>
    /// Checks the condition for step six in the shunting yard algorithm.
    /// </summary>
    /// <param name="stack">The shunting yard algorithm stack.</param>
    /// <param name="factory">The shunting yard OperatorNodeFactory.</param>
    /// <param name="token">The shunting yard current token.</param>
    /// <returns>Whether the conidition is true or not</returns>
    private bool ShuntingYardStepSixCondition(ref Stack<string> stack, ref OperatorNodeFactory factory, string token)
    {
        // If the incoming symbol is an operator and has either lower precedence
        // than the operator on the top of the stack, or has the same precedence as
        // the operator on the top of the stack and is left associative
        string a;
        if (token == "(" || token == ")")
        {
            return false;
        }

        if (stack.Count == 0)
        {
            return false;
        }

        if (stack.Peek() == "(" || stack.Peek() == ")")
        {
            return false;
        }
        return factory.IsOperator(token) && (
                    (factory.GetOperatorPrecedence(token) < factory.GetOperatorPrecedence(stack.Peek())) ||
                    (factory.GetOperatorPrecedence(token) == factory.GetOperatorPrecedence(stack.Peek()) &&
                     factory.GetOperatorAssosiativity(token) == "Left"));
    }
}
