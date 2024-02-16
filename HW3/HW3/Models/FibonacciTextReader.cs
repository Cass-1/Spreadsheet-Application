using System.IO;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using HarfBuzzSharp;

namespace HW3.Models;

/// <summary>
/// calculates fibonacci sequence
/// </summary>
public class FibonacciTextReader : TextReader
{
    
    // the maximum number of lines available
    public int MaxLines { get; set; }

    // the current position in the sequence
    private int currentPosition;
    
    // the previous fibonacci number
    private BigInteger previousNumber;
    
    // the current fibonacci number
    private BigInteger currentNumber;
    
    // constructor
    public FibonacciTextReader(int maxLines = 0)
    {
        MaxLines = maxLines;
        currentPosition = 0;
    }

    /// <summary>
    /// returns the next value in the fibonacci sequence
    /// </summary>
    /// <returns>a string of the next value in the fibonacci sequence</returns>
    public override string? ReadLine()
    {
        
        // check if done
        if (currentPosition == MaxLines)
        {
            return null;
        }
        
        // spcial cases
        if (currentPosition == 0)
        {
            previousNumber = 0;
            currentNumber = 0;
        }
        else if (currentPosition == 1)
        {
            previousNumber = 0;
            currentNumber = 1;
        }
        else
        {
            // store the current number
            BigInteger tempCurrentNumber = currentNumber;
            
            // update the current number
            currentNumber = currentNumber + previousNumber;
            
            // set the previous number
            previousNumber = tempCurrentNumber;

        }
        
        // increment position
        currentPosition++;
        
        // return a string version of teh currentNumber
        var str = new StringBuilder("" + currentNumber + "\n");
        return str.ToString();
    }

    /// <summary>
    /// creates a string of the fibonacci sequence up to the maxline
    /// </summary>
    /// <returns>a string of the fibonacci sequence</returns>
    public override string ReadToEnd()
    {
        // will store the fibonacci sequence
        StringBuilder sequence = new StringBuilder("");
        
        // read all the fibonacci numbers up to the MaxLines
        string number = this.ReadLine();
        while (number != null)
        {
            sequence.Append(number);
            number = this.ReadLine();
        }

        return sequence.ToString();
    }
}