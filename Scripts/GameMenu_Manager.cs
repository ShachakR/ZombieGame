using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameMenu_Manager : MonoBehaviour
{

    [Header("OBJECTS")]
    public Transform currentMount;
    public Camera camera;
    public Text mapSelectTxt; 

    [Header("SETTINGS")]
    [Tooltip("Set 1.1 for instant fly")]
    [Range(0.01f, 1.1f)] public float speed = 0.1f;
    public float zoom = 1.0f;

    private Vector3 lastPosition;
    private string LoadSceneName;
    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentMount.position, speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speed);
        camera.fieldOfView = 60 + zoom;
        lastPosition = transform.position;
    }

    public void setMount(Transform newMount)
    {
        currentMount = newMount;
    }

    public void SetQaulity(Dropdown dropdown)
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        Debug.Log("Changed Qaulity Setting"); 
    }

    public void SetResolution(Dropdown dropdown)
    {
        switch (dropdown.value) {
            case 0:
                Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(1366, 768, FullScreenMode.FullScreenWindow);
                break;
            case 2:
                Screen.SetResolution(1600, 900, FullScreenMode.FullScreenWindow);
                break;
            case 3:
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                break;
        }
        Debug.Log("Changed Resolution Setting");
    }

    public void SetVolume(Slider slider)
    {
        AudioListener.volume = slider.value; 
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(LoadSceneName, LoadSceneMode.Single); 
    }

    public void SetScene(string name)
    {
        LoadSceneName = name;
        mapSelectTxt.text = name;
    }

    public void Quit()
    {
        Application.Quit(); 
    }
}
