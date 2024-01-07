using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIMoveTargetTest : MonoBehaviour
{
    [SerializeField] GameObject[] nodeArray;
    [SerializeField] Transform nodeTarget;
    [SerializeField] bool isNodeCol;
    [SerializeField] AudioSource animalSound;
    [SerializeField] Animator animalFeedAnim;
    [SerializeField] bool isEating;
    int randPenguinAnim;


    [SerializeField] int animalType;
    //animal types and thier numbers
    // 1 - panda
    // 2 - coati
    // 3 - penguin
    // 4 - meerkat
    //sloth isnt in here as it uses a different script.


    NavMeshAgent agent;
    Transform animalTransform;
    int nodeTargetNumber = 0;
    [SerializeField]float speed = 1.0f;
    Quaternion nodeRotQuat;
    int currentNodeChoice;
   
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animalTransform = GetComponent<Transform>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isNodeCol = false;
        StartCoroutine(changeNode());
        StartCoroutine(soundPlay());
    }

    // Update is called once per frame
    void Update()
    {
        while(!isEating)
        {
            nodeTarget = nodeArray[nodeTargetNumber].transform;

            agent.SetDestination(nodeTarget.position);
            nodeRotQuat = Quaternion.LookRotation(nodeTarget.transform.position - animalTransform.position);
            animalTransform.rotation = Quaternion.Slerp(animalTransform.rotation, nodeRotQuat, speed * Time.deltaTime);
            break;
        }
        

 

       

        




    }

    private IEnumerator changeNode()
    {
        while (true)
        {
            nodeTargetNumber = UnityEngine.Random.Range(0, 7);
            if (currentNodeChoice != nodeTargetNumber)
            {
                currentNodeChoice = nodeTargetNumber;
            }
            else if(currentNodeChoice == nodeTargetNumber)
            {
                nodeTargetNumber = UnityEngine.Random.Range(0, 7);
            }

            if(animalType == 1)
            {
                animalFeedAnim.Play("Armature|walkv0");
            }
            if (animalType == 2)
            {
                animalFeedAnim.Play("Armature|walk");
            }
            if (animalType == 3)
            {
                randPenguinAnim = Random.Range(0, 2);
                if(randPenguinAnim == 1)
                {
                    animalFeedAnim.Play("Armature|Jump");
                    yield return new WaitForSeconds(2);
                    animalFeedAnim.Play("Armature|slideidle");
                    yield return new WaitForSeconds(5);
                }
                else
                {
                    animalFeedAnim.Play("Armature|walk");
                    yield return new WaitForSeconds(5);
                }
                        

                
               

            }





            yield return new WaitForSeconds(7);
        }

    }

    private IEnumerator soundPlay()
    {
        //since we have spatial blended sound, all sounds will play at once, only one will be heard considering the spatial sound settings
        while(true)
        {
            yield return new WaitForSeconds(10);
            animalSound.Play();

        }
    }

    private void OnTriggerEnter(Collider col)

    {
        if(col.gameObject.tag == "Node")
        {
            nodeTargetNumber = UnityEngine.Random.Range(0, 7);
        }

    }

    public void startFeedAnim()
    {
        StopAllCoroutines();
        StartCoroutine(eatAnim());
        isEating = true;
    }
    private IEnumerator eatAnim()
    {
        if(animalFeedAnim != null)
        {
            if(animalType == 1)
            {
                animalFeedAnim.Play("Armature|headbob");
            }
            if (animalType == 2)
            {
                animalFeedAnim.Play("Armature|headbob");
            }

        }

        Debug.Log("animPlaying");
        yield return new WaitForSeconds(5);
        StartCoroutine(soundPlay());
        StartCoroutine(changeNode());
        isEating=false;

    }
}
