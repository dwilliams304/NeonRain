using UnityEditor;
using UnityEngine;



public class SwordGenerator : EditorWindow
{
    int amountToCreate = 1;
    int weaponDamageVariance = 5;
    int critChanceVariance = 5;
    float swingRateVariance = 0.25f;
    float swingCoolDownVariance = 0.25f;

    string weaponName = "Common Sword";
    float minDamage = 5f;
    float maxDamage = 10f;
    int critChance = 5;
    float swingRate = 0.5f;
    float swingCoolDown = 0.5f;

    int detailedType = 0;
    int wepRarity = 0;

    [MenuItem("Tools/Weapon Generator/Sword Generator")]
    public static void ShowWindow(){
        GetWindow(typeof(SwordGenerator));
    }


    private void OnGUI(){
        GUILayout.Label("Generate Multiple Swords", EditorStyles.boldLabel);
        GUILayout.Space(20);
        
        weaponName = EditorGUILayout.TextField("Default Weapon Name", weaponName);
        amountToCreate = EditorGUILayout.IntField("How many to create?", amountToCreate);
        GUILayout.Space(20);

        GUILayout.Label("Weapon Random Variance", EditorStyles.boldLabel);
        weaponDamageVariance = EditorGUILayout.IntField("Weapon Damage Var", weaponDamageVariance);
        critChanceVariance = EditorGUILayout.IntField("Crit Chance Var", critChanceVariance);
        swingRateVariance = EditorGUILayout.FloatField("Swing Rate Var", swingRateVariance);
        swingCoolDownVariance = EditorGUILayout.FloatField("Swing Cooldown Var", swingCoolDownVariance);
        GUILayout.Space(30);

        GUILayout.Label("Weapon Attributes", EditorStyles.boldLabel);
        minDamage = EditorGUILayout.FloatField("Minimum Damage", minDamage);
        maxDamage = EditorGUILayout.FloatField("Maximum Damage", maxDamage);
        critChance = EditorGUILayout.IntField("Crit Chance", critChance);
        swingRate = EditorGUILayout.FloatField("Swing Rate", swingRate);
        swingCoolDown = EditorGUILayout.FloatField("Swing Cooldown", swingCoolDown);
        GUILayout.Space(30);

        GUILayout.Label("Weapon Type and Rarity", EditorStyles.boldLabel);
        GUILayout.Label("0 - Sword, 1 - Saber, 2 - Dagger, 3 - Scythe");
        detailedType = EditorGUILayout.IntField("Type", detailedType);
        GUILayout.Space(10);
        GUILayout.Label("0 - Common, 1 - Uncommon, 2 - Rare, 3 - Corrupted, 4 - Legendary, 5 - Unique");
        wepRarity = EditorGUILayout.IntField("Rarity", wepRarity);
        

        if(GUILayout.Button("Generate Swords!")){
            if(amountToCreate <= 0){
                Debug.LogError($"Can't generate {amountToCreate} weapons, please enter a valid number");
            }else{
                CreateWeapons();
            }
        }
    }


    void CreateWeapons(){
        int amountCreated = 0;
        if(wepRarity > 5 || wepRarity < 0){
            Debug.LogWarning($"Invalid Weapon Rarity, Entered: {wepRarity}. -> Defaulting to 'Common' ( 0 )");
            wepRarity = 0;
        }
        if(detailedType > 3 || detailedType < 0){
            Debug.LogWarning($"Invalid Sword Type, Entered: {detailedType}. -> Defaulting to 'Sword!' ( 0 )");
            detailedType = 0;
        }
        //weaponName = weaponName + amountCreated.ToString();
        while(amountCreated < amountToCreate){

            Sword sword = ScriptableObject.CreateInstance<Sword>();
            sword.weaponType = WeaponType.Sword;
            switch(detailedType){
                case 0:
                    sword.swordType = SwordType.Sword;
                    break;
                case 1:
                    sword.swordType = SwordType.Saber;
                    break;
                case 2:
                    sword.swordType = SwordType.Dagger;
                    break;
                case 3:
                    sword.swordType = SwordType.Scythe;
                    break;  
            }
            AssignStats(sword, amountCreated);

            amountCreated++;
        }
        Debug.Log($"Created {amountCreated} swords!");
    }

    Weapon AssignRarity(Weapon weapon){
        switch(wepRarity){
                case 0:
                    weapon.rarity = Rarity.Common;
                    break;
                case 1:
                    weapon.rarity = Rarity.Uncommon;
                    break;
                case 2:
                    weapon.rarity = Rarity.Rare;
                    break;
                case 3:
                    weapon.rarity = Rarity.Corrupted;
                    break;
                case 4:
                    weapon.rarity = Rarity.Legendary;
                    break;
                case 5:
                    weapon.rarity = Rarity.Unique;
                    break;
            }
        return weapon;
    }

    void AssignStats(Sword weapon, int amountCreated){

            string fileName = $"{weapon.rarity} {weapon.swordType} {amountCreated}";
            //weaponName += amountCreated;
            AssetDatabase.CreateAsset(weapon, $"Assets/Created Weapons/Swords/{weapon.swordType}/{weapon.rarity}/{fileName}.asset");
            weapon.weaponName = fileName;
            weapon.minDamage = Mathf.Ceil(Random.Range(minDamage - weaponDamageVariance, minDamage + weaponDamageVariance));
            weapon.maxDamage = Mathf.Ceil(Random.Range(maxDamage - weaponDamageVariance, maxDamage + weaponDamageVariance));
            if(weapon.maxDamage <= weapon.minDamage){ weapon.maxDamage = Mathf.Ceil(weapon.minDamage + weaponDamageVariance);}
            weapon.critChance = Random.Range(critChance - critChanceVariance, critChance + critChanceVariance);
            Debug.Log($"Created:  '{fileName}' in 'Assets/Created Weapons/Swords/{weapon.swordType}/{weapon.rarity}'");
    }


}
