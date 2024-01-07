using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class pcController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pcCam;
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject playerCvs;
    [SerializeField] private GameObject screenSaver;
    [SerializeField] private GameObject bootUp;
    [SerializeField] private GameObject desktop;
    [SerializeField] private AudioSource startup, shutdown;
    private Coroutine zoomCoroutine;
    private float targetFOV = 20, startFOV = 60;
    private float counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        prompt.SetActive(false);
        screenSaver.SetActive(true);
        bootUp.SetActive(false);
        desktop.SetActive(false);
        pcCam.GetComponent<Camera>().enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        prompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        prompt.SetActive(false);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E)) //Enter the PC
            {
                counter = 0;
                playerCvs.SetActive(false);
                player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
                player.GetComponent<FirstPersonController>().enabled = false;
                player.GetComponent<ZoologistControl>().enabled = false;
                player.GetComponentInChildren<Camera>().enabled = false;
                pcCam.GetComponent<Camera>().enabled = true;
                screenSaver.SetActive(false);
                startup.Play();
                bootUp.SetActive(true);
                prompt.SetActive(false);
                if (zoomCoroutine != null)
                    StopCoroutine(zoomCoroutine);
                zoomCoroutine = StartCoroutine(ZoomIn(20.5f,1));
               
            }
            else if (Input.GetKey(KeyCode.R)) //Exit the pc -- will be a log out button in future
            {
                if (zoomCoroutine != null)
                    StopCoroutine(zoomCoroutine);
                pcCam.GetComponent<Camera>().fieldOfView = 60;
                playerCvs.SetActive(true);
                player.GetComponent<FirstPersonController>().enabled = true;
                player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(true);
                player.GetComponent<ZoologistControl>().enabled = true;
                player.GetComponentInChildren<Camera>().enabled = true;
                pcCam.GetComponent<Camera>().enabled = false;
                desktop.SetActive(false);
                shutdown.Play();
                screenSaver.SetActive(true);
                prompt.SetActive(true);
            }

        }
        
    }

    IEnumerator ZoomIn(float target, float duration) //https://stackoverflow.com/questions/47957484/zoom-camera-fov-overtime
    {
        while (pcCam.GetComponent<Camera>().fieldOfView>target)
        {
            counter += Time.deltaTime;
            float fOVTime = counter / duration;
            Debug.Log(fOVTime);

            //Change FOV
            pcCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(startFOV, targetFOV, fOVTime);
            if (fOVTime > 0.95)
            {
                bootUp.SetActive(false);
                desktop.SetActive(true);
            }
            //Wait for a frame
            yield return null;
        }
    }

    public void MeerkatButton()
    {
       // DontDestroyOnLoad(player);
     //   DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("FeedMeerkat2DMiniGame");
    }
    public void PandaButton()
    {
        //   DontDestroyOnLoad(player);
        player.SetActive(false);
        //     DontDestroyOnLoad(this.gameObject);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("panda dance");
    }
    public void PenguinButton()
    {
     //   DontDestroyOnLoad(player);
        player.SetActive(false);
    //    DontDestroyOnLoad(this.gameObject);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("PenguinSliding");
//        Debug.Log("PENGUIN GAME STARTED");
    }
    public void SlothButton()
    {
     //   DontDestroyOnLoad(player);
        player.SetActive(false);
   //     DontDestroyOnLoad(this.gameObject);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("SlothRacing");
    }
    public void CoatiButton()
    {
        //   DontDestroyOnLoad(player);
        player.SetActive(false);
        //     DontDestroyOnLoad(this.gameObject);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("CoatiMaze");
       
    }

}
