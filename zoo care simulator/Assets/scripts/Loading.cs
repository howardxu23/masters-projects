using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{
    [Header("Tip List")]
    [SerializeField] TMP_Text[] text = new TMP_Text[8];
    [Header("Black Panel Obj")]
    [SerializeField] Image panel;
    [Header("Fade Rate")]
    [SerializeField] float fadeRate = 1f;
    [Header("Starting Opacity")]
    [SerializeField] float startOpacity = 3f;
    [Header("Tip Choice")]
    int tipSelection;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 8; i++)
        {
            text[i].enabled = false;
        }
        tipSelection = Random.Range(1, 8);
        text[tipSelection].enabled = true;
        StartCoroutine(loadSequence());
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("fadeOut", 0f, 1f);
    }

    IEnumerator loadSequence()
    {
        yield return new WaitForSeconds(6.8f);
        SceneManager.LoadScene("Nmesh-DemoTwo");
    }
    void fadeOut()
    {

            startOpacity = startOpacity - fadeRate;
            //UnityEngine.Debug.Log(panel.color);
            panel.color = new Vector4(0, 0, 0, startOpacity);
        

    }
}
