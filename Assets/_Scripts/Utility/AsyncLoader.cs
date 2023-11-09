using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AsyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TMP_Text flavorText;

    [SerializeField] private List<string> loadingFlavorText;


    public void LoadLevelBtn(string levelName){
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        ChangeText();
        StartCoroutine(Async_LoadLevel(levelName));
    }

    IEnumerator Async_LoadLevel(string levelName){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelName);
        while(!loadOperation.isDone){
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingBar.value = progress;
            yield return new WaitForSeconds(5f);
            ChangeText();
        }
    }

    void ChangeText(){
        int i = Random.Range(0, loadingFlavorText.Count);
        flavorText.text = loadingFlavorText[i];
    }

}
