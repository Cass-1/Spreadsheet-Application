// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine_Tests;

using System.Reflection;
using SpreadsheetEngine;

/// <summary>
/// Tests for the ExpressionTree class.
/// </summary>
public class ExpressionTreeTests
{
    /// <summary>
    /// The object to be tested.
    /// </summary>
    private ExpressionTree objectUnderTest = new ExpressionTree(string.Empty);

    /// <summary>
    /// Tests a basic creation of a variable.
    /// </summary>
    [Test]
    public void SetVariableCreateVariableTest()
    {
        ExpressionTree expressionTree = new ExpressionTree(string.Empty);

        expressionTree.SetVariable("A1", 5);

        // TODO: be able to access private members
        var value = expressionTree.GetVariable("A1");

        Assert.That(value, Is.EqualTo(5));
    }

    /// <summary>
    /// Testing the creation of a variable with a long name.
    /// </summary>
    [Test]
    public void SetVariableCreateLongVariableNameTest()
    {
        ExpressionTree expressionTree = new ExpressionTree(string.Empty);

        expressionTree.SetVariable("a123123a342a4", 5);

        var value = expressionTree.GetVariable("a123123a342a4");

        Assert.That(value, Is.EqualTo(5));
    }

    /// <summary>
    /// Tests the creation of a variable with an invalid name.
    /// </summary>
    [Test]
    public void SetVariableInvalidBasicNameTest()
    {
        ExpressionTree expressionTree = new ExpressionTree(string.Empty);
        bool flag = false;

        try
        {
            expressionTree.SetVariable("1", 5);
        }
        catch (ArgumentException)
        {
            flag = true;
        }

        Assert.IsTrue(flag);
    }

    /// <summary>
    /// Tests if SetVariable can update the value of a variable.
    /// </summary>
    [Test]
    public void SetVariableUpdateExistingVariableTest()
    {
        ExpressionTree expressionTree = new ExpressionTree(string.Empty);
        expressionTree.SetVariable("A2", 5);
        expressionTree.SetVariable("A2", 12);

        var value = expressionTree.GetVariable("A2");

        Assert.That(value, Is.EqualTo(12));
    }

    /// <summary>
    /// Tests the GetVariable function for a variable that exists.
    /// </summary>
    [Test]
    public void GetVariableExistingVariableTest()
    {
        ExpressionTree expressionTree = new ExpressionTree(
            string.Empty);
        expressionTree.SetVariable("A2", 5);

        var value = expressionTree.GetVariable("A2");

        Assert.That(value, Is.EqualTo(5));
    }

    /// <summary>
    /// A simple test for SetVariable.
    /// </summary>
    [Test]
    public void SetVariableSimpleTest()
    {
        ExpressionTree tree = new ExpressionTree(string.Empty);
        tree.SetVariable("A2", 5);

        Assert.That(tree.GetVariable("A2"), Is.EqualTo(5));
    }

    /// <summary>
    /// A simple test for SetVariable.
    /// </summary>
    [Test]
    public void SetVariableSimpleTest2()
    {
        ExpressionTree tree = new ExpressionTree(string.Empty);
        tree.SetVariable("A23456", 5);

        Assert.That(tree.GetVariable("A23456"), Is.EqualTo(5));
    }

    /// <summary>
    /// Tests the SetVariable method when no value is given for a variable.
    /// </summary>
    [Test]
    public void SetVariableNoValueGivenTest()
    {
        ExpressionTree tree = new ExpressionTree(string.Empty);
        tree.SetVariable("A23456");

        Assert.That(tree.GetVariable("A23456"), Is.EqualTo(0));
    }

    /// <summary>
    /// Tests the SetVariable method when a variable is incorrectly named.
    /// </summary>
    [Test]
    public void SetVariableImproperNameTest()
    {
        ExpressionTree tree = new ExpressionTree(string.Empty);
        bool check = false;

        try
        {
            tree.SetVariable("2ab");
        }
        catch (ArgumentException)
        {
            check = true;
        }

        Assert.True(check);
    }

