using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    public static bool Roll100(int chance){
        if(Random.Range(0, 101) <= chance) return true;
        return false;
    }

    public static T RandomFromList<T>(this IList<T> list){
        return list[Random.Range(0, list.Count)];
    }
}
