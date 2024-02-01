using System.Net.Mime;

namespace ApplicationTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MyDistinctTest1()
    {
        var rand = new Random();
        var list = new List<int>();
        
        // generate list with 10,000 random integers in the range [0, 20,000]
        for (int i = 0; i < 10000; i++)
        {
            list.Add(rand.Next(0, 20000));
        }
        
        
        
    }
}