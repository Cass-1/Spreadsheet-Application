// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

using System.Reflection;

namespace SpreadsheetEngine_Tests;

using System.ComponentModel;
using SpreadsheetEngine;

/// <summary>
/// Tests for the ExpressionTree class.
/// </summary>
public class ExpressionTreeTests
{
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

        Assert.Equals(value, 5);
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

        Assert.Equals(value, 5);
    }

    /// <summary>
    /// Tests the creation of a variable with an invalid name.
    /// </summary>
    [Test]
    public void SetVariableInvalidBasicNameTest()
    {
        ExpressionTree expressionTree = new ExpressionTree(string.Empty);

        try
        {
            expressionTree.SetVariable("1", 5);
        }
        catch (Exception e)
        {
            Assert.True(e is InvalidEnumArgumentException);
        }

        Assert.Fail();
    }

    /// <summary>
    /// Tests the creation of a variable with an invalid name.
    /// </summary>
    [Test]
    public void SetVariableInvalidLongNameTest()
    {
        ExpressionTree expressionTree = new ExpressionTree(string.Empty);

        try
        {
            expressionTree.SetVariable("aaaaaa111111!asdf", 5);
        }
        catch (Exception e)
        {
            Assert.True(e is InvalidEnumArgumentException);
        }

        Assert.Fail();
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

        Assert.Equals(value, 12);
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

        Assert.Equals(value, 5);
    }

    /// <summary>
    /// Tests the GetVariable function trying to retreive a variable that doesn't exist.
    /// </summary>
    [Test]
    public void GetVariableNotExistingVariableTest()
     {
         ExpressionTree expressionTree = new ExpressionTree(string.Empty);

         try
         {
             expressionTree.GetVariable("A2");
         }
         catch (Exception e)
         {
             Assert.True(e is InvalidEnumArgumentException);
         }

         Assert.Fail();
     }

    /// <summary>
    /// Tests TokeniseExpression when there are only short constants.
    /// </summary>
    [Test]
    public void TokenizeExpressionShortConstantsTest()
    {
        ExpressionTree expressionTree = new ExpressionTree("5+1+9");

        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[]{ "5+1+9" });

        var correct = CheckTokenizedExpression(
            tokens,
            ["5", "+", "1", "+", "9"]);

        Assert.IsTrue(correct);
    }

    /// <summary>
    /// Tests TokeniseExpression when there are long constants.
    /// </summary>
    [Test]
    public void TokenizeExpressionLongConstantsTest()
    {
        ExpressionTree expressionTree = new ExpressionTree("533+123+9222222");

        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[]{ "533+123+9222222" });

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
        ExpressionTree expressionTree = new ExpressionTree("A22222+1-A1");

        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[]{ "A22222+1-A1" });

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
        ExpressionTree expressionTree = new ExpressionTree("1");

        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[]{ "1" });

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
        ExpressionTree expressionTree = new ExpressionTree("(+-*)");

        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[]{ "(+-*)" });

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
        ExpressionTree expressionTree = new ExpressionTree("*");
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[]{ "*" });

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
        ExpressionTree expressionTree = new ExpressionTree("1+A2+5-3*/");
        MethodInfo methodInfo = this.GetMethod("TokenizeExpression");
        var tokens = (List<string>)methodInfo.Invoke(this.objectUnderTest, new object?[]{ "1+A2+5-3*/"});

        var correct = CheckTokenizedExpression(
            tokens,
            ["1", "+", "A2", "+", "5", "-", "3", "*", "/"]);

        Assert.IsTrue(correct);
    }
    
    /// <summary>
    /// Tests ConvertExpressionToPostfix
    /// </summary>
    [Test]
    public void SimpleConvertExpressionToPostfixTest()
    {
      MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");
      List<string> infixTokens = new List<string> { "(", "1", "+", "2", ")", "/", "3" };
      object[] parameters = new object[] { infixTokens };
      List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters);
      
      Assert.IsTrue(CheckTokenizedExpression(postfix, new List<string>(["1", "2", "+", "3", "/"])));
    }
    
    
    /// <summary>
    /// Tests ConvertExpressionToPostfix
    /// </summary>
    [Test]
    public void SimpleConvertExpressionToPostfixTest2()
    {
      MethodInfo methodInfo = this.GetMethod("ConvertExpressionToPostfix");

      List<string> infixTokens = new List<string> { "1", "-", "2", "/", "3", "*", "3", "+", "4" };
      object[] parameters = new object[] { infixTokens };
      List<string> postfix = (List<string>)methodInfo.Invoke(this.objectUnderTest, parameters);
      
      Assert.IsTrue(postfix != null && CheckTokenizedExpression(postfix, new List<string>(["1", "2", "3", "/", "3", "*", "-", "4", "+"])));
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

    
    
    private ExpressionTree objectUnderTest = new ExpressionTree("");
    
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

        var method = this.objectUnderTest.GetType().GetMethod(methodName, BindingFlags.NonPublic |
            BindingFlags.Static | BindingFlags.Instance);

        if (method == null)
        {
            Assert.Fail(methodName + " method not found");
        }

        return method;
    }
}
