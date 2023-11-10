using UnityEngine;


public class AugmentStation : MonoBehaviour, IInteractable
{
    public delegate void OnAugmentStationInteracted();
    public static OnAugmentStationInteracted onAugmentStationInteracted;
    public static AnimationCurve GoldCostScaler;
    [SerializeField] private AnimationCurve _goldCostScaler;

    void Awake() => GoldCostScaler = _goldCostScaler;
    
    public void Interacted()
    {
        Debug.Log("Augment Station interacted!");
    }


    
}
