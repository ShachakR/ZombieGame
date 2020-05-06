using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class WeaponStats_UI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text DamageTxt;
    public Text FirerateTxt;
    public Text MagazineSizeTxt;
    public Text WeaponName;
    public GameObject WeaponStatsUI;

    private GameObject WeaponObj; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenWeaponStats()
    {
            if (GetComponent<Backpack_Slot>().obj)
            {
            WeaponStatsUI.SetActive(true);
            WeaponObj = GetComponent<Backpack_Slot>().obj;

            WeaponName.text = WeaponObj.GetComponent<Weapon_Controller>().name;
            DamageTxt.text = "Damage: " + WeaponObj.GetComponent<Weapon_Controller>().damage;
            FirerateTxt.text = "FireRate: " + WeaponObj.GetComponent<Weapon_Controller>().fireRate;
            MagazineSizeTxt.text = "MagazineSize: " + WeaponObj.GetComponent<Weapon_Controller>().magazineSize;
            }
    }
    public void CloseWeaponStats()
    {
        WeaponStatsUI.SetActive(false);
    }

}
