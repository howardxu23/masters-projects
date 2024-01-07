using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.SearchService;

public class AsyncLoadHandling : MonoBehaviour
{
    [Header("Screen Obj")]
    [SerializeField] GameObject loadingScreen;
    [Header("Main Menu Obj")]
    [SerializeField] GameObject menuScreen;
    [Header("Loading Bar")]
    [SerializeField] Slider loadBarFill;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int sceneID)
    {
        loadingScreen.SetActive(true);
        menuScreen.SetActive(false);

        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneID);

        

        while(!loadOperation.isDone)
        {
            float progressVal = Mathf.Clamp01(loadOperation.progress);
            UnityEngine.Debug.Log(loadOperation.progress);

            loadBarFill.value= progressVal;

            yield return null;
        }
    }
}
