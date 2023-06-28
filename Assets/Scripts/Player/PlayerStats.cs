using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats playerStats;
    public delegate void HandleLevelIncrease();
    public delegate void HandlePlayerDeath();
    public static HandleLevelIncrease handleLevelIncrease;
    public static HandlePlayerDeath onPlayerDeath;
    [Header("Movement")]
    public float BaseSpeed = 7f;
    public float MoveSpeed;
    public float DashSpeed = 20f;
    public float DashDuration = .2f;
    public float DashCoolDown = 1f;

    [Header("Health")]
    public float PlayerMaxHealth = 100;
    public float CurrentHealth;


    [Header("PlayerXP")]
    public int CurrentLevel = 1;
    public int CurrentPlayerXP = 0;
    public int ExperienceToNextLevel = 100;
    public float XPModifier = 1f;
    [SerializeField] private AnimationCurve xpScaling;

    [Header("Corruption/Gold")]
    public int PlayerCorruptionLevel = 0;
    public float PlayerGold = 0;
    public float AdditionalGoldMod = 1f;


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

    void OnEnable(){
        XPManager.Instance.onXPChange += IncrementExperience;
    }

    void Start(){
        UIManager.uiManagement.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }

    public void ModifyMoveSpeed(float mod){
        MoveSpeed += mod;
    }

    public void AddCorruption(int amount){
        PlayerCorruptionLevel += amount;
    }

    public void AddGold(int amount){
        PlayerGold += Mathf.Ceil(amount * AdditionalGoldMod);
        UIManager.uiManagement.UpdateGoldUI(PlayerGold);
    }

    public void IncrementExperience(int xpAmnt){
        CurrentPlayerXP += Mathf.CeilToInt(xpAmnt * XPModifier);
        if(CurrentPlayerXP >= ExperienceToNextLevel){
            int overflow = CurrentPlayerXP - ExperienceToNextLevel;
            IncreaseLevel(overflow);
        }
        UIManager.uiManagement.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }

    void IncreaseLevel(int xpOverflow){
        PlayerMaxHealth += 25;
        CurrentHealth = PlayerMaxHealth;
        BaseDamageDone += 0.02f;
        BaseCritChance += 1;
        CurrentLevel++;
        CurrentPlayerXP = xpOverflow;
        ExperienceToNextLevel = Mathf.RoundToInt(xpScaling.Evaluate(CurrentLevel));
        UIManager.uiManagement.UpdateHealthBar();
        handleLevelIncrease?.Invoke();
    }

    public void TakeDamage(float damage){
        CurrentHealth -= Mathf.Ceil(damage * BaseDamageTaken);
        if(CurrentHealth <= 0){
            PlayerDied();
        }
        UIManager.uiManagement.UpdateHealthBar();
    }


    void PlayerDied(){
        onPlayerDeath?.Invoke();
    }
}
