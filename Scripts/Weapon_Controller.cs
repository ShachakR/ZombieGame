using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Weapon_Controller : MonoBehaviour
{
    [Header("WeaponStats")]
    public float magazineSize;
    public float currentMagSize;
    public float damage;
    public float range;
    public float fireRate;

    [Header("WeaponInizilier")]
    [NonSerialized] public int Weapon_ID;
    public Camera cam;
    [NonSerialized] public float value; 

    [Header("Particles")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem variantColourParitlces;
    public GameObject[] ImpactEffects;
    private float nextTimetoFire = 0f;
    public bool equipped = false;

    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip reloadSound; 
    private AudioSource source;

    //Misc
    private float intervalToDestroy;
    [HideInInspector] public Color[] colours = {Color.white, Color.green, Color.cyan, Color.red, Color.yellow};
    [HideInInspector] public int VariantType; // the position in Colors[]
    [HideInInspector] public bool updateWeapon = false; //for when a weapon gets equipped
    [HideInInspector] public bool PickedUp;
    [HideInInspector] public bool disableFire = false;

    void Start()
    {
        source = GetComponentInParent<AudioSource>();
        InizlizeWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        if (updateWeapon)
        {
            UpdateWeaponStats();
            updateWeapon = false;
        }

        if (!disableFire)
        {
            if (Input.GetMouseButton(0) && currentMagSize > 0 && Time.time >= nextTimetoFire && equipped && GetComponentInParent<Player_Controller>().isReloading == false)
            {
                FireWeapon();
                currentMagSize--;
                nextTimetoFire = Time.time + 1f / fireRate;
            }
        }

        if (!PickedUp)
        {
            DestroyIfNotPickedUp();
        }

        StopPartileSystem();
    }

    #region FireWeapon
    private void FireWeapon()
    {
        muzzleFlash.Play();
        source.PlayOneShot(shootSound);
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponentInParent<Enemy_Controller>().Health -= damage;
                GameObject impact1 = Instantiate(ImpactEffects[0], hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact1, 2f);
            }
            GameObject impact2 = Instantiate(ImpactEffects[1], hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact2, 1.5f);
        }
    }
    #endregion

    #region StopPartilces
    private void StopPartileSystem(){//stop the particleSystem from the weapon
        if (equipped)
        {
            variantColourParitlces.Stop();
        }
    }
    #endregion

    #region InizlizeWeapon
    private void InizlizeWeapon()
    {
        currentMagSize = magazineSize;
        SetVariantColour();
        VariantStats();
        intervalToDestroy = 15;
        PickedUp = false;
        value =  100 + (100 * VariantType);
        Weapon_ID = ++GameManager.instance.CurrentWeapon_ID;
    }

    private void SetVariantColour()
    {
        double n = Random.Range(0, 100);

        if(n < 40)
        {
            VariantType = 0; 
        }
        if (n >= 40 && n < 70)
        {
            VariantType = 1;
        }
        if (n >= 70 && n < 90)
        {
            VariantType = 2;
        }
        if (n >=90 && n < 98)
        {
            VariantType = 3;
        }
        if (n >= 98 && n < 100)
        {
            VariantType = 4;
        }

        var psMain = variantColourParitlces.main;
        psMain.startColor = colours[VariantType];
    }

    public void VariantStats()
    {
        magazineSize += VariantType * 5;
        damage += VariantType * 5;
        fireRate += VariantType;
    }
    #endregion

    #region UpdateWeapon
    public void UpdateWeaponStats()
    {
        magazineSize = GetComponentInParent<Inventory>().temp.GetComponent<Weapon_Controller>().magazineSize;
        damage = GetComponentInParent<Inventory>().temp.GetComponent<Weapon_Controller>().damage;
        fireRate = GetComponentInParent<Inventory>().temp.GetComponent<Weapon_Controller>().fireRate;
        VariantType = GetComponentInParent<Inventory>().temp.GetComponent<Weapon_Controller>().VariantType;
        Weapon_ID = GetComponentInParent<Inventory>().temp.GetComponent<Weapon_Controller>().Weapon_ID;
    }
    #endregion

    #region WeaponSounds
    public void PlayRealoadSound()
    {
        source.PlayOneShot(reloadSound); 
    }
    #endregion

    #region DestroyIfNotPickedUp
    private void DestroyIfNotPickedUp()
    {
        intervalToDestroy -= Time.deltaTime; 
        for(int i = 0; i < GameObject.Find("Player").GetComponent<Inventory>().gameWeapons.Length; i++)
        {
            if (intervalToDestroy <= 0 && gameObject != GameObject.Find("Player").GetComponent<Inventory>().gameWeapons[i] && i == GameObject.Find("Player").GetComponent<Inventory>().gameWeapons.Length - 1
                && gameObject != GameObject.Find("Player").GetComponent<Inventory>().equippedGun)
            {
                Debug.Log(gameObject.name);
                Destroy(gameObject);
            }
        }
    }
    #endregion
}

