using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClassSelector : MonoBehaviour
{
    [SerializeField] private List<ClassData> possibleClasses;

    [SerializeField] TMP_Text className;
    [SerializeField] TMP_Text classHealth;
    [SerializeField] TMP_Text classDamage;
    [SerializeField] TMP_Text classDamageTaken;
    [SerializeField] TMP_Text classFireRate;
    [SerializeField] TMP_Text classMoveSpeed;
    [SerializeField] TMP_Text classDashSpeed;
    [SerializeField] TMP_Text totalClasses;

    public delegate void ClassChosen(ClassData classChosen);
    public static ClassChosen classChosen;

    private int currentIdx = 0;

    void Start(){
        UpdateUI(possibleClasses[currentIdx]);
        Time.timeScale = 0f;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            CycleLeft();
        }else if(Input.GetKeyDown(KeyCode.D)){
            CycleRight();
        }
    }

    void UpdateUI(ClassData classInfo){
        className.text = classInfo.ClassName;
        classHealth.text = classInfo.MaxHealth.ToString();
        classDamage.text = classInfo.DamageDone.ToString();
        classDamageTaken.text = classInfo.DamageTaken.ToString();
        classMoveSpeed.text = classInfo.MoveSpeed.ToString();
        classDashSpeed.text = classInfo.DashSpeed.ToString();
        totalClasses.text = $"{currentIdx + 1} of {possibleClasses.Count}";
    }

    public void CycleRight(){
        currentIdx++;
        if(currentIdx >= possibleClasses.Count){
            currentIdx = 0;
        }
        UpdateUI(possibleClasses[currentIdx]);
    }
    public void CycleLeft(){
        currentIdx--;
        if(currentIdx < 0){
            currentIdx = possibleClasses.Count - 1;
        }
        UpdateUI(possibleClasses[currentIdx]);
    }

    public void ConfirmChoice(){
        // PlayerStats.playerStats.ClassChosen(possibleClasses[currentIdx]);
        classChosen?.Invoke(possibleClasses[currentIdx]);
        Time.timeScale = 1f;
    }
}
