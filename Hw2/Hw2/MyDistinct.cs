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
        //TODO: implement MyDistinct.sortFirst()
        return list.Count();
    }
}