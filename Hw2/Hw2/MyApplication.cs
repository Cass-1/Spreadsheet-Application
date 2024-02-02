using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hw2;

public class MyApplication
{
    /// <summary>
    /// The method called by avalonia to perform tasks 1, 2, and 3
    /// </summary>
    /// <returns>a string with answers to the questions for tasks 1, 2, and 3</returns>
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

        String hashSetString = "1. HashSet method: " + hashSetVal + " unique numbers\n" +
                               "The time complexity of this method is O(n^2). " +
                               "This is due to the fact that it requires: \n\n" +
                               "> Constant time to create a HashSet\n\n" +
                               "> O(n) time to loop through every item in the array. This will be multiplied by the" +
                               " O(n) time that it takes to" +
                               "to add an item to a HashSet.\n" +
                               "Adding an item to a HashSet is O(n) time because in the worst case the " +
                               "HashSet will need to resize and that takes O(n) time (according to the MSDN).\n" +
                               "Thus, adding an item to a hash set " +
                               "has a worst case time complexity of O(n).\n" +
                               "Thus the total time complexity " +
                               "to add all the items from the given list to the hashset is O(n^2). \n\n" +
                               "> O(n) time to call the toList() method on the hash set. \n\n" +
                               "Thus the complexity can be represented as T(n) = O(1) + O(n)*O(n) + O(n)\n" +
                               "Meaning that the worst case time complexity is O(n^2)\n\n\n";

        String o1MemoryString = "2. O(1) storage method: " + o1MemoryVal + " unique numbers\n\n\n";
        
        String sortFirstString = "3. Sorted method: " + sortFirstVal + " unique numbers\n\n\n";
        
        return hashSetString + o1MemoryString + sortFirstString;
    }
}