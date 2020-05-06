using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Main_Controller : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject ammoDisplay;
    public GameObject PickUpDisplay;
    public GameObject WeaponDisplay;
    public GameObject ammoBar;
    public Image healthBar; 
    public GameObject[] InvSlots;

    private float ammo;
    private float currentMag = 0;
    private GameObject player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player; 
        PickUpDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ammo = player.GetComponent<Player_Controller>().ammo;
        healthBar.fillAmount = GameManager.instance.player.GetComponent<Player_Controller>().health / 100;

        if (player.GetComponent<Inventory>().equippedGun != null)
        {
            currentMag = player.GetComponent<Inventory>().equippedGun.GetComponent<Weapon_Controller>().currentMagSize;//for the UI current mag display

            WeaponDisplay.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Inventory>().equippedGun.name; // for the UI, which weapon is equipped

            WeaponDisplay.GetComponent<TextMeshProUGUI>().color = player.GetComponent<Inventory>().equippedGun.GetComponent<Weapon_Controller>().colours[
                player.GetComponent<Inventory>().equippedGun.GetComponent<Weapon_Controller>().VariantType]; // for the UI which colour it is 

            ammoBar.GetComponent<Image>().fillAmount = player.GetComponent<Inventory>().equippedGun.GetComponent<Weapon_Controller>().currentMagSize /
                player.GetComponent<Inventory>().equippedGun.GetComponent<Weapon_Controller>().magazineSize; //set AmmoBar display
        }
        else
        {
            WeaponDisplay.GetComponent<TextMeshProUGUI>().text = "";
            WeaponDisplay.GetComponent<TextMeshProUGUI>().color = Color.black;
            currentMag = 0;
        }

        ammoDisplay.GetComponent<TextMeshProUGUI>().text = currentMag.ToString() + "/" + ammo.ToString();

        if (player.GetComponent<PickUp_Controller>().pickUpAviable == true)
        {
            PickUpDisplay.SetActive(true);
            PickUpDisplay.GetComponent<TextMeshProUGUI>().text = "E to pick up " + player.GetComponent<PickUp_Controller>().objectHit.name;
        }
        else
        {
            PickUpDisplay.SetActive(false);
        }
    }
}
