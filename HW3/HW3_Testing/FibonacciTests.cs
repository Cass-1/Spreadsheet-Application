namespace HW3_Testing;

using HW3.Models;

public class Tests
{
    [SetUp]
    public void Setup()
    {

    }

    /// <summary>
    /// Tests when the FibonacciTextReader is instantiated with no inputs.
    /// </summary>
    [Test]
    public void FibonacciTextReaderEmptyTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader();
        var str = fib.ReadToEnd();
        Assert.That(str, Is.EqualTo(string.Empty));
    }
    
    /// <summary>
    /// Tests the first value of the fibonacci sequence.
    /// </summary>
    [Test]
    public void FibonacciTextReaderOneTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(1);
        var str = fib.ReadToEnd();
        Assert.That(str, Is.EqualTo("0\n"));
    }
    
    /// <summary>
    /// Tests the tenth value of the fibonacci sequence.
    /// </summary>
    [Test]
    public void FibonacciTextReaderFirstTenTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(10);
        for (int i = 0; i < 9; i++)
        {
            fib.ReadLine();
        }

        var str = fib.ReadLine();
        var fibonacciTen = "34\n";

        Assert.That(fibonacciTen, Is.EqualTo(str));
    }

    /// <summary>
    /// Tests the first 10 values in the fibonacci sequence
    /// </summary>
    [Test]
    public void FibonacciTextReaderFirstTenStringTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(10);
        var str = fib.ReadToEnd();
        var ans = "0\n1\n1\n2\n3\n5\n8\n13\n21\n34\n";
        Assert.That(ans, Is.EqualTo(str));
    }
    
    /// <summary>
    /// Tests the 50th value in the fibonacci sequence.
    /// </summary>
    [Test]
    public void FibonacciTextReaderFiftyTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(50);
        for (int i = 0; i < 49; i++)
        {
            fib.ReadLine();
        }

        var str = fib.ReadLine();
        var fibonacciFifty = "7778742049\n";

        Assert.That(fibonacciFifty, Is.EqualTo(str));
    }
    
    /// <summary>
    /// Tests the 100th value of the fibonacci sequence
    /// </summary>
    [Test]
    public void FibonacciTextReaderHundredTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(100);
        for (int i = 0; i < 99; i++)
        {
            fib.ReadLine();
        }

        var str = fib.ReadLine();
        var fibonacciHundred = "218922995834555169026\n";

        Assert.That(fibonacciHundred, Is.EqualTo(str));
    }
}