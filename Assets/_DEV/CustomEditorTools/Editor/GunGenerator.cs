using UnityEditor;
using UnityEngine;

public class GunGenerator : EditorWindow
{
    //Basic Details
    string weaponName = "";
    int amountToCreate = 5;
    GunType detailedType = 0;
    Rarity wepRarity = 0;

    //Random Variances (+/-) -> if weaponDamageVariance = 5, minDamage/maxDamage will be anywhere between damage-5 -> damage+5
    int weaponDamageVariance = 5;
    int critChanceVariance = 5;
    float fireRateVariance = 0.25f;
    float reloadSpeedVariance = 0.1f;
    int magSizeVariance = 10;
    int projectileSpeedVariance = 10;

    //Actual vars
    float minDamage = 5f;
    float maxDamage = 10f;
    int critChance = 5;
    float fireRate = 0.5f;
    float reloadSpeed = 0.5f;
    int magSize = 20;
    int projectileSpeed = 30;

    //Visuals/Sound
    Color gunColor = Color.white;
    AudioClip gunShotSound;
    Sprite gunSprite;

    [MenuItem("Tools/Weapon Generator/Gun Generator")]
    public static void ShowWindow(){
        GetWindow(typeof(GunGenerator));
    }

    private void OnGUI(){
        GUILayout.Label("Generate Multiple Guns", EditorStyles.largeLabel);
        GUILayout.Space(20);
        
        weaponName = EditorGUILayout.TextField("Default Weapon Name", weaponName);
        amountToCreate = EditorGUILayout.IntField("How many to create?", amountToCreate);
        detailedType = (GunType)EditorGUILayout.EnumPopup("Type", detailedType);
        wepRarity = (Rarity)EditorGUILayout.EnumPopup("Rarity", wepRarity);
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

        GUILayout.Label("Other Attributes.");
        gunColor = EditorGUILayout.ColorField("Color", gunColor);
        gunShotSound = (AudioClip)EditorGUILayout.ObjectField("Sound", gunShotSound, typeof(AudioClip), true);
        GUILayout.Label("Gun Icon");
        gunSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", gunSprite, typeof(Sprite), true);
        
        weaponName = $"{wepRarity} {detailedType}";
        switch(wepRarity){
            case Rarity.Common:
                gunColor = Color.white;
            break;

            case Rarity.Uncommon:
                gunColor = Color.green;
            break;

            case Rarity.Rare:
                gunColor = Color.blue;
            break;

            case Rarity.Corrupted:
                gunColor = Color.red;
            break;

            case Rarity.Legendary:
                gunColor = Color.yellow;
            break;

            case Rarity.Unique:
                gunColor = Color.magenta;
            break;
        }

        if(GUILayout.Button("Generate Guns!")){
            if(amountToCreate <= 0 || gunSprite == null || gunShotSound == null || weaponName == ""){
                Debug.LogError("Error generating guns. Are you missing a sprite/sound/name?");
            }else{
                CreateWeapons();
            }
        }
    }


    //Will change the default values based off of the weapon type. ----WIP----
    void ChangeBaseWeaponStats(GunType type){
        switch(type){
            case GunType.Pistol:
            break;

            case GunType.Revolver:
            break;

            case GunType.Automatic_Rifle:
            break;

            case GunType.Shotgun:
            break;

            case GunType.Sniper:
            break;

            case GunType.Submachine_Gun:
            break;
        }
    }


    void CreateWeapons(){
        int amountCreated = 0;
        //weaponName = weaponName + amountCreated.ToString();
        while(amountCreated < amountToCreate){
            Gun gun = CreateInstance<Gun>();
            amountCreated++;
            

            string fileName = $"{wepRarity} {detailedType} {amountCreated}";
            AssetDatabase.CreateAsset(gun, $"Assets/Created Weapons/Guns/{wepRarity}/{fileName}.asset");
            gun.weaponName = fileName;
            gun.gunType = detailedType;
            gun.rarity = wepRarity;
            gun.minDamage = Mathf.Ceil(Random.Range(minDamage - weaponDamageVariance, minDamage + weaponDamageVariance));
            gun.maxDamage = Mathf.Ceil(Random.Range(maxDamage - weaponDamageVariance, maxDamage + weaponDamageVariance));
            if(gun.maxDamage <= gun.minDamage){ gun.maxDamage = Mathf.Ceil(gun.minDamage + weaponDamageVariance);}
            gun.critChance = Random.Range(critChance - critChanceVariance, critChance + critChanceVariance);
            gun.fireRate = Mathf.Round(Random.Range(fireRate - fireRateVariance, fireRate + fireRateVariance) * 100f) / 100f;
            gun.reloadSpeed = Mathf.Round(Random.Range(reloadSpeed - reloadSpeedVariance, reloadSpeed + reloadSpeedVariance) * 100f) / 100f;
            gun.magSize = Random.Range(magSize - magSizeVariance, magSize + magSizeVariance);
            gun.projectileSpeed = Random.Range(projectileSpeed - projectileSpeedVariance, projectileSpeed + projectileSpeedVariance);
            gun.color = gunColor;
            gun.gunShot = gunShotSound;
            gun.weaponSprite = gunSprite;
            Debug.Log($"Created:  '{fileName}' in 'Assets/Created Weapons/Guns/{gun.gunType}/{gun.rarity}'");
        }
        Debug.Log($"Created {amountCreated} guns!");
    }


}
