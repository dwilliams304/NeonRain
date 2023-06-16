using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : MonoBehaviour
{
    public string abilityName;
    public float abilityCooldown;
    public bool isReady;

    public abstract void Activate();
    public abstract void Enable();
    public abstract void Disable();
}
