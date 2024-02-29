namespace SpreadsheetEngine;

public class ExpressionTree
{

    /// <summary>
    /// A dictionary of all the variables.
    /// </summary>
    private Dictionary<string, double> variableDatabase;
    

    private List<string> tokenizedExpression;

    /// <summary>
    /// The actual expression tree.
    /// </summary>
    private Node root;

    /// <summary>
    /// The expression as a string.
    /// </summary>
    private string expression;
    
    private void ConvertExpressionToPostfix()
    {
        
    }

    private void TokenizeExpession()
    {
        
    }

    private void CheckExpressionTree()
    {
        
    }
    
    private Node GenerateExpressionTree()
    {
        foreach (var item in tokenizedExpression)
        {
            
        }
    }


    private double EvaluateExpressionTree()
    {
        
    }

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