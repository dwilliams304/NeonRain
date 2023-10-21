using UnityEngine;

public static class Extensions 
{
    public static bool ChanceRoll(int chance){
        if(Random.Range(0, 101) <= chance) return true;
        return false;
    }
}
