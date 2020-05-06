using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Miscellaneous")]
    [HideInInspector] public Animator anim;
    public bool isReloading = false;
    private bool playSound = false;

    [Header("STATS")]
    public float health;
    public float money;
    public float ammo;

    [Header("OBJECTS")]
    public List<GameObject> defenceObjs = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ReloadController();
    }

    public void SetDefenceActive()
    {
        foreach(GameObject obj in defenceObjs)
        {
            obj.SetActive(false);
        }
        defenceObjs[0].SetActive(true); 
    }

    #region ReloadWeapon 
    private void ReloadController(){
       GameObject equppiedGun = GetComponent<Inventory>().equippedGun;

        if (isReloading == false)
        {
            if (Input.GetKeyDown(KeyCode.R) && GetComponent<Player_Controller>().ammo > equppiedGun.GetComponent<Weapon_Controller>().magazineSize && GetComponent<Player_Controller>().ammo > 0)
            {
                anim.SetTrigger(equppiedGun.GetComponent<Weapon_Controller>().name);
                equppiedGun.GetComponent<Weapon_Controller>().currentMagSize = equppiedGun.GetComponent<Weapon_Controller>().magazineSize;
                GetComponent<Player_Controller>().ammo -= equppiedGun.GetComponent<Weapon_Controller>().magazineSize;
            }
            else if (Input.GetKeyDown(KeyCode.R) && GetComponent<Player_Controller>().ammo < equppiedGun.GetComponent<Weapon_Controller>().magazineSize && GetComponent<Player_Controller>().ammo > 0)
            {
                anim.SetTrigger(equppiedGun.GetComponent<Weapon_Controller>().name);
                equppiedGun.GetComponent<Weapon_Controller>().currentMagSize = GetComponent<Player_Controller>().ammo;
                GetComponent<Player_Controller>().ammo = 0;
            }
            else
            {
               //noammo
            }
        }

        if (isReloading && playSound == true)
        {
            equppiedGun.GetComponent<Weapon_Controller>().PlayRealoadSound();
            playSound = false; 
        }
    }

    public void SetReloadingTrue(){
        isReloading = true;
        playSound = true; 
    }
    public void SetReloadingFalse()
    {
        isReloading = false;
    }
    #endregion
}
