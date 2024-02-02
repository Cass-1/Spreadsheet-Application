using System.Collections.Generic;
using System.Linq;

namespace Hw2;

public class MyDistinct
{
    
    /// <summary>
    /// Find the number of distinct elements in a list using a HashSet
    /// </summary>
    /// <param name="list">a list of integers</param>
    /// <returns>the number of distinct elements in the input list</returns>
    public static int UsingHashSet(List<int> list)
    {
        //TODO: check MyDistinct.usingHashSet()
        var set = new HashSet<int>();
        foreach (var item in list)
        {
            set.Add(item);
        }
        return set.ToList().Count;
    }
    
    /// <summary>
    /// find the number distinct elements in a list with storage complexity O(1)
    /// </summary>
    /// <param name="list">a list of integers</param>
    /// <returns>the number of distinct elements in the input list</returns>
    public static int O1Memory(List<int> list)
    {
        //TODO: check MyDistinct.o1Memory()
        
        int distinctElements = 0;
        bool isUnique;
        
        // for loop to loop through all the numbers in list in O(n^2) time
        for (int i = 0; i < list.Count; i++)
        {
            isUnique = true;
            int j = i;
            
            // check if any elements are the same as i in the rest of the list
            for(; j < list.Count; j++)
            {
                if (i !=j && list.ElementAt(i) == list.ElementAt(j))
                {
                    isUnique = false;
                    break;
                }
            }
            
            // if there are no elements the same as i in the rest of the list update the counter
            if (isUnique)
            {
                distinctElements++;
            }
        }

        return distinctElements;
    }
    
    // 
    /// <summary>
    /// sorts the prameter list and then finds the number of distinct elements in O(n) time and with O(1) storage
    /// complexity
    /// </summary>
    /// <param name="list">a list of integers</param>
    /// <returns>the number of distinct elements in the input list</returns>
    public static int SortFirst(List<int> list)
    {
        int distinctElements = 1;

        list.Sort();
        
        // EDGE CASES
        
        // check if list is empty
        if (list.Count == 0)
        {
            return 0;
        }

        
        // COMMON CASE   1   1    3   4
        int i = 1;
        // check all the elements in between
        for (; i < list.Count; i++)
        {
            if (list.ElementAt(i - 1) != list.ElementAt(i))
            {
                distinctElements++;
            }
        }
        
        return distinctElements;
    }
}