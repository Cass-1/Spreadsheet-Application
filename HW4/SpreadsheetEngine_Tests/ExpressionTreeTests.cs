namespace SpreadsheetEngine_Tests;

public class ExpressionTreeTests
{
    /// <summary>
    /// Tests a basic creation of a variable.
    /// </summary>
    [Test]
    public void SetVariableBasicTest()
    {
        ExpressionTree expressionTree = new ExpressionTree();
        
        expressionTree.SetVariable("A1", 5);
        
        // TODO: be able to access private members
        var value = expressionTree._variableDatabase["A1"];

        Assert.Equals(value, 5);
    }
    
   
    /// <summary>
    /// Testing the creation of a variable with a long name.
    /// </summary>
    [Test]
    public void SetVariableLongVariableNameTest()
    {
        ExpressionTree expressionTree = new ExpressionTree();
        
        expressionTree.SetVariable("a123123a342a4", 5);
        
        var value = expressionTree._variableDatabase["a123123a342a4"];

        Assert.Equals(value, 5);
    }
    
}