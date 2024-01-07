using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    private void Start()
    {
        //gameObject.SetActive(false);
    }

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}