using DynamicData;
using Hw2;

namespace Hw2Tests;
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MyDistinctUsingHashSetSimpleTest()
    {
        var list = new List<int>();
    
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);

        // the correct answer to compare my implementation to
        IEnumerable<int> distinctList = list.Distinct();
        
        // my implementation
        var myDistinctList = MyDistinct.usingHashSet(list);
        
        Assert.IsTrue(distinctList.SequenceEqual(myDistinctList));
    }
    [Test]
    public void MyDistinctUsingHashSetAssignmentTest()
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
        
        Assert.IsTrue(distinctList.SequenceEqual(myDistinctList));
    }
    
    [Test]
    public void MyDistinctO1MemorySimpleTest()
    {
        var list = new List<int>();
        
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);

        // the correct answer to compare my implementation to
        IEnumerable<int> distinctList = list.Distinct();
        
        // my implementation
        var myDistinctList = MyDistinct.o1Memory(list);
        
        Assert.IsTrue(distinctList.SequenceEqual(myDistinctList));
    }
    
    [Test]
    public void MyDistinctO1MemoryAssignmentTest()
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
        
        Assert.IsTrue(distinctList.SequenceEqual(myDistinctList));
    }
    
    [Test]
    public void MyDistinctSortFirstSimpleTest()
    {
        var list = new List<int>();
    
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);

        // the correct answer to compare my implementation to
        IEnumerable<int> distinctList = list.Distinct();
        
        // my implementation
        var myDistinctList = MyDistinct.sortFirst(list);
        
        Assert.IsTrue(distinctList.SequenceEqual(myDistinctList));
    }
    
    [Test]
    public void MyDistinctSortFirstAssignmentTest()
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
        
        Assert.IsTrue(distinctList.SequenceEqual(myDistinctList));
    }
}