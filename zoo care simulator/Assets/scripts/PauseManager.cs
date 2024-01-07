using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject PausePrimary;
    [SerializeField] private GameObject PauseHelp;
    [SerializeField] private GameObject PauseOptions;
    [SerializeField] private GameObject PlayerCanvases;
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private AsyncLoadHandling AsyncLoad;
    private Resolution[] resolutions;
    
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        PausePrimary.SetActive(false);
        PauseHelp.SetActive(false);
        PauseOptions.SetActive(false);
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }
    
    private void TogglePauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeInHierarchy);
        PausePrimary.SetActive(PauseMenu.activeInHierarchy);
        PauseHelp.SetActive(false);
        PauseOptions.SetActive(false);
        fpsController.m_MouseLook.SetCursorLock(!PauseMenu.activeInHierarchy);
        fpsController.enabled = !PauseMenu.activeInHierarchy;
        PlayerCanvases.SetActive(!PlayerCanvases.activeInHierarchy);
    }

    
    public void Restart()
    {
        Debug.Log("Restart Pressed");
    }
    public void Help()
    {
        Debug.Log("Help Pressed");
        PausePrimary.SetActive(false);
        PauseHelp.SetActive(true);
        
    }
    public void Options()
    {
        Debug.Log("Options Pressed");
        PausePrimary.SetActive(false);
        PauseOptions.SetActive(true);
    }
    public void MainMenu()
    {
        AsyncLoad.LoadScene(0);
        Debug.Log("MainMenu Pressed");
    }

    public void Back()
    {
       PauseHelp.SetActive(false);
       PauseOptions.SetActive(false);
       PausePrimary.SetActive(true);
    }

    public void SetVolume(float volume) //www.youtube.com/watch?v=YOaYQrN1oYQ&t=301s
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullsccreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
