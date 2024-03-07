namespace SpreadsheetEngine;

public class AdditionOperatorNode : OperatorNode
{
    
    public static char Character = '+';
    
    public static int Precedence = 1;

    public static string Assosiativity = "Left";
    
     /// <summary>
    /// The left child node.
    /// </summary>
    private Node leftChild;

    /// <summary>
    /// The right child node.
    /// </summary>
    private Node rightChild;
    
    /// <summary>
    /// Evaluates the sum of the two child nodes.
    /// </summary>
    /// <returns>The sum of the two child nodes.</returns>
    public override double Evaluate()
    {
        return leftChild.Evaluate() + rightChild.Evaluate();
    }
}