using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    #region Singleton
    public static UI_Controller instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("UI Elements")]
    public GameObject PickedUpNotifaction;
    public GameObject PauseMenu;
    public GameObject EndGameMenu;
    public GameObject UIMain; 
    public Sprite[] Icons;

    private bool Update_UI = false;
    private bool mouseCursorOn = false;
    // Start is called before the first frame update
    void Start()
    {
        EndGameMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        #region Open and Close UI

        #region Shop and Inventory
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.B)) && mouseCursorOn == false)
        {
            GetComponentInChildren<BackPack_Controller>().SetButtons();
            GetComponentInChildren<BackPack_Controller>().UpdateBackpack();
            DisableMouse();
            Debug.Log("BackPack_Updated");
        }
        else if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.B)) && mouseCursorOn == true)
        {
            EnableMouse();
        }
        #endregion

        #region PauseMenu

        if (Input.GetKeyDown(KeyCode.Escape) && mouseCursorOn == false)
        {
            Time.timeScale = 0;
            PauseMenu.GetComponent<Animator>().SetTrigger("PauseMenuOpen");
            DisableMouse();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && mouseCursorOn == true)
        {
            Time.timeScale = 1;
            PauseMenu.GetComponent<Animator>().SetTrigger("PauseMenuClose");
            EnableMouse();
        }

        #endregion

        #endregion

    }

    public void UI_Notification(string action, string Item)
    {
        GameObject g = Instantiate(PickedUpNotifaction);
        g.transform.SetParent(transform);
        g.GetComponentInChildren<Text>().text = action + " " + "<color=#FFE100>"  + Item + "</color>";
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        PauseMenu.GetComponent<Animator>().SetTrigger("PauseMenuClose");
        EnableMouse();
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game_Menu"); 
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        DisableMouse();
        UIMain.SetActive(false); 
        EndGameMenu.SetActive(true); 

    }

    #region Disable/EnableMouse
    public void DisableMouse()
    {
        GameManager.instance.player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.SetCursorLock(false);

        if (GameManager.instance.player.GetComponent<Inventory>().equippedGun != null)
        {
            GameManager.instance.player.GetComponent<Inventory>().equippedGun.GetComponent<Weapon_Controller>().disableFire = true;
        }
        mouseCursorOn = true;
    }

    public void EnableMouse()
    {
        GameManager.instance.player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.SetCursorLock(true);

        if (GameManager.instance.player.GetComponent<Inventory>().equippedGun != null)
        {
            GameManager.instance.player.GetComponent<Inventory>().equippedGun.GetComponent<Weapon_Controller>().disableFire = false;
        }

        mouseCursorOn = false;
    }
    #endregion
}
