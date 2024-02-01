using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hw2;

public class MyApplication
{
    public static string Run()
    {
        var rand = new Random();
        var list = new List<int>();
    
        // make list of 10000 from 0 to 20000
        for (int i = 0; i < 10000; i++)
        {
            list.Add(rand.Next(0, 20000));
        }

        var hashSetVal = MyDistinct.UsingHashSet(list);
        var o1MemoryVal = MyDistinct.O1Memory(list);
        var sortFirstVal = MyDistinct.SortFirst(list);

        var vals = "Vals: " + hashSetVal + " " + o1MemoryVal + " " + sortFirstVal;
        
        return vals;
    }
}