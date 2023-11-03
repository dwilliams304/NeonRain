using UnityEditor;
using UnityEngine;



public class GunGenerator : EditorWindow
{
    int amountToCreate = 1;
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
    AudioClip gunShotSound;

    int detailedType = 0;
    int wepRarity = 0;

    [MenuItem("Tools/Weapon Generator/Gun Generator")]
    public static void ShowWindow(){
        GetWindow(typeof(GunGenerator));
    }


    private void OnGUI(){
        GUILayout.Label("Generate Multiple Guns", EditorStyles.boldLabel);
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
        maxDamage = EditorGUILayout.FloatField("Maximum Damage", maxDamage);
        critChance = EditorGUILayout.IntField("Crit Chance", critChance);
        fireRate = EditorGUILayout.FloatField("Fire rate", fireRate);
        reloadSpeed = EditorGUILayout.FloatField("Reload Speed", reloadSpeed);
        magSize = EditorGUILayout.IntField("Magazine Size", magSize);
        projectileSpeed = EditorGUILayout.IntField("Projectile Speed", projectileSpeed);
        GUILayout.Space(30);

        GUILayout.Label("Weapon Type and Rarity", EditorStyles.boldLabel);
        GUILayout.Label("0 - Pistol, 1 - Revolver, 2 - Automatic_Rifle, 3 - Shotgun, 4 - Sniper, 5 - Submachine_Gun");
        detailedType = EditorGUILayout.IntField("Type", detailedType);
        GUILayout.Space(10);
        GUILayout.Label("0 - Common, 1 - Uncommon, 2 - Rare, 3 - Corrupted, 4 - Legendary, 5 - Unique");
        wepRarity = EditorGUILayout.IntField("Rarity", wepRarity);

        GUILayout.Space(20);
        GUILayout.Label("Gunshot Sound!");
        gunShotSound = (AudioClip)EditorGUILayout.ObjectField("Sound", gunShotSound, typeof(AudioClip), true);
        

        if(GUILayout.Button("Generate Guns!")){
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
            Debug.LogWarning($"Invalid Weapon Type, Entered: {detailedType}. -> Defaulting to 'Pistol!' ( 0 )");
            detailedType = 0;
        }
        //weaponName = weaponName + amountCreated.ToString();
        while(amountCreated < amountToCreate){

            Gun gun = ScriptableObject.CreateInstance<Gun>();
            switch(detailedType){
                case 0:
                    gun.gunType = GunType.Pistol;
                    break;
                case 1:
                    gun.gunType = GunType.Revolver;
                    break;
                case 2:
                    gun.gunType = GunType.Automatic_Rifle;
                    break;
                case 3:
                    gun.gunType = GunType.Shotgun;
                    break;
                case 4:
                    gun.gunType = GunType.Sniper;
                    break;
                case 5:
                    gun.gunType = GunType.Submachine_Gun;
                    break;
            }

            switch(wepRarity){
                case 0:
                    gun.rarity = Rarity.Common;
                    gun.color = Color.white;
                    break;
                case 1:
                    gun.rarity = Rarity.Uncommon;
                    gun.color = Color.green;
                    break;
                case 2:
                    gun.rarity = Rarity.Rare;
                    gun.color = Color.blue;
                    break;
                case 3:
                    gun.rarity = Rarity.Corrupted;
                    gun.color = Color.red;
                    break;
                case 4:
                    gun.rarity = Rarity.Legendary;
                    gun.color = Color.yellow;
                    break;
                case 5:
                    gun.rarity = Rarity.Unique;
                    gun.color = Color.magenta;
                    break;
            }
            amountCreated++;

            string fileName = $"{gun.rarity} {gun.gunType} {amountCreated}";
            //weaponName += amountCreated;
            AssetDatabase.CreateAsset(gun, $"Assets/Created Weapons/Guns/{gun.gunType}/{gun.rarity}/{fileName}.asset");
            gun.weaponName = fileName;
            gun.minDamage = Mathf.Ceil(Random.Range(minDamage - weaponDamageVariance, minDamage + weaponDamageVariance));
            gun.maxDamage = Mathf.Ceil(Random.Range(maxDamage - weaponDamageVariance, maxDamage + weaponDamageVariance));
            gun.gunShot = gunShotSound;
            if(gun.maxDamage <= gun.minDamage){ gun.maxDamage = Mathf.Ceil(gun.minDamage + weaponDamageVariance);}
            gun.critChance = Random.Range(critChance - critChanceVariance, critChance + critChanceVariance);
            gun.fireRate = Mathf.Round(Random.Range(fireRate - fireRateVariance, fireRate + fireRateVariance) * 100f) / 100f;
            gun.reloadSpeed = Mathf.Round(Random.Range(reloadSpeed - reloadSpeedVariance, reloadSpeed + reloadSpeedVariance) * 100f) / 100f;
            gun.magSize = Random.Range(magSize - magSizeVariance, magSize + magSizeVariance);
            gun.projectileSpeed = Random.Range(projectileSpeed - projectileSpeedVariance, projectileSpeed + projectileSpeedVariance);
            Debug.Log($"Created:  '{fileName}' in 'Assets/Created Weapons/Guns/{gun.gunType}/{gun.rarity}'");
        }
        Debug.Log($"Created {amountCreated} guns!");
    }


}
