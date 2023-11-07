using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance;

    public delegate void OnXPChange(int curAmount, int amountToNextLevel);
    public delegate void AddXP(int amount);
    public delegate void OnLevelChange(int level);
    public static OnXPChange onXPChange;
    public static AddXP addXP;
    public static OnLevelChange onLevelChange;

    public int CurrentXP {get; private set;} = 0;
    public int XPToNextLevel {get; private set;} = 100;
    public int CurrentLevel {get; private set;} = 1;

    [SerializeField] AnimationCurve XPScaler;

    public float XPMultiplier {get; private set;} = 1f;

    void Awake(){
        Instance = this;
    }

    void Start(){
        XPToNextLevel = Mathf.RoundToInt(XPScaler.Evaluate(CurrentLevel));
        onXPChange?.Invoke(CurrentXP, XPToNextLevel);
    }
    public void AddExperience(int amount){
        int newXPAmnt = Mathf.RoundToInt(amount * XPMultiplier);
        GameStats.xpEarned += newXPAmnt;
        CurrentXP += newXPAmnt;
        addXP?.Invoke(newXPAmnt);
        if(CurrentXP >= XPToNextLevel){
            int overflow = CurrentXP - XPToNextLevel;
            if(CurrentLevel < 75) {
                IncreaseLevel(overflow);
            }
        }
        onXPChange?.Invoke(CurrentXP, XPToNextLevel);
    }

    public void ForceIncreaseLevel() => IncreaseLevel(0);

    void IncreaseLevel(int overflow){
        CurrentLevel++;
        CurrentXP = overflow;
        XPToNextLevel = Mathf.RoundToInt(XPScaler.Evaluate(CurrentLevel));
        onLevelChange?.Invoke(CurrentLevel);
    }
}
