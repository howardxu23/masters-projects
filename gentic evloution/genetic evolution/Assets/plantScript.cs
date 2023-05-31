using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantScript : MonoBehaviour
{
    public int lightInput;
    public int heatInput;
    

    public int leafSize=1;
    public int cellWallStrength=1;
    public int colourShade;
    public int rootSize;
    public bool storageRoot;
    public int storedEnergy;
    [SerializeField]
    private int maxValue = 20;
    public float EatenChance;
    [SerializeField]
    private float deathChance;
    [SerializeField]
    
    private int energy=0;
    [SerializeField]
    private int timeToMature = 1;
    [SerializeField]
    private int TimeInMaturity = 1;
    [SerializeField]
    public int currentGrowthStage=0;
    public float mutationChance;
    
    public int growDuration = 1;
    public Color growing = Color.yellow;
    public Color mature = Color.green;
    public Color dorment = Color.cyan;

    public bool isMature = false;
    private bool isDorment = false;
    // Update is called once per frame
    private void Awake()//plants the plant
    {
        gameObject.GetComponent<SpriteRenderer>().color = growing;
        //cell wall strength increases time in maturity, allowing for more offspring rolls, but takes longer to mature.
        TimeInMaturity += cellWallStrength - 1;
        timeToMature += cellWallStrength;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            advanceGrowth();
        }
    }
    public void setValues(int leafsize, int colorshade, int rootsize, int cellwallstrength, bool storageroot)
    {
        leafSize= leafsize;
        colourShade= colorshade;
        rootSize= rootsize;
        cellWallStrength= cellwallstrength;
        storageRoot = storageroot;
    }

    public void advanceGrowth()//advances the turn growth
    {
        //cell wall strengh increases the time it stays in maturity for more offspring, in exchange for a longer inital growth
        //large leaf size increases energy aquired per turn, but larger leaves increases the chance of being eaten
        //large root networks improves surival in dry enviroments, but increases chance of death in wetter enviroments
        //having a storage root allows skipping the growth stage and allows for surival after "death cases" after maturity, but needs stored energy from the leaves. if it reaches 0 it dies.
        //the higher the colour shade, the lighter the leaves, meaning that they can surive high tempretures better, but is less efficent at converting energy.

        //when passing down the genes it will take half of 2 random mature parents in the field
        //mutation is random ivrelent of the all other conditions when passing down the genes

        //rolls the eaten/death chance first
        float death=Random.Range(0f, 100f);
        float eaten=Random.Range(0f,100f);

        if (isDorment == false)
        {
            if (currentGrowthStage > (TimeInMaturity + timeToMature))
            {
                //dies from old age
                Destroy(gameObject);
            }
            if (death <= deathChance || eaten <= EatenChance )
            {
                if (storageRoot == true)
                {
                    //if there is a storage root minus 10 energy instead of dying and go into dorment for 1 turn
                    isDorment = true;
                    gameObject.GetComponent<SpriteRenderer>().color = dorment;
                    storedEnergy -= 5;
                    if (storedEnergy < 0)
                    {
                        //if it runs out of energy it dies
                        Destroy(gameObject);
                    }
                    //stops all other actions when dorment
                    return;
                }
                else
                {
                    //removes itself if eaten, killed off or dies of old age
                    Destroy(gameObject);
                }
            }


            //when age is mature it sets it to be able to produce offspring
            if (currentGrowthStage >= timeToMature)
            {
                isMature = true;
                gameObject.GetComponent<SpriteRenderer>().color = mature;
            }
            //gains energy in terms of size of leaf -colour(higher means lighter)       
            energy = lightInput + (leafSize - colourShade);

            //energy is expended on cell wall and root size per round for the growth
            energy -= (cellWallStrength + rootSize);
            //if energy expenditure is greater then the gain then it immediatly dies
            if (energy < 0)
            {
                Destroy(gameObject);
            }
            //if there is a storage root it stores any remaining energy in it, otherwise any remaining is lost
            if (storageRoot == true)
            {
                storedEnergy += energy;
            }
            //caculates the death chance based on heat, leaf size and colour and root size
            deathChance = heatInput - colourShade - rootSize + leafSize;
            EatenChance = (float)leafSize / 4;
            
        }
        else
        {
            isDorment = false;//reactivates growth after a turn of dormancy
            gameObject.GetComponent<SpriteRenderer>().color = growing;
        }
        //advances growth stage
        currentGrowthStage += 1;
    }
}
