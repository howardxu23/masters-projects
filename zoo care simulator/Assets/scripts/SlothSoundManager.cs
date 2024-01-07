using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class SlothSoundManager: MonoBehaviour
{
    [SerializeField] AudioSource animalSound;



    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(soundPlay());
    }

    // Update is called once per frame
    void Update()
    {
 






    }


    private IEnumerator soundPlay()
    {
        //since we have spatial blended sound, all sounds will play at once, only one will be heard considering the spatial sound settings
        while (true)
        {
            yield return new WaitForSeconds(10);
            animalSound.Play();

        }
    }


}
