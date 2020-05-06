using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Controller : MonoBehaviour
{
    public Camera camera;
    int count = 0;
    [HideInInspector] public bool pickUpAviable = false;
    [HideInInspector] public Transform objectHit;
    public List<GameObject> PickedUpItems = new List<GameObject>();//track which items were pickedup, array size = backup size
    private void Start()
    {
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            objectHit = hit.transform;

            if (objectHit.CompareTag("Rifle"))
            {
                pickUpAviable = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.GetComponent<Weapon_Controller>().PickedUp = true; //Inorder for the object not to be destryoed: cancel the destroy method 
                    Debug.Log(PickedUpItems.Count);
                    PickedUpItems.Add(hit.transform.gameObject);//add object to pickedup items Array
                    hit.transform.gameObject.SetActive(false);//remove objects from screen
                    hit.transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    GameObject.Find("UI").GetComponent<UI_Controller>().UI_Notification("Collected", hit.transform.gameObject.name);
                }
            }
            else
            {
                pickUpAviable = false;
            }
        }
    }
}
