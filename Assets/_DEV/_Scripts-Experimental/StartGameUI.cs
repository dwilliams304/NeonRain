using System.Collections;
using TMPro;
using UnityEngine;

public class StartGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _startingPanel;
    [SerializeField] private TMP_Text _startingText;
    [SerializeField] private AudioClip _startingAudio;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip _fadeIn;
    [SerializeField] private AnimationClip _fadeOut;
    

    void OnEnable(){
        FollowingSpawner.onGameStart += ShowStartText;
        FollowingSpawner.onStartGameFinished += StartGame;
    }
    void OnDisable(){
        FollowingSpawner.onGameStart -= ShowStartText;
        FollowingSpawner.onStartGameFinished -= StartGame;
    }


    void ShowStartText(float delay){
        _startingPanel.SetActive(true);
        anim.AddClip(_fadeIn, "Fade In");
        anim.AddClip(_fadeOut, "Fade Out");
        anim.Play("Fade In");
        _startingText.text = $"You have {Mathf.RoundToInt(delay)} seconds until game start.";
    }

    void StartGame(){
        StartCoroutine(ChangeText());
        SoundManager.Instance.PlayEffectAudio(_startingAudio);
    }

    IEnumerator ChangeText(){
        _startingText.text = "Good luck...";
        yield return new WaitForSeconds(2f);
        anim.Play("Fade Out");
        yield return new WaitForSeconds(2f);
        _startingPanel.SetActive(false);
    }
}
