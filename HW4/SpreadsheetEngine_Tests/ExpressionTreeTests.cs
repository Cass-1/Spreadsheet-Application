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
}