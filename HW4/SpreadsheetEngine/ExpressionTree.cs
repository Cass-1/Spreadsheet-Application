namespace SpreadsheetEngine;

public class ExpressionTree
{

    private Dictionary<string, double> _variableDatabase;

    private string _expression;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
    /// </summary>
    /// <param name="expression">The expression to evaluate.</param>
    public ExpressionTree(string expression)
    {
        // set the _expression
        this._expression = expression;

        // allocate variable database
        this._variableDatabase = new Dictionary<string, double>();

    }

    public void SetVariable(string variableName, double variableValue)
    {

    }

    public double Evaluate()
    {
        // evaluate the expression
        double result = 0;


        return result;
    }
    
    
    
}