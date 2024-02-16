// Copyright (c) Cass Dahle. Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace HW3.Models;

using System.IO;
using System.Numerics;
using System.Text;

/// <summary>
/// calculates fibonacci sequence.
/// </summary>
public class FibonacciTextReader : TextReader
{
    // the current position in the sequence
    private int currentPosition;

    // the previous fibonacci number
    private BigInteger previousNumber;

    // the current fibonacci number
    private BigInteger currentNumber;

    // the maximum number of lines available
    public int MaxLines { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
    /// </summary>
    /// <param name="maxLines">the maximum number of lines for printing.</param>
    public FibonacciTextReader(int maxLines = 0)
    {
        this.MaxLines = maxLines;
        this.currentPosition = 0;
    }

    /// <summary>
    /// returns the next value in the fibonacci sequence.
    /// </summary>
    /// <returns>a string of the next value in the fibonacci sequence.</returns>
    public override string? ReadLine()
    {
        // check if done
        if (this.currentPosition == this.MaxLines)
        {
            return null;
        }

        // spcial cases
        if (this.currentPosition == 0)
        {
            this.previousNumber = 0;
            this.currentNumber = 0;
        }
        else if (this.currentPosition == 1)
        {
            this.previousNumber = 0;
            this.currentNumber = 1;
        }
        else
        {
            // store the current number
            BigInteger tempCurrentNumber = this.currentNumber;

            // update the current number
            this.currentNumber = this.currentNumber + this.previousNumber;

            // set the previous number
            this.previousNumber = tempCurrentNumber;
        }

        // increment position
        this.currentPosition++;

        // return a string version of teh currentNumber
        var str = new StringBuilder(string.Empty + this.currentNumber + "\n");
        return str.ToString();
    }

    /// <summary>
    /// creates a string of the fibonacci sequence up to the maxline.
    /// </summary>
    /// <returns>a string of the fibonacci sequence.</returns>
    public override string ReadToEnd()
    {
        // will store the fibonacci sequence
        StringBuilder sequence = new StringBuilder(string.Empty);

        // read all the fibonacci numbers up to the MaxLines
        string? number = this.ReadLine();
        while (number != null)
        {
            sequence.Append(number);
            number = this.ReadLine();
        }

        return sequence.ToString();
    }
}
