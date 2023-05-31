

using System.Collections;
using System.Collections.Generic;
using TMPro;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enviromentScript : MonoBehaviour
{
    [SerializeField]
    GameObject plantPrefab;
    [SerializeField]
    private List<GameObject> plantList;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    TMP_InputField leafSize;
    [SerializeField]
    TMP_InputField leafColour;
    [SerializeField]
    TMP_InputField rootSize;
    [SerializeField]
    TMP_InputField cellWall;
    [SerializeField]
    Toggle storageRoot;
    [SerializeField]
    Button startSim;
    [SerializeField]
    Button addPlant;
    [SerializeField]
    GameObject planterUI;
    [SerializeField]
    GameObject growingUI;
    [SerializeField]
    Button reset;
    [SerializeField]
    Slider tempreture;
    [SerializeField]
    TMP_Text heatvalue;
    [SerializeField]
    Slider light;
    [SerializeField]
    TMP_Text lightValue;
    [SerializeField]
    Slider radiation;
    [SerializeField]
    TMP_Text radiationValue;
    // Start is called before the first frame update
    void Start()
    {
        addPlant.onClick.AddListener(addPlantButton);
        startSim.onClick.AddListener(startButton);
        reset.onClick.AddListener(restbutton);

        planterUI.SetActive(true);
        growingUI.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 markerPos=startPos.position;
        int coluom = 0;
        if (growingUI.activeSelf)
        {
            foreach (var plant in plantList)//looks at each plant
            {
                if (plant == null)
                {
                    plantList.Remove(plant);
                    break;
                }
                //creates a 5x5 grid of plants
                plant.transform.position = markerPos;
                coluom++;
                if (coluom == 5)
                {
                    markerPos.x = startPos.position.x;
                    markerPos.y -= 1;
                    coluom = 0;
                }
                else
                {
                    markerPos.x += 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //advance "turn" when space is pressed
                foreach (var plant in plantList)//looks at each plant
                {
                    var plantscript = plant.GetComponent<plantScript>();
                    //puts in the currently selected values for the turn;
                    plantscript.heatInput = (int)tempreture.value;
                    plantscript.lightInput = (int)light.value;

                    for(int i = 1; i <= 4; i++) { 
                    if (plantscript.isMature)//grabs another mature plant in the list to create an offspring if it is mature
                    {
                            foreach (var plant2 in plantList)
                            {
                                var plantscript2 = plant2.GetComponent<plantScript>();
                                if (plantscript2.isMature == true)
                                {
                                    //makes new plant from an average of both parants, with a roll chance for mutation                              
                                    int leafsize = (plantscript.leafSize + plantscript2.leafSize) / 2;

                                    if (Random.Range(0, 100) < radiation.value)
                                    {
                                        leafsize = Random.Range(0, 20);
                                    }

                                    int leafcolor = (plantscript.colourShade + plantscript2.colourShade) / 2;
                                    if (Random.Range(0, 100) < radiation.value)
                                    {
                                        leafcolor = Random.Range(0, 20);
                                    }
                                    int rootsize = (plantscript.rootSize + plantscript2.rootSize) / 2;
                                    if (Random.Range(0, 100) < radiation.value)
                                    {
                                        rootsize = Random.Range(0, 20);
                                    }
                                    int cellwall = (plantscript.cellWallStrength + plantscript2.cellWallStrength) / 2;
                                    if (Random.Range(0, 100) < radiation.value)
                                    {
                                        cellwall = Random.Range(0, 20);
                                    }
                                    bool storageroot;
                                    if (plantscript.storageRoot == true && plantscript2.storageRoot == true)
                                    {
                                        storageroot = true;
                                    }
                                    else if (plantscript.storageRoot == true && plantscript2.storageRoot == false)
                                    {
                                        if (Random.Range(0, 2) == 1)
                                        {
                                            storageroot = false;
                                        }
                                        else
                                        {
                                            storageroot = true;
                                        }
                                    }
                                    else if (plantscript.storageRoot == false && plantscript2.storageRoot == true)
                                    {
                                        if (Random.Range(0, 2) == 1)
                                        {
                                            storageroot = false;
                                        }
                                        else
                                        {
                                            storageroot = true;
                                        }
                                    }
                                    else
                                    {
                                        storageroot = false;
                                    }
                                    if (Random.Range(0, 100) < radiation.value)
                                    {
                                        if (Random.Range(0, 2) == 1)
                                        {
                                            storageroot = true;
                                        }
                                        else
                                        {
                                            storageroot = false;
                                        }
                                    }

                                    var newPlant = Instantiate(plantPrefab);
                                    newPlant.GetComponent<plantScript>().setValues(leafsize, leafcolor, rootsize, cellwall, storageroot);
                                    plantList.Add(newPlant);
                                }
                            }
                        }
                    }
                    //advances the plant growth
                    plantscript.advanceGrowth();
                        

                }
            }
            lightValue.text =light.value.ToString();
            heatvalue.text = tempreture.value.ToString();
            radiationValue.text = radiation.value.ToString();
        }
        
    }
    
    void startButton()
    {
        growingUI.SetActive(true);
        planterUI.SetActive(false);
    }
    void addPlantButton()
    {
        //creates a new plant prefab and adds it to the list
        var plant = Instantiate(plantPrefab);
        plant.GetComponent<plantScript>().setValues(int.Parse(leafSize.text), int.Parse(leafColour.text), int.Parse(rootSize.text), int.Parse(cellWall.text), storageRoot );
        plantList.Add(plant);
    }


    void restbutton()
    {

        SceneManager.LoadScene("SampleScene");
    }
}
