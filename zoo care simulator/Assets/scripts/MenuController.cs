using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject firstPrompt;
    [SerializeField] private KeyCode PressToPlay;
    [SerializeField] private GameObject FirstMenu;
    [SerializeField] private GameObject FirstMenuWarp;
    [SerializeField] private GameObject PlayMenu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject HelpMenu;
    [SerializeField] private GameObject StoryMenu;
    [SerializeField] private AsyncLoadHandling AsyncLoad;
    private Coroutine warpCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        firstPrompt.SetActive(false);
        FirstMenu.SetActive(false);
        PlayMenu.SetActive(false);
        StoryMenu.SetActive(false);
        FirstMenuWarp.SetActive(false);
        SettingsMenu.SetActive(false);
        HelpMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PressToPlay))
        {
            Anim.SetTrigger("GameStart");
            firstPrompt.SetActive(false);
        }  
    }
    
    public void AlertObservers(string message)
    {
        if (message.Equals("IntroAnimationEnded"))
        {
            firstPrompt.SetActive(true);
        }
    }

    public void ShowFirstMenu(string message)
    {
        if (message.Equals("TravelAnimationEnded"))
        {
            if (warpCoroutine != null)
                StopCoroutine(warpCoroutine);
            warpCoroutine = StartCoroutine(WarpEffect());
        }
    }

    IEnumerator WarpEffect()
    {
        FirstMenuWarp.SetActive(true);
        yield return new WaitForSeconds(1f);
        FirstMenu.SetActive(true);
        FirstMenuWarp.SetActive(false);
        yield return null; 
    }
    
    public void ShowPlayMenu(string message)
    {
        if (message.Equals("TravelAnimationEnded"))
        {
            PlayMenu.SetActive(true);
        }
    }

    public void StartTutorial(string message)
    {
        if (message.Equals("TravelAnimationEnded"))
        {
            //SceneManager.LoadScene("Loading");
            AsyncLoad.LoadScene(4);
        }
    }
    
    public void ShowStoryMenu(string message)
    {
        if (message.Equals("TravelAnimationEnded"))
        {
            StoryMenu.SetActive(true);
        }
    }

    public void ShowSettingsMenu(string message)
    {
        if (message.Equals("TravelAnimationEnded"))
        {
            SettingsMenu.SetActive(true);
        }
    }

    public void ShowHelpMenu(string message)
    {
        if (message.Equals("TravelAnimationEnded"))
        {
            HelpMenu.SetActive(true);
        }
    }

    public void Fade2Game(string message)
    {
        if (message.Equals("TravelAnimationEnded"))
        {
            //SceneManager.LoadScene("Loading");
            AsyncLoad.LoadScene(4);
        }
    }
    
    public void PlayButton()
    {
        Anim.SetTrigger("PressPlay");
        StopCoroutine(warpCoroutine);
    }

    public void PlayReverse()
    {
        Anim.SetTrigger("PressPlayBack");
        PlayMenu.SetActive(false);
        if (warpCoroutine != null)
            StopCoroutine(warpCoroutine);
        warpCoroutine = StartCoroutine(WarpEffect());
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void TutorialButton()
    {
        Anim.SetTrigger("PressTutorial");
    }
    
    public void StoryButton()
    {
        Anim.SetTrigger("PressStory");
    }
    
    public void StoryReverse()
    {
        Anim.SetTrigger("PressStoryReverse");
        PlayMenu.SetActive(true);
    }

    public void SettingsButton()
    {
        Anim.SetTrigger("PressSettings");
        FirstMenu.SetActive(false);
    }

    public void SettingsReverse()
    {
        Anim.SetTrigger("PressOptionsBack");
        FirstMenu.SetActive(true);
        HelpMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    public void HelpButton()
    {
        Anim.SetTrigger("PressHelp");
        SettingsMenu.SetActive(false);
    }

    public void HelpReverse()
    {
        Anim.SetTrigger("PressHelpBack");
        SettingsMenu.SetActive(true);
    }

    public void PlayGame()
    {
        Anim.SetTrigger("PlayGame");
    }
}
