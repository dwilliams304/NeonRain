using UnityEngine;


public class PlayerStatModifier : MonoBehaviour
{
    public static PlayerStatModifier Instance;
    public delegate void OnStatChange();
    public static OnStatChange onStatChange;

    //DAMAGE DONE/TAKEN
    private float _damageDone = 1f;
    private float _damageTaken = 1f;
    public float Corruption_DamageDone = 0;
    public float Corruption_DamageTaken = 0;
    
    //CRIT STRIKES
    private int _critChance = 0;
    private float _critDamage = 3f;
    public int Corruption_CritChance = 0;
    public float Corruption_CritDamage = 0;

    //MOVE SPEED
    private float _moveSpeed = 1f;
    public float Corruption_MoveSpeed = 0;

    //MISC.
    private float _fireRateMod = 1f;
    private float _additonalXP = 1f;
    private float _additionalGold = 1f;
    public float Corruption_AdditionalGold = 0;
    public float Corruption_AdditionalXP = 0;
    
    //Returns
    public float MOD_DamageDone => _damageDone + Corruption_DamageDone;
    public float MOD_DamageTaken => _damageTaken + Corruption_DamageTaken;
    public int MOD_CritChance => _critChance + Corruption_CritChance;
    public float MOD_CritDamage => _critDamage + Corruption_CritDamage;
    public float MOD_FireRate => _fireRateMod;
    public float MOD_MoveSpeed => _moveSpeed + Corruption_MoveSpeed;
    public float MOD_AdditonalXP => _additonalXP + Corruption_AdditionalXP;
    public float MOD_AdditionalGold => _additionalGold + Corruption_AdditionalGold;


    void Awake(){
        Instance = this;
    }
    public void ChangeClassMods(ClassData classChosen){
        _moveSpeed = classChosen.MoveSpeed;
        _damageDone = classChosen.DamageDone;
        _damageTaken = classChosen.DamageTaken;
        _critChance = classChosen.CritChance;
        _critDamage = classChosen.CritMultiplier;
        _additionalGold = classChosen.GoldMod;

    }

    public void CorruptionAmountsSwitched(){
        onStatChange?.Invoke();
    }


    public void ChangeMoveSpeedMod(float amount){
        _moveSpeed += amount;
        onStatChange?.Invoke();
    }

    public void ChangeDamageTakenMod(float amount){
        _damageTaken += amount;
        onStatChange?.Invoke();
    }

    public void ChangeDamageDoneMod(float amount){
        _damageDone += amount;
        onStatChange?.Invoke();
    }

    public void ChangeCritChanceMod(int amount){
        _critChance += amount;
        onStatChange?.Invoke();
    }

    public void ChangeCritDamageMod(float amount){
        _critDamage += amount;
        onStatChange?.Invoke();
    }

    public void ChangeFireRateMod(float amount){
        _fireRateMod += amount;
        onStatChange?.Invoke();
    }

    public void ChangeAdditionalGoldMod(float amount){
        _additionalGold += amount;
        onStatChange?.Invoke();
    }

    public void ChangeHealthRegenAmount(float amount){
        PlayerStats.Instance.IncreaseHealthRegenAmount(amount);
        onStatChange?.Invoke();
    }
    public void ChangeHealthRegenTime(float amount){
        PlayerStats.Instance.DecreaseHealthRegenTime(amount);
        onStatChange?.Invoke();
    }


}
