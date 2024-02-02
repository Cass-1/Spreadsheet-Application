using Hw2;
// <copyright file="MyDistinctTests.cs" company="Cass Dahle">
// "GNU Public Licence v3"
// </copyright>
namespace Hw2Tests;
public class MyDistinctTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    /// <summary>
    /// tests the UsingHashSet method with an empty list
    /// </summary>
    [Test]
    public void MyDistinctUsingHashSetEmptyTest()
    {
        List<int> list = new List<int>();

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();

        int len = distinctList.Count();
        
        Assert.That(MyDistinct.UsingHashSet(list), Is.EqualTo(len));
    }
    
    /// <summary>
    /// tests the UsingHashSet method with a simple list
    /// </summary>
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
        
        Assert.That(MyDistinct.UsingHashSet(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the UsingHashSet method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctUsingHashSetEdgeTest1()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(3);
        list.Add(8);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.UsingHashSet(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the UsingHashSet method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctUsingHashSetEdgeTest2()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.UsingHashSet(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the UsingHashSet method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctUsingHashSetEdgeTest3()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(8);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.UsingHashSet(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the UsingHashSet method with the actual assignment specifications
    /// </summary>
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

        Assert.That(MyDistinct.UsingHashSet(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the ConstantMemory method with an empty list
    /// </summary>
    // o1Memory
    [Test]
    public void MyDistinctConstantMemoryEmptyTest()
    {
        var list = new List<int>();

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.ConstantMemory(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the ConstantMemory method with a simple list
    /// </summary>
    [Test]
    public void MyDistinctConstantMemorySimpleTest()
    {
        var list = new List<int>();
        
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        
        Assert.That(MyDistinct.ConstantMemory(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the ConstantMemory method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctConstantMemoryEdgeTest1()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(3);
        list.Add(8);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.ConstantMemory(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the ConstantMemory method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctConstantMemoryEdgeTest2()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.ConstantMemory(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the ConstantMemory method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctConstantMemoryEdgeTest3()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(8);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.ConstantMemory(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the ConstantMemory method with the actual assignment specifications
    /// </summary>
    [Test]
    public void MyDistinctConstantMemoryAssignmentTest()
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
        
        Assert.That(MyDistinct.ConstantMemory(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the SortFirst method with an empty list
    /// </summary>
    // sortFirst
    [Test]
    public void MyDistinctSortFirstEmptyTest()
    {
        var list = new List<int>();

        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        
        Assert.That(MyDistinct.SortFirst(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the SortFirst method with a simple list
    /// </summary>
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
        
        Assert.That(MyDistinct.SortFirst(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the SortFirst method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctSortFirstEdgeTest1()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(3);
        list.Add(8);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.SortFirst(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the SortFirst method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctSortFirstEdgeTest2()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.SortFirst(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the SortFirst method with an edge case
    /// </summary>
    [Test]
    public void MyDistinctSortFirstEdgeTest3()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(1);
        list.Add(8);
        list.Add(8);
        
        // the correct distinct list
        IEnumerable<int> distinctList = list.Distinct();
        
        Assert.That(MyDistinct.SortFirst(list), Is.EqualTo(distinctList.Count()));
    }
    
    /// <summary>
    /// tests the SortFirst method with the actual assignment specifications
    /// </summary>
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
        
        Assert.That(MyDistinct.SortFirst(list), Is.EqualTo(distinctList.Count()));
    }
}