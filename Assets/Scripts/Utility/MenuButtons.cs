using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    public void LoadLevel(string levelName){
        SceneManager.LoadScene(levelName);
    }


    public void LoadSettings(){
        SceneManager.LoadScene("SettingsMenu");
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }


    public void QuitGame(){
        Application.Quit();
    }

    public void OpenLink(string link){
        Application.OpenURL(link);
    }

    public void ShowCorruptionToolTip(TMP_Text textToUpdate, string text){
        textToUpdate.text = text;
    }
}
