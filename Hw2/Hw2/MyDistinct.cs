using System.Collections.Generic;
using System.Linq;

namespace Hw2;

public class MyDistinct
{
    public static List<int> usingHashSet(List<int> list)
    {
        //TODO: implement MyDistinct.usingHashSet()
        var set = new HashSet<int>();
        foreach (var item in list)
        {
            set.Add(item);
        }
        return set.ToList();
    }

    public static List<int> o1Memory(List<int> list)
    {
        //TODO: implement MyDistinct.o1Memory()
        return list;
    }

    public static List<int> sortFirst(List<int> list)
    {
        //TODO: implement MyDistinct.sortFirst()
        return list;
    }
}