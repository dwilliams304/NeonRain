using System.Collections;
using UnityEngine;
using TMPro;

public class FinalGameStatsUI : MonoBehaviour
{
    [SerializeField] TMP_Text _enemiesSlain;
    [SerializeField] TMP_Text _damageDone;
    [SerializeField] TMP_Text _damageTaken;
    [SerializeField] TMP_Text _abilitiesUsed;
    [SerializeField] TMP_Text _xpGained;
    [SerializeField] TMP_Text _goldGained;
    [SerializeField] TMP_Text _goldSpent;


    [SerializeField] float timeStep = 0.01f;


    void OnEnable(){
        Combat.onPlayerDeath += StartStatsDisplay;
    }

    void OnDisable(){
        Combat.onPlayerDeath -= StartStatsDisplay;
    }

    void StartStatsDisplay(){
        _enemiesSlain.text = "0";
        _damageDone.text = "0";
        _damageTaken.text = "0";
        _abilitiesUsed.text = "0";
        _xpGained.text = "0";
        _goldGained.text = "0";
        _goldSpent.text = "0";
        StartCoroutine(WrapperCoroutine());
    }

    IEnumerator WrapperCoroutine(){
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(CountUpText(_enemiesSlain, GameStats.enemiesKilled));
        StartCoroutine(CountUpText(_damageDone, Mathf.RoundToInt(GameStats.damageDone)));
        StartCoroutine(CountUpText(_damageTaken, Mathf.RoundToInt(GameStats.damageTaken)));
        StartCoroutine(CountUpText(_abilitiesUsed, GameStats.abilitiesUsed));
        StartCoroutine(CountUpText(_xpGained, GameStats.xpEarned));
        StartCoroutine(CountUpText(_goldGained, GameStats.goldEarned));
        StartCoroutine(CountUpText(_goldSpent, GameStats.goldSpent));
        yield break;
    }

    IEnumerator CountUpText(TMP_Text textToSet, int numToGetTo){
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(timeStep);
        int incrementer = 1;
        int j = 0;
        //If our number is greater than 1000, we want the number to count a little bit faster!
        if(numToGetTo > 1000){
            int toPowerOf = Mathf.RoundToInt(Mathf.Log10(numToGetTo)) - 2;
            incrementer = Mathf.RoundToInt(Mathf.Pow(10, toPowerOf));
        }
        for(int i = 0; i < numToGetTo + 1; i += incrementer){
            textToSet.text = i.ToString();
            j = i;
            yield return wait;
        }
        if(j > numToGetTo || j < numToGetTo){ //Number will count over based off of the incrementer, after we finish just set the number!
            j = numToGetTo;
            textToSet.text = j.ToString();
        }
        yield return null;
    }

}
