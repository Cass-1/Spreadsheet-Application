using SpreadsheetEngine;

namespace ExpressionTreeConsole;

/// <summary>
/// The class that reads and responds to input from the user.
/// </summary>
public class Application
{
    private ExpressionTree expressionTree = null;

    public void MainMenuScreen()
    {
        MenuText.PrintMainMenu();

        this.mainMenu(MenuText.MainMenuOptions);
    }

    public void ExpressionScreen()
    {
        MenuText.PrintExpressionMenu();

        this.expressionMenu();
        
        MainMenuScreen();
    }

    public void VariableScreen()
    {
        MenuText.PrintVariableMenu();

        this.variableMenu();
        
        MainMenuScreen();
    }

    public void EvaluateScreen()
    {
        MenuText.PrintEvaluateMenu();

        this.evaluateMenu();
        
        MainMenuScreen();
    }

    public void QuitScreen()
    {
        MenuText.PrintQuitMenu();
        return;
    }
    
    public void RunApp()
    {
        this.MainMenuScreen();
    }

    /// <summary>
    /// Checks if a given string input is a valid input choice.
    /// </summary>
    /// <param name="rawInput">The string input.</param>
    /// <param name="validInputs">The valid inputs.</param>
    /// <param name="result">The input the user selected. Null if the input is not valid</param>
    /// <returns>Whether the input was valid.</returns>
    private bool checkInput(string rawInput, List<int> validInputs, out int? result)
    {
        int cleanedInput = 0;

        bool sucsessfulClean = Int32.TryParse(rawInput, out cleanedInput);

        if (validInputs.Contains(cleanedInput))
        {
            result = cleanedInput;
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// Changes menu based on user input.
    /// </summary>
    private void mainMenu(List<int> validInputs)
    {
        string? rawInput = "";
        int? usersChoice = 0;
        bool validChoice = false;
        do
        {
            rawInput = Console.ReadLine();
            
            validChoice = rawInput != null && checkInput(rawInput, validInputs, out usersChoice);
        } while (!validChoice);

        switch (usersChoice)
        {
            case 1:
                this.ExpressionScreen();
                break;
            case 2:
                this.VariableScreen();
                break;
            case 3:
                this.EvaluateScreen();
                break;
            case 4:
                this.QuitScreen();
                break;
        }
    }

    private void expressionMenu()
    {
        string expression = Console.ReadLine();

        this.expressionTree = new ExpressionTree(expression);
    }
    
    private void variableMenu()
    {
        if (this.expressionTree != null)
        {
            string variableName = string.Empty;
            double variableValue = 0.0;
            
            Console.WriteLine("Enter a variable name: ");
            variableName = Console.ReadLine(); 
            
            Console.WriteLine("Enter the variable's value: ");
            variableValue = double.Parse(Console.ReadLine());

            this.expressionTree.SetVariable(variableName, variableValue);
        }
        else
        {
            Console.WriteLine("Please enter expression first.");
        }
    }

    private void evaluateMenu()
    {
        if (this.expressionTree != null)
        {
            Console.WriteLine(this.expressionTree.Evaluate());
        }
        else
        {
            Console.WriteLine("Please enter expression first.");
        }
    }
}