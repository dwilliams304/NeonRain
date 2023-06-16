using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float baseSpeed = 7f;
    private float _currentMoveSpeed;
    public float MoveSpeed => _currentMoveSpeed;

    private int _playerMaxHealth = 100;
    private int _currentHealth;
    public int PlayerMaxHealth => _playerMaxHealth;
    public int CurrentHealth => _currentHealth;

    
    private int _currentLevel = 1;
    private int _currentPlayerXP = 0;
    public int CurrentLevel => _currentLevel;  
    public int CurrentPlayerXP => _currentPlayerXP;


    private int _playerCorruptionLevel = 0;
    public int PlayerCorruptionLevel => _playerCorruptionLevel;

    private int _playerGold = 1;
    public int PlayerGold => _playerGold;

    void Start(){
        _currentMoveSpeed = baseSpeed;
        _currentHealth = _playerMaxHealth;
    }


    public void ModifyMoveSpeed(float mod){
        _currentMoveSpeed += mod;
    }
}
