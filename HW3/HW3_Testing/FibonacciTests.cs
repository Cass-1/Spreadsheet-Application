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

    [Test]
    public void FibonacciTextReaderBasicTest()
    {
        FibonacciTextReader fib = new FibonacciTextReader(10);
        var str = fib.ReadToEnd();
        var ans = "0\n1\n1\n3\n4\n5\n8\n13\n21\n34";
        Assert.Equals(str, ans);
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