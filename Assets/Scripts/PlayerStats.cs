using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats playerStats;
    [Header("Movement")]
    public float BaseSpeed = 7f;
    public float MoveSpeed;
    public float DashSpeed = 20f;
    public float DashDuration = .2f;
    public float DashCoolDown = 1f;

    [Header("Health")]
    public int PlayerMaxHealth = 100;
    public int CurrentHealth;


    [Header("PlayerXP")]
    public int CurrentLevel = 1;
    public int CurrentPlayerXP = 0;
    public int MaxExperience;

    [Header("Corruption/Gold")]
    public int PlayerCorruptionLevel = 0;
    public int PlayerGold = 1;


    [Header("Base stats")]
    public int BaseCritChance = 10;
    public float BaseDamageDone = 1f;
    public float BaseDamageTaken = 1f;
    public float CritDamageMod = 3f;


    public enum CorruptionTier{
        Tier0,
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5
    }
    public int tier1BreakPoint = 100;
    public int tier2BreakPoint = 200;
    public int tier3BreakPoint = 300;
    public int tier4BreakPoint = 400;
    public int tier5BreakPoint = 500;

    public CorruptionTier tier;

    void Awake(){
        MoveSpeed = BaseSpeed;
        CurrentHealth = PlayerMaxHealth;
        playerStats = this;
    }


    public void ModifyMoveSpeed(float mod){
        MoveSpeed += mod;
    }

    public void AddCorruption(int amount){
        PlayerCorruptionLevel += amount;
        if(PlayerCorruptionLevel >= tier5BreakPoint){
            tier = CorruptionTier.Tier5;
        }
    }
}
