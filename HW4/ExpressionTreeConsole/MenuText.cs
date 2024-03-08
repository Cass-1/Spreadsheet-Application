namespace ExpressionTreeConsole;

/// <summary>
/// Stores the static text used for the menus.
/// </summary>
public static class MenuText
{
    /// <summary>
    /// The main menu option numbers.
    /// </summary>
    public static readonly List<int> MainMenuOptions = new List<int> { 1, 2, 3, 4 };

    /// <summary>
    /// Prints out the main menu.
    /// </summary>
    public static void PrintMainMenu()
    {
        Console.WriteLine("Menu");
        Console.WriteLine(MainMenuOptions[0] + ") Enter a new expression.");
        Console.WriteLine(MainMenuOptions[1] + ") Set a variable value.");
        Console.WriteLine(MainMenuOptions[2] + ") Evaluate tree.");
        Console.WriteLine(MainMenuOptions[3] + ") Quit.");
    }

    /// <summary>
    /// Prints the expression menu.
    /// </summary>
    public static void PrintExpressionMenu()
    {
        Console.WriteLine("Please enter an expression:");
    }

    /// <summary>
    /// Prints the variable menu.
    /// </summary>
    public static void PrintVariableMenu()
    {
        Console.WriteLine("Please enter a variable value:");
    }

    /// <summary>
    /// Prints the evaluate menu.
    /// </summary>
    public static void PrintEvaluateMenu()
    {
        Console.WriteLine("The result is:");
    }

    /// <summary>
    /// Prints the quit menu.
    /// </summary>
    public static void PrintQuitMenu()
    {
        Console.WriteLine("Have a good day.");
    }
}