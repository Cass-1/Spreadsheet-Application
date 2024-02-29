namespace SpreadsheetEngine;

public class ConstantNode : Node
{

    private double value;

    public ConstantNode(double val)
    {
        this.value = val;
    }
    
    public override double Evaluate()
    {
        return this.value;
    }
}