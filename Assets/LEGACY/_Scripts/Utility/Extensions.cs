using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    /// <summary>
    /// Rolls from 0 - 100, returning true or false whether or not the roll
    /// was successful.
    /// </summary>
    /// <param name="chance"></param>
    /// <returns></returns>
    public static bool Roll100(int chance){
        if(Random.Range(0, 101) <= chance) return true;
        return false;
    }

    /// <summary>
    /// Will grab and return a random element from any given list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">List to grab random item from</param>
    /// <returns></returns>
    public static T RandomFromList<T>(this IList<T> list){
        return list[Random.Range(0, list.Count)];
    }
}
