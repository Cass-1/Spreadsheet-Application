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
        this.expression = expression;

        // allocate variable database
        this.variableDatabase = new Dictionary<string, double>();

    }

    /// <summary>
    /// Sets of value of a variable. Creates a variable if it doesn't already exist.
    /// </summary>
    /// <param name="variableName">The name of the variable to create/update.</param>
    /// <param name="variableValue">The value to set the variable to.</param>
    public void SetVariable(string variableName, double variableValue)
    {

    }

    /// <summary>
    /// Gets the value of a specified variable.
    /// </summary>
    /// <param name="varName">The name of the variable to get.</param>
    /// <returns>The value of the variable.</returns>
    public double GetVariable(string varName)
    {
        return this.variableDatabase[varName];
    }

    public double Evaluate()
    {
        // evaluate the expression
        double result = 0;

        TokenizeExpession();
        
        ConvertExpressionToPostfix();

        this.root = GenerateExpressionTree();

        result = EvaluateExpressionTree();

        return result;
    }
    
}