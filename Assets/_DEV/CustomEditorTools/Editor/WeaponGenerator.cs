using UnityEditor;
using UnityEngine;
using System.Collections;



public class WeaponGenerator : EditorWindow
{
    int amountToCreate = 0;
    int weaponDamageVariance = 5;
    int critChanceVariance = 5;
    float fireRateVariance = 0.25f;
    float reloadSpeedVariance = 0.1f;
    int magSizeVariance = 10;
    int projectileSpeedVariance = 10;

    string weaponName = "Common Pistol";
    float minDamage = 5f;
    float maxDamage = 10f;
    int critChance = 5;
    float fireRate = 0.5f;
    float reloadSpeed = 0.5f;
    int magSize = 20;
    int projectileSpeed = 30;




    int wepType = 0;
    int wepRarity = 0;

    [MenuItem("Tools/Weapon Generator")]
    public static void ShowWindow(){
        GetWindow(typeof(WeaponGenerator));
    }


    private void OnGUI(){
        GUILayout.Label("Generate Multiple Weapons", EditorStyles.boldLabel);
        GUILayout.Space(20);
        
        weaponName = EditorGUILayout.TextField("Default Weapon Name", weaponName);
        amountToCreate = EditorGUILayout.IntField("How many to create?", amountToCreate);
        GUILayout.Space(20);


        GUILayout.Label("Weapon Random Variance", EditorStyles.boldLabel);
        weaponDamageVariance = EditorGUILayout.IntField("Weapon Damage Var", weaponDamageVariance);
        critChanceVariance = EditorGUILayout.IntField("Crit Chance Var", critChanceVariance);
        fireRateVariance = EditorGUILayout.FloatField("Fire Rate Var", fireRateVariance);
        reloadSpeedVariance = EditorGUILayout.FloatField("Reload Speed Var", reloadSpeedVariance);
        magSizeVariance = EditorGUILayout.IntField("Magazine Size Var", magSizeVariance);
        projectileSpeedVariance = EditorGUILayout.IntField("HPorjectile Speed Var", projectileSpeedVariance);
        GUILayout.Space(30);

        GUILayout.Label("Weapon Attributes", EditorStyles.boldLabel);
        minDamage = EditorGUILayout.FloatField("Minimum Damage", minDamage);
        maxDamage = EditorGUILayout.FloatField("Minimum Damage", maxDamage);
        critChance = EditorGUILayout.IntField("Crit Chance", critChance);
        fireRate = EditorGUILayout.FloatField("Fire rate", fireRate);
        reloadSpeed = EditorGUILayout.FloatField("Reload Speed", reloadSpeed);
        magSize = EditorGUILayout.IntField("Magazine Size", magSize);
        projectileSpeed = EditorGUILayout.IntField("Projectile Speed", projectileSpeed);
        GUILayout.Space(30);

        GUILayout.Label("Weapon Type and Rarity", EditorStyles.boldLabel);
        GUILayout.Label("0 - Pistol, 1 - AutomaticRifle, 2 - Shotgun, 3 - Sniper");
        wepType = EditorGUILayout.IntField("Type", wepType);
        GUILayout.Space(10);
        GUILayout.Label("0 - Common, 1 - Uncommon, 2 - Rare, 3 - Corrupted, 4 - Legendary, 5 - Unique");
        wepRarity = EditorGUILayout.IntField("Rarity", wepRarity);
        

        if(GUILayout.Button("Generate Weapons!")){
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
        if(wepType > 3 || wepType < 0){
            Debug.LogWarning($"Invalid Weapon Type, Entered: {wepType}. -> Defaulting to 'Pistol!' ( 0 )");
            wepType = 0;
        }
        //weaponName = weaponName + amountCreated.ToString();
        while(amountCreated < amountToCreate){
            Weapon newWeapon = ScriptableObject.CreateInstance<Weapon>();

            // string weaponTypeFolder = "";
            // string weaponRarityFolder = "";
            switch(wepType){
                case 0:
                    newWeapon.type = Weapon.Type.Pistol;
                    break;
                case 1:
                    newWeapon.type = Weapon.Type.AutomaticRifle;
                    break;
                case 2:
                    newWeapon.type = Weapon.Type.Shotgun;
                    break;
                case 3:
                    newWeapon.type = Weapon.Type.Sniper;
                    break;
            }
            switch(wepRarity){
                case 0:
                    newWeapon.rarity = Weapon.Rarity.Common;
                    break;
                case 1:
                    newWeapon.rarity = Weapon.Rarity.Uncommon;
                    break;
                case 2:
                    newWeapon.rarity = Weapon.Rarity.Rare;
                    break;
                case 3:
                    newWeapon.rarity = Weapon.Rarity.Corrupted;
                    break;
                case 4:
                    newWeapon.rarity = Weapon.Rarity.Legendary;
                    break;
                case 5:
                    newWeapon.rarity = Weapon.Rarity.Unique;
                    break;
            }

            // string fileName = newWeapon.rarity.ToString() + amountCreated.ToString();
            string fileName = $"{newWeapon.rarity} {newWeapon.type} {amountCreated}";
            //weaponName += amountCreated;
            AssetDatabase.CreateAsset(newWeapon, $"Assets/Created Weapons/{newWeapon.type}/{newWeapon.rarity}/{fileName}.asset");
            newWeapon.weaponName = fileName;
            newWeapon.minDamage = Mathf.Ceil(Random.Range(minDamage - weaponDamageVariance, minDamage + weaponDamageVariance));
            newWeapon.maxDamage = Mathf.Ceil(Random.Range(maxDamage - weaponDamageVariance, maxDamage + weaponDamageVariance));
            if(newWeapon.maxDamage <= newWeapon.minDamage){ newWeapon.maxDamage = Mathf.Ceil(newWeapon.minDamage + weaponDamageVariance);}
            newWeapon.critChance = Random.Range(critChance - critChanceVariance, critChance + critChanceVariance);
            newWeapon.fireRate = Mathf.Round(Random.Range(fireRate - fireRateVariance, fireRate + fireRateVariance) * 100f) / 100f;
            newWeapon.reloadSpeed = Mathf.Round(Random.Range(reloadSpeed - reloadSpeedVariance, reloadSpeed + reloadSpeedVariance) * 100f) / 100f;
            newWeapon.magSize = Random.Range(magSize - magSizeVariance, magSize + magSizeVariance);
            newWeapon.projectileSpeed = Random.Range(projectileSpeed - projectileSpeedVariance, projectileSpeed + projectileSpeedVariance);
            Debug.Log($"Created:  '{fileName}' in 'Assets/Created Weapons/{newWeapon.type}/{newWeapon.rarity}'");
            amountCreated++;
        }
        Debug.Log($"Created {amountCreated} weapons!");
    }


}
