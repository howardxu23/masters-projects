using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text TimerDisplay;
    [SerializeField]
    GameObject bomb;
    [SerializeField]
    GameObject defuseNode;
    [SerializeField]
    GameObject unlockNode;

    [SerializeField]
    GameObject winScren;
    [SerializeField]
    GameObject LoseScreen;
    [SerializeField]
    Button resetButton;
    private float timer = 30;

    void Start()
    {
        winScren.SetActive(false);
        LoseScreen.SetActive(false);
        resetButton.onClick.AddListener(resetlevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (bomb.GetComponent<bombScript>().isPowered)//checks to see if bomb is powered, if so is instant loss
        {
            LoseScreen.SetActive(true);
        }
        if (unlockNode.GetComponent<EndNodeScript>().nodePowered == true)//checks to see if the unlock node is active, if so it is a win
        {
            winScren.SetActive(true);
        }
        if (defuseNode.GetComponent<EndNodeScript>().nodePowered != true && timer >=0)//timer pauses if defuse node is powered, runs when unpowered
        {
            timer -= 1 * Time.deltaTime;
        }
        if (timer <= 0)//game over when timer runs out
        {
            LoseScreen.SetActive(true);
        }
        //displays the timer
        int intTimer=(int)timer;
        TimerDisplay.text=intTimer.ToString();
        
        
    }
    void resetlevel()
    {
        SceneManager.LoadScene("bomb defuseal");
    }
}
