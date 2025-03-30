// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace ExpressionTreeConsole;

/// <summary>
/// The program entry point.
/// </summary>
public class Program
{
    /// <summary>
    /// The main program.
    /// </summary>
    /// <param name="args">Arguments passed into main.</param>
    /// <returns>The exit code.</returns>
    public static int Main(string[] args)
    {
        Application application = new Application();

        application.RunApp();
        return 0;
    }
}
