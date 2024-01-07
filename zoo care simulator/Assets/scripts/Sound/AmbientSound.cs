using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] AudioSource[] audioArray = new AudioSource[5];
    [Header("Sleep Time")]
    [SerializeField] int sleepTime;
    [Header("Assigned Sound")]
    [SerializeField] int soundToPlay;
    [Header("is Coroutine Done")]
    [SerializeField] bool isFinished;
    // Start is called before the first frame update
    void Start()
    {
        isFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFinished)
        {
            StartCoroutine(AmbientPlay());
        }
    }

    IEnumerator AmbientPlay()
    {
        
        isFinished = false;
        soundToPlay = Random.Range(0, 5);
        sleepTime = Random.Range(15,30);
        Debug.Log("Delay " + sleepTime);
        Debug.Log("Playing " + soundToPlay);
        audioArray[soundToPlay].Play();
        yield return new WaitForSeconds(sleepTime);
        isFinished= true;

    }
}
