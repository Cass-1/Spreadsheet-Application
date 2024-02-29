using System.ComponentModel;
using System.Linq.Expressions;
using SpreadsheetEngine;

namespace SpreadsheetEngine_Tests;

public class ExpressionTreeTests
{
    /// <summary>
    /// Tests a basic creation of a variable.
    /// </summary>
    [Test]
    public void SetVariableCreateVariableTest()
    {
        ExpressionTree expressionTree = new ExpressionTree("");
        
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
        ExpressionTree expressionTree = new ExpressionTree("");
        
        expressionTree.SetVariable("a123123a342a4", 5);
        
        var value = expressionTree.GetVariable("a123123a342a4");

        Assert.Equals(value, 5);
    }
    
    /// <summary>
    /// Tests the creation of a variable with an invalid name
    /// </summary>
    [Test]
    public void SetVariableInvalidBasicNameTest()
    {
        ExpressionTree expressionTree = new ExpressionTree("");

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
    /// Tests the creation of a variable with an invalid name
    /// </summary>
    [Test]
    public void SetVariableInvalidLongNameTest()
    {
        ExpressionTree expressionTree = new ExpressionTree("");

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
        ExpressionTree expressionTree = new ExpressionTree("");
        expressionTree.SetVariable("A2", 5);
        expressionTree.SetVariable("A2", 12);
        
        var value = expressionTree.GetVariable("A2");
        
        Assert.Equals(value, 12);
    }
    
    [Test]
    public void GetVariableExistingVariableTest()
    {
        ExpressionTree expressionTree = new ExpressionTree("");
        expressionTree.SetVariable("A2", 5);

        var value = expressionTree.GetVariable("A2");

        Assert.Equals(value, 5);
    }
    
    [Test]
     public void GetVariableNotExistingVariableTest()
     {
         ExpressionTree expressionTree = new ExpressionTree("");

         try
         {
            var value = expressionTree.GetVariable("A2");
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

        expressionTree.TokenizeExpression();

        var correct = checkTokenizedExpression(
            expressionTree.tokenizedExpression,
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

        expressionTree.TokenizeExpression();

        var correct = checkTokenizedExpression(
            expressionTree.tokenizedExpression,
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
        
        expressionTree.SetVariable("A22222", 5);
        expressionTree.SetVariable("A1", 1);

        expressionTree.TokenizeExpression();
        
        var correct = checkTokenizedExpression(
            expressionTree.tokenizedExpression,
            ["A22222", "+", "1", "-", "A1"]);
        
        Assert.IsTrue(correct);
    }
    
     
    [Test]
    public void TokenizeExpressionOneValueTest()
    {
        ExpressionTree expressionTree = new ExpressionTree("1");
        
        expressionTree.TokenizeExpression();
        
        var correct = checkTokenizedExpression(
            expressionTree.tokenizedExpression,
            ["1"]);
        
    }

    [Test]
    public void TokenizeExpressionInvalidExpressionTest1()
    {
        ExpressionTree expressionTree = new ExpressionTree("++-*/");
        
        expressionTree.TokenizeExpression();
        
        var correct = checkTokenizedExpression(
            expressionTree.tokenizedExpression,
            ["+", "+", "-", "*", "/"]);
        
        Assert.IsTrue(correct);
    }
    
    [Test]
    public void TokenizeExpressionInvalidExpressionTest2()
    {
        ExpressionTree expressionTree = new ExpressionTree("*");

        expressionTree.TokenizeExpression();
        
        var correct = checkTokenizedExpression(
            expressionTree.tokenizedExpression,
            ["*"]);
        
        Assert.IsTrue(correct);
    }
    
    [Test]
    public void TokenizeExpressionInvalidExpressionTest3()
    {
        ExpressionTree expressionTree = new ExpressionTree("1+A2+5-3*/");

        expressionTree.TokenizeExpression();
        
        var correct = checkTokenizedExpression(
            expressionTree.tokenizedExpression,
            ["1", "+","A2", "+","5", "-", "3", "*", "/"]);
        
        Assert.IsTrue(correct);

    }

    /// <summary>
    /// Helper function to check if TokenizedExpression works
    /// </summary>
    /// <param name="tokens">The list of tokens generated by TokenizeExpression.</param>
    /// <param name="answerKey">What the correct tokenized result is.</param>
    /// <returns>If the two lists are the same.</returns>
    private bool checkTokenizedExpression(List<string> tokens, List<string> answerKey)
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
}