    /// <summary>
    /// Tests the GetVariable function trying to retreive a variable that doesn't exist.
    /// </summary>
    [Test]
    public void GetVariableNotExistingVariableTest()
     {
         ExpressionTree expressionTree = new ExpressionTree(string.Empty);
         bool flag = false;

         try
         {
             expressionTree.GetVariable("A2");
         }
         catch (KeyNotFoundException)
         {
             flag = true;
         }

         Assert.IsTrue(flag);
     }

    /// <summary>
    /// Tests TokeniseExpression when there are only short constants.
    /// </summary>
    [Test]
    public void TokenizeExpressionShortConstantsTest()
    {
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[] { "5+1+9" })!;

        var correct = CheckTokenizedExpression(
            tokens: tokens,
            ["5", "+", "1", "+", "9"]);

        Assert.IsTrue(correct);
    }

    /// <summary>
    /// Tests TokeniseExpression when there are long constants.
    /// </summary>
    [Test]
    public void TokenizeExpressionLongConstantsTest()
    {
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[] { "533+123+9222222" })!;

        var correct = CheckTokenizedExpression(
            tokens,
            ["533", "+", "123", "+", "9222222"]);

        Assert.IsTrue(correct);
    }

    /// <summary>
    /// Tests TokenizeExpression when there are variables inthe expression.
    /// </summary>
    [Test]
    public void TokenizeExpressionVariablesTest()
    {
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[] { "A22222+1-A1" })!;

        var correct = CheckTokenizedExpression(
            tokens,
            ["A22222", "+", "1", "-", "A1"]);

        Assert.IsTrue(correct);
    }

    /// <summary>
    /// Tests TokenizeExpression for an expression with a single value.
    /// </summary>
    [Test]
    public void TokenizeExpressionOneValueTest()
    {
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[] { "1" })!;

        var correct = CheckTokenizedExpression(
            tokens,
            ["1"]);

        Assert.IsTrue(correct);
    }

    /// <summary>
    /// Tests tokenize expression on an invalid expression. I want to make sure tokenize expression can still
    /// tokenize invalid expressions properly.
    /// </summary>
    [Test]
    public void TokenizeExpressionInvalidExpressionTest1()
    {
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[] { "(+-*)" })!;

        var correct = CheckTokenizedExpression(
            tokens,
            ["(", "+", "-", "*", ")"]);

        Assert.IsTrue(correct);
    }

    /// <summary>
    /// Tests TokenizeExpression with an invalid expression.
    /// </summary>
    [Test]
    public void TokenizeExpressionInvalidExpressionTest2()
    {
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[] { "*" })!;

        var correct = CheckTokenizedExpression(
            tokens,
            ["*"]);

        Assert.IsTrue(correct);
    }

    /// <summary>
    /// Tests TokenizeExpression with an invalid expression.
    /// </summary>
    [Test]
    public void TokenizeExpressionInvalidExpressionTest3()
    {
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[] { "1+A2+5-3*/" })!;

        var correct = CheckTokenizedExpression(
            tokens,
            ["1", "+", "A2", "+", "5", "-", "3", "*", "/"]);

        Assert.IsTrue(correct);
    }

    /// <summary>
    /// Tests ConvertExpressionToPostfix.
    /// </summary>
    [Test]
    public void SimpleConvertExpressionToPostfixTest()
    {
      MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");
      List<string> infixTokens = new List<string> { "(", "1", "+", "2", ")", "/", "3" };
      object[] parameters = new object[] { infixTokens };
      List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

      Assert.IsTrue(CheckTokenizedExpression(postfix, new List<string>(["1", "2", "+", "3", "/"])));
    }

    /// <summary>
    /// Tests ConvertExpressionToPostfix.
    /// </summary>
    [Test]
    public void SimpleConvertExpressionToPostfixTest2()
    {
      MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");

      List<string> infixTokens = new List<string> { "1", "-", "2", "/", "3", "*", "3", "+", "4" };
      object[] parameters = new object[] { infixTokens };
      List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

      Assert.IsTrue(CheckTokenizedExpression(postfix, new List<string>(["1", "2", "3", "/", "3", "*", "-", "4", "+"])));
    }

    /// <summary>
    /// Tests ConvertExpressionToPostfix.
    /// </summary>
    [Test]
    public void SimpleConvertExpressionToPostfixTest3()
    {
        MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");

        List<string> infixTokens = new List<string> { "(", "1", "+", "2", ")", "/", "3" };
        List<string> expectedPostfix = new List<string> { "1", "2", "+", "3", "/" };
        object[] parameters = new object[] { infixTokens };

        List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

        Assert.IsTrue(CheckTokenizedExpression(postfix, expectedPostfix));
    }

    /// <summary>
    /// Tests ConvertExpressionToPostfix.
    /// </summary>
    [Test]
    public void ComplexConvertExpressionToPostfixTest()
    {
        MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");

        List<string> infixTokens = new List<string> { "(", "2", "*", "3", "+", "4", ")", "/", "(", "2", "+", "5", ")" };
        List<string> expectedPostfix = new List<string> { "2", "3", "*", "4", "+", "2", "5", "+", "/" };
        object[] parameters = new object[] { infixTokens };

        List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

        Assert.IsTrue(CheckTokenizedExpression(postfix, expectedPostfix));
    }

    /// <summary>
    /// Tests ConvertExpressionToPostfix with assosiativity.
    /// </summary>
    [Test]
    public void WithAssosiativityConvertExpressionToPostfixTest()
    {
        MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");

        List<string> infixTokens = new List<string> { "2", "+", "3", "*", "4" };
        List<string> expectedPostfix = new List<string> { "2", "3", "4", "*", "+" };
        object[] parameters = new object[] { infixTokens };

        List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

        Assert.IsTrue(CheckTokenizedExpression(postfix, expectedPostfix));
    }

    /// <summary>
    /// Tests ConvertExpressionToPostfix with a single operator.
    /// </summary>
    [Test]
    public void SingleOperandConvertExpressionToPostfixTest()
    {
        MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");

        List<string> infixTokens = new List<string> { "10" };
        List<string> expectedPostfix = new List<string> { "10" };
        object[] parameters = new object[] { infixTokens };

        List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

        Assert.IsTrue(CheckTokenizedExpression(postfix, expectedPostfix));
    }

    /// <summary>
    /// Tests ConvertExpressionToPostfix with nested parenthesis.
    /// </summary>
    [Test]
    public void NestedParenthesisConvertExpressionToPostfixTest()
    {
        MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");

        List<string> infixTokens = new List<string> { "(", "(", "1", "+", "2", ")", "*", "3", ")", "-", "4" };
        List<string> expectedPostfix = new List<string> { "1", "2", "+", "3", "*", "4", "-" };
        object[] parameters = new object[] { infixTokens };

        List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

        Assert.IsTrue(CheckTokenizedExpression(postfix, expectedPostfix));
    }

    /// <summary>
    /// Tests TokensToNodes with a simple test.
    /// </summary>
    [Test]
    public void TokensToNodesSimpleTest()
    {
        MethodInfo methodInfo = this.GetMethod("TokensToNodes");

        List<string> postfixTokens = new List<string> { "1", "2", "+", "3", "*" };
        List<Node> expectedNodes = new List<Node> { new ConstantNode(1), new ConstantNode(2), new AdditionOperatorNode(), new ConstantNode(3), new MultiplicationOperatorNode() };
        object[] parameters = new object[] { postfixTokens };

        List<Node> nodes = (List<Node>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

        Assert.IsTrue(CheckNodeList(nodes, expectedNodes));
    }

    /// <summary>
    /// Tests TokensToNodes with a test using multiple operators.
    /// </summary>
    [Test]
    public void TokensToNodesMultipleOperatorsTest()
    {
        MethodInfo methodInfo = this.GetMethod("TokensToNodes");

        List<string> postfixTokens = new List<string> { "1", "2", "+", "3", "-", "4", "*", "5", "/" };
        List<Node> expectedNodes = new List<Node> { new ConstantNode(1), new ConstantNode(2), new AdditionOperatorNode(), new ConstantNode(3), new SubtractionOperatorNode(), new ConstantNode(4), new MultiplicationOperatorNode(), new ConstantNode(5), new DivisionOperatorNode() };
        object[] parameters = new object[] { postfixTokens };

        List<Node> nodes = (List<Node>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

        Assert.IsTrue(CheckNodeList(nodes, expectedNodes));
    }

    /// <summary>
    /// Tests TokensToNodes with an expression that includes variables.
    /// </summary>
    [Test]
    public void TokensToNodesWithVariablesTest()
    {
        MethodInfo methodInfo = this.GetMethod("TokensToNodes");
        Dictionary<string, double>? dictionary = new Dictionary<string, double>();

        List<string> postfixTokens = new List<string> { "x2", "2", "*", "3", "var3", "*", "+", "5" };
        List<Node> expectedNodes = new List<Node> { new VariableNode("x2", ref dictionary), new ConstantNode(2), new MultiplicationOperatorNode(), new ConstantNode(3), new VariableNode("var3", ref dictionary), new MultiplicationOperatorNode(), new AdditionOperatorNode(), new ConstantNode(5) };
        object[] parameters = new object[] { postfixTokens };

        List<Node> nodes = (List<Node>)methodInfo.Invoke(this.objectUnderTest, parameters)!;

        Assert.IsTrue(CheckNodeList(nodes, expectedNodes));
    }

    /// <summary>
    /// Simple test for the whole expression tree.
    /// </summary>
    [Test]
    public void ExpressionTreeSimpleTest()
    {
        ExpressionTree tree = new ExpressionTree("1+2/2");
        var result = tree.Evaluate();

        Assert.That(result, Is.EqualTo(2));
    }

    /// <summary>
    /// Complex test for the whole expression tree.
    /// </summary>
    [Test]
    public void ExpressionTreeComplexTest1()
    {
        ExpressionTree tree = new ExpressionTree("((8-2)*(5+3))/(6-1)");
        var result = tree.Evaluate();

        Assert.That(result, Is.EqualTo(9.6));
    }

    /// <summary>
    /// Complex test for the whole expression tree.
    /// </summary>
    [Test]
    public void ExpressionTreeComplexTest2()
    {
        ExpressionTree tree = new ExpressionTree("((10 / 2) * (4 + 2)) - (3 * (6 + 2))");
        var result = tree.Evaluate();

        Assert.That(result, Is.EqualTo(6));
    }

    private static bool CheckNodeList(List<Node> nodes, List<Node> answerKey)
    {
        if (nodes.Count != answerKey.Count)
        {
            return false;
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            var testNode = nodes.ElementAt(i);
            var answerNode = answerKey.ElementAt(i);
            if (testNode.Equals(answerNode))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Helper function to check if TokenizedExpression works.
    /// </summary>
    /// <param name="tokens">The list of tokens generated by TokenizeExpression.</param>
    /// <param name="answerKey">What the correct tokenized result is.</param>
    /// <returns>If the two lists are the same.</returns>
    private static bool CheckTokenizedExpression(List<string> tokens, List<string> answerKey)
    {
        if (tokens.Count != answerKey.Count)
        {
            return false;
        }

        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens.ElementAt(i) != answerKey.ElementAt(i))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Gets a private method of the ExpressionTree class.
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    /// <returns>The method.</returns>
    private MethodInfo GetMethod(string methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            Assert.Fail("methodName is null or whitespace");
        }

        var method = this.objectUnderTest.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

        if (method == null)
        {
            Assert.Fail(methodName + " method not found");
        }

        return method!;
    }
}
