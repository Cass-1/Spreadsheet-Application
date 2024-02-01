using System.Collections.Generic;
using System.Linq;

namespace Hw2;

public class MyDistinct
{
    
    // find the distinct elements in a list using a HashSet
    public static int usingHashSet(List<int> list)
    {
        //TODO: check MyDistinct.usingHashSet()
        var set = new HashSet<int>();
        foreach (var item in list)
        {
            set.Add(item);
        }
        return set.ToList().Count;
    }

    // find the distinct elements in a list with storage complexity O(1)
    public static int o1Memory(List<int> list)
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

    public static int sortFirst(List<int> list)
    {
        //TODO: check MyDistinct.sortFirst()
        int distinctElements = 0;
        int i = 0;

        list.Sort();
        
        // EDGE CASES
        
        // check if list is empty
        if (list.Count == 0)
        {
            return 0;
        }
        // check if list is all the same number
        if (list.ElementAt(0) == list.ElementAt(list.Count - 1))
        {
            return 1;
        }
        
        // COMMON CASE
        
        // check first element
        if (i + 1 < list.Count && list.ElementAt(i) != list.ElementAt(i + 1))
        {
            distinctElements++;
        }

        i++;
        
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