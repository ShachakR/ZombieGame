using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] gameWeapons;
    public GameObject[] invWeapons;
    bool gunEquiiped = false;
    public GameObject equippedGun = null;
    [HideInInspector] public int currentInvEmptySlot = 0;
    [HideInInspector] public GameObject temp;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region InvWeaponsKeys
        if (Input.GetKeyDown(KeyCode.Alpha1) && invWeapons[0] != null)
        {
            EquipGun(0);
            gunEquiiped = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && invWeapons[1] != null)
        {
            EquipGun(1);
            gunEquiiped = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && invWeapons[2] != null)
        {
            EquipGun(2);
            gunEquiiped = true;
        }
        #endregion 

        if (gunEquiiped == true && equippedGun.CompareTag("Rifle"))
        {
            anim.SetBool("RifleEquipped", true);
        }
        else
        {
            anim.SetBool("RifleEquipped", false);
        }
    }

    #region Equip/UnEquip Weapon
    private void EquipGun(int n)
    {
        for (int i = 0; i < gameWeapons.Length; i++)
        {
            if (gameWeapons[i].name.Equals(invWeapons[n].name))//if ak47 is in gameweapons and its in the inveweapon slots
            {
                temp = invWeapons[n];//to use for updating the gamewepon to fit the invweapon stats
                gameWeapons[i].GetComponent<Weapon_Controller>().updateWeapon = true;
                gameWeapons[i].GetComponent<Weapon_Controller>().equipped = true;
                equippedGun = gameWeapons[i];
                gameWeapons[i].SetActive(true);
            }
            else
            {
                gameWeapons[i].SetActive(false);
                gameWeapons[i].GetComponent<Weapon_Controller>().equipped = false;
            }
        }
    }

    public void UnEquipGun(GameObject gun)
    {
        //remove it from inventory
        for (int i = 0; i < invWeapons.Length; i++)
        {
            if (gun == invWeapons[i])
            {
                GameObject.Find("UI_Main").GetComponent<UI_Main_Controller>().InvSlots[i].GetComponentInChildren<Image>().color = Color.black; //ResetInvSlot UI Color

                invWeapons[i] = null;
                for(int j = 0; j < gameWeapons.Length; j++)
                {
                    if(gun.GetComponent<Weapon_Controller>().Weapon_ID == gameWeapons[j].GetComponent<Weapon_Controller>().Weapon_ID)
                    {
                        if (gameWeapons[j].GetComponent<Weapon_Controller>().equipped)
                        {
                            gameWeapons[j].GetComponent<Weapon_Controller>().equipped = false;
                            gameWeapons[j].SetActive(false);
                            equippedGun = null;
                            gunEquiiped = false; 
                        }
                    }
                }
                break;
            }
        }
    }
    #endregion 
}
