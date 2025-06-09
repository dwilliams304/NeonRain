/*
    This is actually not an original thing that I myself created, this came from a channel on YouTube: git-amend
    Link: https://www.youtube.com/@git-amend
*/

using System.Collections.Generic;
using UnityEngine;

public static class WaitForSecondsExtensions
{
    private static Dictionary<float, WaitForSeconds> dictionary = new Dictionary<float, WaitForSeconds>();

    /// <summary>
    /// Get or create a cached WaitForSeconds
    /// </summary>
    /// <param name="speed">The value of WaitForSeconds we need</param>
    /// <returns></returns>
    public static WaitForSeconds GetWaitForSeconds(float speed)
    {
        if (dictionary.TryGetValue(speed, out WaitForSeconds wfs)) return wfs;
        else
        {
            WaitForSeconds _wfs = new WaitForSeconds(speed);
            dictionary.Add(speed, _wfs);
            return _wfs;
        }
    }
}