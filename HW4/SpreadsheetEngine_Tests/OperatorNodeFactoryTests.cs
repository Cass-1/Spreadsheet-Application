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
}
