using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Backpack_Slot : MonoBehaviour
{
    [Header("OBJECTS")]
    public GameObject obj;
    public GameObject fillBorder;
    public Image icon; 
    // Start is called before the first frame update
    void Start()
    {
        fillBorder = transform.GetChild(1).gameObject;
        icon.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    private void SetText()//set the text for the backpack slot UI
    {
        if(obj != null)
        {
            GetComponentInChildren<Text>().text = obj.name + "\n" + "Level: " +
            obj.GetComponent<Weapon_Controller>().VariantType;
            fillBorder.GetComponent<Image>().color = obj.GetComponent<Weapon_Controller>().colours[obj.GetComponent<Weapon_Controller>().VariantType];
            SetIcon(obj.name);
        }
        else
        {
            GetComponentInChildren<Text>().text = "None";
            fillBorder.GetComponent<Image>().color = Color.black;
            icon.color = new Color(0f, 0f, 0f, 0f); 
        }
    }

    public void EquipObj()
    {
        if (GameManager.instance.player.GetComponent<Inventory>().currentInvEmptySlot > 2)
        {
            GameManager.instance.player.GetComponent<Inventory>().currentInvEmptySlot = 0;
        }

        GameManager.instance.player.GetComponent<Inventory>().invWeapons[GameManager.instance.player.GetComponent<Inventory>().currentInvEmptySlot] = obj;

        GameObject.Find("UI_Main").GetComponent<UI_Main_Controller>().InvSlots[GameManager.instance.player.GetComponent<Inventory>().currentInvEmptySlot].GetComponentInChildren<Image>().color =
           obj.GetComponent<Weapon_Controller>().colours[obj.GetComponent<Weapon_Controller>().VariantType]; // for the UI display of the weapon colour variant 

        GameManager.instance.player.GetComponent<Inventory>().currentInvEmptySlot++;
    }

    public void SellObj()
    {
        //get its value and sell
        float value = obj.GetComponent<Weapon_Controller>().value;
        GameManager.instance.player.GetComponent<Player_Controller>().money += value;
        UI_Controller.instance.UI_Notification("Sold For:", value.ToString());

        //unEquip the weapon if its equipped
        GameManager.instance.player.GetComponent<Inventory>().UnEquipGun(obj);

        //remove from pickupItems List

        for (int i = 0; i < GameManager.instance.player.GetComponent<PickUp_Controller>().PickedUpItems.Count; i++)
        {
            if(obj == GameManager.instance.player.GetComponent<PickUp_Controller>().PickedUpItems[i])
            {
                GameManager.instance.player.GetComponent<PickUp_Controller>().PickedUpItems.Remove(GameManager.instance.player.GetComponent<PickUp_Controller>().PickedUpItems[i]); 
            }
        }
            //destroy 
            Destroy(obj);
        obj = null; 
    }

    private void SetIcon(string name)
    {
        switch (name) {
            case "Ak47":
                icon.sprite = UI_Controller.instance.Icons[0];
                break;
            case "Ar2":
                icon.sprite = UI_Controller.instance.Icons[1];
                break;
        }
        icon.color = obj.GetComponent<Weapon_Controller>().colours[obj.GetComponent<Weapon_Controller>().VariantType];
    }
}
