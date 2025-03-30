// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace ExpressionTreeConsole;

using SpreadsheetEngine;

/// <summary>
/// The class that reads and responds to input from the user.
/// </summary>
public class Application
{
    /// <summary>
    /// A private expression tree.
    /// </summary>
    private ExpressionTree expressionTree = new ExpressionTree("A1+B1+C1");

    /// <summary>
    /// The screen for the main menu. Displays the text and reads user input.
    /// </summary>
    public void MainMenuScreen()
    {
        // print the expression
        Console.WriteLine("Expression: " + this.expressionTree.Expression);

        // print the txt
        MenuText.PrintMainMenu();

        // get user input
        this.MainMenu(MenuText.MainMenuOptions);
    }

    /// <summary>
    /// The screen used for entering expressions.
    /// </summary>
    public void ExpressionScreen()
    {
        // print out the menu text
        MenuText.PrintExpressionMenu();

        // read user input
        this.ExpressionMenu();

        // return to the main menu screen
        this.MainMenuScreen();
    }

    /// <summary>
    /// The screen used for entering variables.
    /// </summary>
    public void VariableScreen()
    {
        // print out the menu text
        MenuText.PrintVariableMenu();

        // get user input
        this.VariableMenu();

        // return to the main menu screen
        this.MainMenuScreen();
    }

    /// <summary>
    /// The screen used for evaluating the tree.
    /// </summary>
    public void EvaluateScreen()
    {
        // print out the menu text
        MenuText.PrintEvaluateMenu();

        // evaluate the tree
        this.EvaluateMenu();

        // return to main menu
        this.MainMenuScreen();
    }

    /// <summary>
    /// The screen used for quitting.
    /// </summary>
    public void QuitScreen()
    {
        // print the quitting menu
        MenuText.PrintQuitMenu();
    }

    /// <summary>
    /// Run the applciation.
    /// </summary>
    public void RunApp()
    {
        this.MainMenuScreen();
    }

    /// <summary>
    /// Checks if a given string input is a valid input choice.
    /// </summary>
    /// <param name="rawInput">The string input.</param>
    /// <param name="validInputs">The valid inputs.</param>
    /// <param name="result">The input the user selected. Null if the input is not valid.</param>
    /// <returns>Whether the input was valid.</returns>
    private bool CheckInput(string rawInput, List<int> validInputs, out int? result)
    {
        int cleanedInput;

        bool sucsessfulClean = int.TryParse(rawInput, out cleanedInput);

        if (sucsessfulClean && validInputs.Contains(cleanedInput))
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
    /// Changes screen based on user input.
    /// </summary>
    private void MainMenu(List<int> validInputs)
    {
        string? rawInput;
        int? usersChoice = 0;
        bool validChoice;
        do
        {
            rawInput = Console.ReadLine();

            validChoice = rawInput != null && this.CheckInput(rawInput, validInputs, out usersChoice);
        }
        while (!validChoice);

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

    /// <summary>
    /// Asks the user for an expression for the expression tree and creates a new expression tree.
    /// </summary>
    private void ExpressionMenu()
    {
        string expression = Console.ReadLine() ?? string.Empty;

        this.expressionTree = new ExpressionTree(expression);
    }

    /// <summary>
    /// Asks the user for a variable and a value for that variable.
    /// </summary>
    private void VariableMenu()
    {
        {
            string variableName;
            double variableValue;

            Console.WriteLine("Enter a variable name: ");
            variableName = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter the variable's value: ");
            variableValue = double.Parse(Console.ReadLine() ?? string.Empty);

            this.expressionTree.SetVariable(variableName, variableValue);
        }
    }

    /// <summary>
    /// Evaluates the expression tree.
    /// </summary>
    private void EvaluateMenu()
    {
        Console.WriteLine(this.expressionTree.Evaluate());
    }
}
