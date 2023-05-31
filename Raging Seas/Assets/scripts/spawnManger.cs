using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawnManger : MonoBehaviour
{
    [SerializeField]
    int maxCoinsAmount;
    [SerializeField]
    GameObject spawnAreaGroup;
    [SerializeField]
    List<Transform> SpawnArea;
    [SerializeField]
    GameObject coinPrefab;
    [SerializeField]
    GameObject AIShipPrefab;
    [SerializeField]
    List<GameObject> coinObject;
    [SerializeField]
    List<GameObject> AIShips;
    [SerializeField]
    float respawnTimer=10;
    float timer;
    [Tooltip("if wave is cleared")]
    public bool waveClear = false;
    [Tooltip("starts the next wave")]
    public bool startWave = true;
    [Tooltip("how many ships are there in a wave")]
    public int waveSize = 15;
    // Start is called before the first frame update
    void Start()
    {
        SpawnArea = spawnAreaGroup.transform.Cast<Transform>().ToList();
        spawncoins();
        timer = respawnTimer;
        startWave = true;
        spawnWave(waveSize);
    }

    // Update is called once per frame
    void Update()
    {
        int coinlength = coinObject.Count;
        try
        {
            for (int i = 0; i < coinlength; i++)
            {//check for null values, then remove it
                if (coinObject[i] == null)
                {
                    coinObject.Remove(coinObject[i]);
                }
            }
        }
        catch
        {

        }
        waveClear = true;
        //checks if all AI ships are destroyed
        for(int i = 0; i < AIShips.Count; i++)
        {
            if (AIShips[i] != null)
            {
                waveClear = false;
            }
        }
        //spawns a wave when triggered and all AI ships are dead
        if (waveClear == true && startWave == true)
        {
            AIShips.Clear();
            spawnWave(waveSize);
            //startWave = false;
            waveClear = false;
        }
        //spawns new set of coins every timer reset
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            spawncoins();
            timer = respawnTimer;
        }
    }
    public void spawncoins()//spawns coins in an area specified by the areas in game
    {
        for (int i = 0; i <= maxCoinsAmount-coinObject.Count; i++)//how many coins it should spawn
        {
            //which spawnzone it should go
            var spawnzone = SpawnArea[Random.Range(0, SpawnArea.Count)];

            //get a random coodinate for the item
            Vector3 origin = spawnzone.position;
            Vector3 range = spawnzone.localScale / 2.0f;
            Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),                                           
                                              Random.Range(-range.y, range.y),
                                              0);
            Vector3 Coods = origin + randomRange;

            //spawn the coin in the targeted zone and position, and add one to the list of coins
            var coinobject = Instantiate(coinPrefab, Coods, Quaternion.identity);
            coinObject.Add(coinobject);
        }
    }
    public void spawnWave(int numAI)//spawns a wave of AI
    {
        for (int i = 0; i < numAI; i++)//spawns one AI in a area specified by spawnarea
        {
            var spawnzone = SpawnArea[Random.Range(0, SpawnArea.Count)];

            //get a random coodinate for the ships
            Vector3 origin = spawnzone.position;
            Vector3 range = spawnzone.localScale / 2.0f;
            Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
                                              Random.Range(-range.y, range.y),
                                              0);
            Vector3 Coods = origin + randomRange;
            //spawn the ships in the targeted zone and position, and add one to the list of ships
            var shipObject = Instantiate(AIShipPrefab, Coods, Quaternion.identity);
            AIShips.Add(shipObject);
        }
    }
}
