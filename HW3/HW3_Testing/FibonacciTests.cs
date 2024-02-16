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
        Assert.AreEqual(str, string.Empty);
    }
    
    [Test]
    public void FibonacciTextReaderOneTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(1);
        var str = fib.ReadToEnd();
        Assert.AreEqual(str, "0\n");
    }
    
    [Test]
    public void FibonacciTextReaderFirstTenTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(10);
        for (int i = 0; i < 9; i++)
        {
            fib.ReadLine();
        }

        var str = fib.ReadLine();
        var fibonacci_ten = "55\n";

        Assert.AreEqual(str, fibonacci_ten);
    }

    [Test]
    public void FibonacciTextReaderFirstTenStringTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(10);
        var str = fib.ReadToEnd();
        var ans = "0\n1\n1\n2\n4\n5\n8\n13\n21\n34\n";
        Assert.AreEqual(str, ans);
    }
    
    [Test]
    public void FibonacciTextReaderFiftyTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(50);
        for (int i = 0; i < 49; i++)
        {
            fib.ReadLine();
        }

        var str = fib.ReadLine();
        var fibonacci_fifty = "12586269025\n";

        Assert.AreEqual(str, fibonacci_fifty);
    }
    
    [Test]
    public void FibonacciTextReaderHundredTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(100);
        for (int i = 0; i < 99; i++)
        {
            fib.ReadLine();
        }

        var str = fib.ReadLine();
        var fibonacci_hundred = "354224848179261915075\n";

        Assert.AreEqual(str, fibonacci_hundred);
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