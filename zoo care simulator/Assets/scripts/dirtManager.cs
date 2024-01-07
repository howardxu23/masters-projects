using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class dirtManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Tooltip("put in assorted dirt prefabs here")]
    List<GameObject> dirtPrefabs;
    [SerializeField]
    int maxDirtAmount=4;
    [SerializeField]
    public bool enclosureClean = false;
    [Header("DEBUG")]
    [SerializeField]
    [Tooltip("assign the range coverage prefab as a child to the main gameobject to fill out irregular shaped enclosures. the varible would autofill by the script")]
    List<Transform> SpawnArea;
    [SerializeField]
    List<GameObject> dirtObjects;
    void Start()
    {
        SpawnArea=transform.Cast<Transform>().ToList();
        
    }

    // Update is called once per frame
    void Update()
    {

        int dirtlength=dirtObjects.Count;
        for (int i=0; i< dirtlength;i++)
        {//check for null values, then remove it
            if (dirtObjects[i] == null)
            {
                dirtObjects.Remove(dirtObjects[i]);
            }
        }
        if (dirtObjects.Count == 0)
        {
            enclosureClean = true;
        }
        else
        {
            enclosureClean= false;
        }
    }
    
    public void spawnDirt()//spawns dirt in an area specified by the areas in game
    {
        for(int i=0; i<= maxDirtAmount;i++)//how many dirt it should spawn
        {
            //which spawnzone it should go
            var spawnzone=SpawnArea[Random.Range(0,SpawnArea.Count)];
            //which dirt type it uses for the instance
            var dirtType=dirtPrefabs[Random.Range(0, dirtPrefabs.Count)];
            //get a random coodinate for the dirt item
            Vector3 origin= spawnzone.position;
            Vector3 range = spawnzone.localScale / 2.0f;
            Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
                                              0,
                                              Random.Range(-range.z, range.z));
            Vector3 dirtCoods=origin+ randomRange;

            //spawn the dirt in the targeted zone and position, and add one to the list of dirt
            var dirtobject=Instantiate(dirtType,dirtCoods,Quaternion.identity);
            dirtObjects.Add(dirtobject);
        }
    }
    
}
