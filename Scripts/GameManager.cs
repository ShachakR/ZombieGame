using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("GAMEOBJECTS")]
    public GameObject[] PrefabWeapons;
    public GameObject[] PrefabDefences; 
    public GameObject player;
    public GameObject CountDown;
    public GameObject objective;

    [Header("USER INTERFACE VALUES")]
    public Text KillsValue_Main;
    public Text WaveValue_Main;
    public Text KillsValue_Final;
    public Text WaveValue_Final;
    public Text money;

    [Header("VALUES")]
    public int wave;
    public int totalKills;

    [HideInInspector] public int CurrentWeapon_ID;
    private bool endGame;
    void Start()
    {
        wave = 1;
        totalKills = 0;
        CurrentWeapon_ID = 0;
        endGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        money.text = player.GetComponent<Player_Controller>().money.ToString();
        WaveValue_Main.text = wave.ToString();
        WaveValue_Final.text = wave.ToString();
        KillsValue_Main.text = totalKills.ToString();
        KillsValue_Final.text = totalKills.ToString();

        EndGame(); 
    }

    #region LevelManager - In Enemy Spanwer

    public void WaveCompleteNotification()
    {
        GameObject.Find("UI").GetComponent<UI_Controller>().UI_Notification("NEW", "WAVE");
    }

    public void CountDownStart(float seconds)
    {
        CountDown.SetActive(true);
        CountDown.GetComponentInChildren<TextMeshProUGUI>().text = Math.Ceiling(seconds).ToString();
        CountDown.transform.GetChild(0).GetComponent<Image>().fillAmount = seconds / 25f; 
    }
    public void CountDownStop()
    {
        CountDown.SetActive(false);
    }
    #endregion

    #region EndGame 

    private void EndGame()
    {
        Vector3 tempValue = objective.transform.GetChild(0).transform.localScale;

        if (endGame == false && (player.GetComponent<Player_Controller>().health <= 0 || tempValue[2] <= 0))
        {
            UI_Controller.instance.EndGame();
            Time.timeScale = 0;
            Debug.Log("GAME OVER!");
            endGame = true;
        }
    }

    #endregion
}
