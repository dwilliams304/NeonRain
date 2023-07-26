using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    
    public AbilityBase ability;

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
                    }
                }
                break;
            case AState.active:
                if(activeTime > 0){
                    activeTime -= Time.deltaTime;
                }else{
                    ability.AbilityComplete();
                    state = AState.cooldown;
                    coolDownTime = ability.coolDownTime;
                }
                break;
            case AState.cooldown:
                if(coolDownTime > 0){
                    coolDownTime -= Time.deltaTime;
                }else{
                    state = AState.ready;
                }
                break;
        }
    }
}
