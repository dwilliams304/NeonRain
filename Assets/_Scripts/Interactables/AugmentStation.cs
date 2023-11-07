using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentStation : MonoBehaviour, IInteractable
{
    public delegate void OnAugmentStationInteracted();
    public static OnAugmentStationInteracted onAugmentStationInteracted;
    [SerializeField] private AnimationCurve _goldCostScaler;

    public List<PlayerAugment> augments = new List<PlayerAugment>();

    public void Interacted()
    {
        Debug.Log("Augment Station interacted!");
    }

    [System.Serializable]
    public class PlayerAugment{
        public string AugmentName;
        public string AugmentDescription;
        public bool AugmentUnlocked;
        public int RequiredForNextTier;
        public int AmountAlreadyPurchased;
        public int AugmentCost;
        public PlayerAugment NextTier;
    }

    
}
