namespace HW3_Testing;

using HW3.Models;
using HW3.ViewModels;


public class Tests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void FibonacciTextReaderEmptyTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader();
        var str = fib.ReadToEnd();
        Assert.Equals(str, null);
    }
    
    public void FibonacciTextReaderZeroTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(0);
        var str = fib.ReadToEnd();
        Assert.True(str, "0\n");
    }

    [Test]
    public void FibonacciTextReaderFirstTen()
    {
        FibonacciTextReader fib = new FibonacciTextReader(10);
        var str = fib.ReadToEnd();
        var ans = "0\n1\n1\n3\n4\n5\n8\n13\n21\n34";
        Assert.Equals(str, ans);
    }
    
    public void FibonacciTextReaderHundredTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(100);
        for (int i = 0; i < 99; i++)
        {
            fib.ReadLine();
        }

        var str = fib.ReadLine();
        var fibonacci_hundred = "354224848179261915075";

        Assert.Equals(str, fibonacci_hundred);
    }

    [Test]
    public void FibonacciTextReaderOverflowTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(10000);
        try
        {
            checked
            {
                fib.ReadToEnd();
            }
        }
        catch (OverflowException e)
        {
            Assert.Pass();
            throw;
        }
        
        Assert.Fail();
    }
    
    
}