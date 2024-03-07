// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace SpreadsheetEngine_Tests;

using SpreadsheetEngine;

/// <summary>
/// Tests for the OperatorNodeFactory class.
/// </summary>
public class OperatorNodeFactoryTests
{
    /// <summary>
    /// Tests the creation of a MultiplicationOperatorNode.
    /// </summary>
    [Test]
    public void CreateOperatorNodeMultiplicationTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        var operatorNode = factory.CreateOperatorNode('*');

        Assert.IsTrue(operatorNode is MultiplicationOperatorNode);
    }

    /// <summary>
    /// Tests the creation of an AdditionOperatorNode.
    /// </summary>
    [Test]
    public void CreateOperatorNodeAdditionTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        var operatorNode = factory.CreateOperatorNode('+');

        Assert.IsTrue(operatorNode is AdditionOperatorNode);
    }

     /// <summary>
     /// Tests what happens when we try and create an unsupported operation.
     /// </summary>
    [Test]
    public void CreateOperatorNodeNonExistantOperatorTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        try
        {
            factory.CreateOperatorNode(']');
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is InvalidOperationException);
            return;
        }

        Assert.Fail();
    }
     
    /// <summary>
    /// Tests what happens when we pass in a invalid operator
    /// </summary>
    [Test]
    public void CreateOperatorNotAnOperatorTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        try
        {
            factory.CreateOperatorNode("sssss");
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is InvalidOperationException);
            return;
        }

        Assert.Fail();
    }

    [Test]
    public void GetPrecedenceAdditionTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        int precedence = factory.GetOperatorPrecedence('+');
        
        Assert.AreEqual(1, precedence);
    }
    
    [Test]
    public void GetPrecedenceSubtractionTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        int precedence = factory.GetOperatorPrecedence('-');
        
        Assert.AreEqual(1, precedence);
    }
    
    
    [Test]
    public void GetPrecedenceMultiplicationTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        int precedence = factory.GetOperatorPrecedence('*');
        
        Assert.AreEqual(2, precedence);
    }
    
    [Test]
    public void GetPrecedenceDivisionTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        int precedence = factory.GetOperatorPrecedence('/');
        
        Assert.AreEqual(2, precedence);
    }
    
    
    [Test]
    public void GetPrecedenceInvalidOperatorTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();
        try
        {
            int precedence = factory.GetOperatorPrecedence('a');
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is InvalidOperationException);
            return;
        }
        
        Assert.Fail();
    }
    
    /// <summary>
    /// Tests the GetOperator function for addition.
    /// </summary>
    [Test]
    public void GetAssosiativityAdditionTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        string precedence = factory.GetOperatorAssosiativity('+');
        
        Assert.AreEqual("Left", precedence);
    }
    
    /// <summary>
    /// Tests the GetOperator function for subtraction.
    /// </summary>
    [Test]
    public void GetAssosiativitySubtractionTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        string precedence = factory.GetOperatorAssosiativity('-');
        
        Assert.AreEqual("Left", precedence);
    }
    
    /// <summary>
    /// Tests the GetOperator function for multiplication.
    /// </summary>
    [Test]
    public void GetAssosiativityMultiplicationTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        string precedence = factory.GetOperatorAssosiativity('*');
        
        Assert.AreEqual("Left", precedence);
    }
    
    /// <summary>
    /// Tests the GetOperator function for division.
    /// </summary>
    [Test]
    public void GetAssosiativityDivisionTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();

        string precedence = factory.GetOperatorAssosiativity('/');
        
        Assert.AreEqual("Left", precedence);
    }
    
    /// <summary>
    /// Tests the GetOperator function for an invalid operation.
    /// </summary>
    [Test]
    public void GetAssosiativityInvalidOperatorTest()
    {
        OperatorNodeFactory factory = new OperatorNodeFactory();
        try
        {
            string precedence = factory.GetOperatorAssosiativity('a');
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is InvalidOperationException);
            return;
        }
        
        Assert.Fail();
    }
}
