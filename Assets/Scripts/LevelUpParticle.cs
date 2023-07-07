using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpParticle : MonoBehaviour
{
    ParticleSystem p;

    void Start(){
        p = GetComponent<ParticleSystem>();
    }


    void OnEnable(){
        PlayerStats.handleLevelIncrease += Show;
    }
    void OnDisable(){
        PlayerStats.handleLevelIncrease -= Show;
    }

    void Show(){
        // p.Emit(3);
        p.Play();
    }
}
