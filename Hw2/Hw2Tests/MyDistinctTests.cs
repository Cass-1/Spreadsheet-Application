using DynamicData;
using Hw2;

namespace Hw2Tests;
public class MyDistinctTests
{
    [SetUp]
    public void Setup()
    {
    }

    // MyDistinct Tests ---------------------------------------------------------------------------------------------    
    
    // usingHashSet
    [Test]
    public void MyDistinctUsingHashSetEmptyTest()
    {
        List<int> list = new List<int>();

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();

        int len = distinctList.Count();
        
        Assert.AreEqual(len, MyDistinct.usingHashSet(list));
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

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.AreEqual(distinctList.Count(), MyDistinct.usingHashSet(list));
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

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();

        Assert.AreEqual(distinctList.Count(), MyDistinct.usingHashSet(list));
    }
    
    // o1Memory
    [Test]
    public void MyDistinctO1MemoryEmptyTest()
    {
        var list = new List<int>();

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.AreEqual(distinctList.Count(), MyDistinct.o1Memory(list));
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

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        
        Assert.AreEqual(distinctList.Count(), MyDistinct.o1Memory(list));
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

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.AreEqual(distinctList.Count(), MyDistinct.o1Memory(list));
    }
    
    // sortFirst
    [Test]
    public void MyDistinctSortFirstEmptyTest()
    {
        var list = new List<int>();

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        
        Assert.AreEqual(distinctList.Count(), MyDistinct.sortFirst(list));
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

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.AreEqual(distinctList.Count(),MyDistinct.sortFirst(list));
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

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        // my implementation
        var myDistinctList = MyDistinct.sortFirst(list);
        
        Assert.AreEqual(distinctList.Count(),MyDistinct.sortFirst(list));
    }
}