using System.IO;
using HarfBuzzSharp;

namespace HW3.Models;

public class FibonacciTextReader : TextReader
{
    
    // the maximum number of lines available
    public int MaxLines { get; set; }

    // the current position in the sequence
    private int currentFibonacciNumber;

    // constructor
    public FibonacciTextReader(int maxLines = 0)
    {
        MaxLines = maxLines;
        currentFibonacciNumber = 0;
    }

    public override string? ReadLine()
    {
        return base.ReadLine();
    }

    public override string ReadToEnd()
    {
        return base.ReadToEnd();
    }
}