using UnityEngine;


public class PlayerStatModifier : MonoBehaviour
{
    public static PlayerStatModifier playerMods;

    //DAMAGE DONE/TAKEN
    public static float MOD_DamageDone  {get; private set; } = 1f;
    public static float MOD_DamageTaken  {get; private set; } = 1f;
    
    //CRIT STRIKES
    public static int MOD_CritChance {get; private set; } = 10;
    public static float MOD_CritDamage  {get; private set; } = 3f;

    //MOVE SPEED
    public static float MOD_MoveSpeed  {get; private set; } = 1f;

    //MISC.
    public static float MOD_AdditonalXP {get; private set;} = 1f;
    public static float MOD_AdditionalGold { get; private set; } = 1f;

    public static void ChangeClassMods(ClassData classChosen){
        MOD_MoveSpeed = classChosen.MoveSpeed;
        MOD_DamageDone = classChosen.DamageDone;
        MOD_DamageTaken = classChosen.DamageTaken;
        MOD_CritChance = classChosen.CritChance;
        MOD_CritDamage = classChosen.CritMultiplier;
        MOD_AdditionalGold = classChosen.GoldMod;

    }


    public static void ChangeMoveSpeedMod(float amount){
        MOD_MoveSpeed += amount;
    }

    public static void ChangeDamageTakenMod(float amount){
        MOD_DamageTaken += amount;
    }

    public static void ChangeDamageDoneMod(float amount){
        MOD_DamageDone += amount;
    }

    public static void ChangeCritChanceMod(int amount){
        MOD_CritChance += amount;
    }

    public static void ChangeCritDamageMod(float amount){
        MOD_CritDamage += amount;
    }

    public static void ChangeAdditionalGoldMod(float amount){
        MOD_AdditionalGold += amount;
    }


}
