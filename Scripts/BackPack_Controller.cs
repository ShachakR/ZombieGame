using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPack_Controller : MonoBehaviour
{
    public Button[] buttons;
    private Button[] temp;
    // Start is called before the first frame update
    void Start()
    {
        SetButtons();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateBackpack()
    {
        foreach (Button button in buttons)
        {
            button.GetComponent<Backpack_Slot>().obj = null;
        }

        for (int i = 0; i < GameManager.instance.player.GetComponent<PickUp_Controller>().PickedUpItems.Count; i++){
            buttons[i].GetComponent<Backpack_Slot>().obj = GameManager.instance.player.GetComponent<PickUp_Controller>().PickedUpItems[i];//give each button an object that was picked up
        }
    }

    public void SetButtons()//set the buttons from the UI to the buttons Array
    {
        temp = GameObject.Find("Item List").GetComponentsInChildren<Button>();//takes all buttons in ItemList
        int index = 0;
        for(int i = 0; i < temp.Length; i++)
        {
            if(temp[i].CompareTag("Item Button"))//Filters to take only the main Item Button
            {
                buttons[index] = temp[i];
                index++;
            }
        }
    }
}
