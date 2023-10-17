using UnityEngine;


public class PlayerStatModifier : MonoBehaviour
{
    public static PlayerStatModifier playerMods;

    //MOVE SPEED
    public float MOD_MoveSpeed => _moveSpeed;
    private float _moveSpeed = 1f;

    //DAMAGE DONE/TAKEN
    public float MOD_DamageDone => _damageDone;
    public float MOD_DamageTaken => _damageTaken;
    private float _damageDone = 1f;
    private float _damageTaken = 1f;
    
    //CRIT STRIKES
    public float MOD_CritChance => _critChance;
    public float MOD_CritDamage => _critDamage;
    private float _critChance = 10f;
    private float _critDamage = 3f;

    //MISC.
    public float MOD_AdditionGold => _additionalGold;
    private float _additionalGold = 1f;



    void Awake(){
        playerMods = this;
    }
}
