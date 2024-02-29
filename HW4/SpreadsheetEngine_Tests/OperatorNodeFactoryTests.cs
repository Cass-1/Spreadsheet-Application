using SpreadsheetEngine;

namespace SpreadsheetEngine_Tests;

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
            var operatorNode = factory.CreateOperatorNode(']');
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is InvalidOperationException);
            return;
        }

        Assert.Fail();
    }
    
}