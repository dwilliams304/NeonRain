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
    }
}
