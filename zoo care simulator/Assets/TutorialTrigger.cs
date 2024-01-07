using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private AudioSource phoneCall;
    public bool answered = false;
    private TutorialManager TManager;
    private Vector3 phonePos;

    private void Start()
    {
        prompt.SetActive(true);
        tutorialCanvas.SetActive(false);
        TManager = GetComponent<TutorialManager>();
        phonePos = gameObject.transform.position;

    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            answered = true;
            GetComponent<SphereCollider>().radius = 0.1f;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (answered)
        {
            TriggerTutorial();
        }
        else
        {
            prompt.SetActive(false);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (answered == false)
        {
            prompt.SetActive(true);
        }
    }

    public void TriggerTutorial()
    {
        if (answered)
        {
            prompt.SetActive(false);
            phoneCall.Stop();
            tutorialCanvas.SetActive(true);
            TManager.StartTutorial(dialogue);
        }
        
    }
}
