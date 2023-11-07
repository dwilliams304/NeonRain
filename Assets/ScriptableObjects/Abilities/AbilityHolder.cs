using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    
    public AbilityBase ability;
    [SerializeField] Image activeRadialBack;
    [SerializeField] Image cooldownBack;
    [SerializeField] Color cooldownColor;
    [SerializeField] Color availableColor;

    float coolDownTime;
    float activeTime;

    enum AState{
        ready,
        active,
        cooldown
    }
    AState state = AState.ready;

    void OnEnable(){
        ClassSelector.classChosen += StarterAbility;
        cooldownBack.color = availableColor;
    }
    void OnDisable(){
        ClassSelector.classChosen -= StarterAbility;
    }

    public void SwapAbility(AbilityBase newAbility){
        ability = newAbility;
    }

    void StarterAbility(ClassData classChosen){
        if(classChosen.StartingAbility != null){
            ability = classChosen.StartingAbility;
        }
    }



    void Update()
    {
        switch(state){
            case AState.ready:
                if(Input.GetKeyDown(KeyCode.Alpha1)){
                    if(ability != null){
                        ability.UseAbility();
                        state = AState.active;
                        activeTime = ability.activeTime;
                        GameStats.abilitiesUsed++;
                    }
                }
                break;
            case AState.active:
                if(activeTime > 0){
                    activeTime -= Time.deltaTime;
                    activeRadialBack.enabled = true;
                    activeRadialBack.fillAmount = activeTime / ability.activeTime;
                }else{
                    ability.AbilityComplete();
                    state = AState.cooldown;
                    coolDownTime = ability.coolDownTime;
                    activeRadialBack.enabled = false;
                }
                break;
            case AState.cooldown:
                if(coolDownTime > 0){
                    coolDownTime -= Time.deltaTime;
                    cooldownBack.enabled = true;
                    cooldownBack.color = cooldownColor;
                    cooldownBack.fillAmount = coolDownTime / ability.coolDownTime;
                }else{
                    state = AState.ready;
                    cooldownBack.fillAmount = 1;
                    cooldownBack.color = availableColor;
                }
                break;
        }
    }
}
