namespace SpreadsheetEngine;

public class DivisionOperatorNode : OperatorNode
{
     /// <summary>
    /// The left child node.
    /// </summary>
    private Node leftChild;

    /// <summary>
    /// The right child node.
    /// </summary>
    private Node rightChild;
    
    /// <summary>
    /// Evaluates the product of the two child nodes.
    /// </summary>
    /// <returns>The product of the two child nodes.</returns>
    public override double Evaluate()
    {
        return leftChild.Evaluate() / rightChild.Evaluate();
    }
}