namespace Hw2Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MyDistinctUsingHashSetBasicTest()
    {
        var rand = new Random();
        var list = new List<int>();
    
        // make list of 10000 from 0 to 20000
        for (int i = 0; i < 10000; i++)
        {
            list.Add(rand.Next(0, 20000));
        }

        // the correct answer to compare my implementation to
        IEnumerable<int> distinctList = list.Distinct();
        
        // my implementation
        var myDistinctList = MyDistinct.usingHashSet(list);
        
        Assert.AreEqual(distinctList, myDistinctList);
    }
    
    [Test]
    public void MyDistinctO1MemoryBasicTest()
    {
        var rand = new Random();
        var list = new List<int>();
    
        // make list of 10000 from 0 to 20000
        for (int i = 0; i < 10000; i++)
        {
            list.Add(rand.Next(0, 20000));
        }

        // the correct answer to compare my implementation to
        IEnumerable<int> distinctList = list.Distinct();
        
        // my implementation
        var myDistinctList = MyDistinct.o1Memory(list);
        
        Assert.AreEqual(distinctList, myDistinctList);
    }
    
    [Test]
    public void MyDistinctSortFirstBasicTest()
    {
        var rand = new Random();
        var list = new List<int>();
    
        // make list of 10000 from 0 to 20000
        for (int i = 0; i < 10000; i++)
        {
            list.Add(rand.Next(0, 20000));
        }

        // the correct answer to compare my implementation to
        IEnumerable<int> distinctList = list.Distinct();
        
        // my implementation
        var myDistinctList = MyDistinct.sortFirst(list);
        
        Assert.AreEqual(distinctList, myDistinctList);
    }
}