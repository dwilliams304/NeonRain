using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionArea : MonoBehaviour
{
    PlayerStats _pStats;
    private int corruptionIncrease = 250;
    void OnTriggerEnter2D(Collider2D coll){
        _pStats = coll.GetComponent<PlayerStats>();
        _pStats.AddCorruption(corruptionIncrease);
        Debug.Log("Player corruption increased! New corruption level: " + _pStats.PlayerCorruptionLevel);
    }
}
