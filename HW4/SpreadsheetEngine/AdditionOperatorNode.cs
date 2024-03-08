namespace SpreadsheetEngine;

public class AdditionOperatorNode : OperatorNode
{
    
    public static char Character = '+';
    
    public static int Precedence = 1;

    public static string Assosiativity = "Left";
    
    /// <summary>
    /// Evaluates the sum of the two child nodes.
    /// </summary>
    /// <returns>The sum of the two child nodes.</returns>
    public override double Evaluate()
    {
        return this.LeftChild.Evaluate() + this.RightChild.Evaluate();
    }
}