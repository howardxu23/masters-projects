using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class HandbookController : MonoBehaviour
{
    [SerializeField] private GameObject handbookCanvas;
    [SerializeField] private FirstPersonController fpsScript;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private AudioSource bOpen, bClose;
    // Start is called before the first frame update
    void Start()
    {
        handbookCanvas.SetActive(false);
        hotbar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        OpenHandbook();
    }

    private void OpenHandbook()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            handbookCanvas.SetActive(!handbookCanvas.activeInHierarchy);
            hotbar.SetActive(!hotbar.activeInHierarchy);
            fpsScript.m_MouseLook.SetCursorLock(!handbookCanvas.activeInHierarchy);
            fpsScript.enabled = !handbookCanvas.activeInHierarchy;

            if (handbookCanvas.activeInHierarchy)
            {
                bOpen.Play();
            }
            else if(!handbookCanvas.activeInHierarchy)
            {
                bClose.Play();
            }

        }
    }
}
