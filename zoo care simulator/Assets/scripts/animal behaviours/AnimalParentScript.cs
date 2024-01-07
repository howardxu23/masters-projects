
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalParentScript : MonoBehaviour
{
    [Tooltip("Displays in the notepad as the name")]public string animalName;
    [SerializeField]
    protected int maxValue=100;
    public int health = 50;
    public int mood = 50;
    public int hunger=50;
    public int currenthealth;
    public int currenthappiness;
    public int currenthunger;
    [SerializeField]
    private int dirtinessValue;
    public float growthRate;
    public float age;
    public bool isDead = false;

    private int daysWithoutFood=0;
    private int daysWithoutPlaytime = 0;
    [SerializeField]
    [Tooltip("aliments for matching with the correct medicene")]
    protected string[] alimentsList ={"flu","rash"};
    public string CurrentAliment="healthy";

    [SerializeField]
    [Tooltip("what class of food can the animal eat. animals will outright not eat types outside of this class")]
    private string[] FoodTypes;
    [SerializeField]
    [Tooltip("what the animal preferes to eat, and gains mood from the food's happiness value")]
    private string[] PreferredFood;

    [Tooltip("for what the animal recives from the player")]
    public GameObject animalInventory;
    [SerializeField]
    [Tooltip("toy drop location for when it is finished playing/is playing")]
    protected Transform animalHand;

    [SerializeField]
    protected bool IsPlayingToy=false;
    [SerializeField]
    [Tooltip("play animation in seconds")]
    protected float playAimationDuration = 5;
    private string healthy = "healthy";
    [SerializeField]
    [Tooltip("lower bound for the stats when generation")]
    private int lowerBoundGenerationModifier=50;
    [SerializeField]
    [Tooltip("tick rate of how often processes update in seconds[DEPRECATED]")]
    private float tickRate = 1;
    private float timer = 0;

    [SerializeField]
    [Tooltip("how much hunger decreases by per day pass")]
    private int hungerDecayRate = 1;
    [SerializeField]
    [Tooltip("how much mood decreases by per day pass")]
    private int moodDecayRate = 1;
    [SerializeField]
    [Tooltip("decay ramps copies from decay rates, no need to set")]
    private int hungerDecayRamp;
    [SerializeField]
    [Tooltip("decay ramps copies from decay rates, no need to set")]
    private int moodDecayRamp;
    [Tooltip("how much health decreases by per day pass if afflicted or low mood/hunger")]
    private int healthDecayRate = 1;
    [SerializeField]
    [Tooltip("increased rate of  +health decay when dirty")]
    private int dirtyIncreasedModifier=5;
    [SerializeField]
    [Tooltip("how much health increases by per day pass when hunger and mood is high")]
    private int healthRestoreRate = 1;
    [SerializeField]
    [Tooltip("what is the upper limit for both hunger and mood before health starts to regenerate")]
    private int HealthRegenPercentage = 75;
    [SerializeField]
    [Tooltip("maximun chance out of 100% to become afflicted at 0 hunger and 0 happiness")]
    private float maxChanceToGetAfflicted = 70;
    [SerializeField]
    [Tooltip("DEBUGGING")]
    private float currentHappinessAfflictedChance;
    [SerializeField]
    [Tooltip("DEBUGGING")]
    private float currenthungerAfflictedChance;
    [SerializeField] private StatBars statBars;

    [SerializeField]
    [Tooltip("Navmesh Script We Need to change Coroutine From")]
    AIMoveTargetTest navMeshScript;
    [SerializeField]
    GameObject dirtCloud;
    [SerializeField]
    GameObject virusCloud;

    [SerializeField] private ParticleSystem emotion;
    [SerializeField] private ParticleSystemRenderer reactionChange;
    [SerializeField] private Material[] reactionType;
    [SerializeField] private AudioSource toySound;

    public bool Played=false;
    public bool Fed=false;
    public bool Cured = false;
    public bool Clean = false;

    
    private void Start()
    {
        currenthealth = maxValue;
        statBars.SetMaxHealth(maxValue);
        
        currenthunger = maxValue;
        statBars.SetMaxHunger(maxValue);
        
        currenthappiness = maxValue;
        statBars.SetHappiness(maxValue);
        becomeDirty();
        
    }
    private void Awake()
    {
        hungerDecayRamp = hungerDecayRate;
        moodDecayRamp = moodDecayRate;
        dirtCloud.SetActive(false);
        virusCloud.SetActive(false);
        // print("decay ramp " + hungerDecayRamp + moodDecayRamp);
    }
    public void resetneeds()//resets on day
    {
        AdvanceTimeStatus();
        Played = false;
        Fed = false;
        becomeDirty();
        
    }
    public void Feed(foodScript food)//increases hunger bar from feeding
    {
        print(food.getFoodName());
        //checks for favourite food first
        for (int i=0;i<PreferredFood.Length;i++)
        {
            if (food.getFoodName() == PreferredFood[i])
            {
                //plays feed anim from animal navmesh script
                if (navMeshScript != null)
                {
                    navMeshScript.startFeedAnim();
                }
                //if it matches eats the food and becomes happier
                hunger += food.saturationRestore;
                mood += food.HappinesRestore;
                //emotion
                reactionChange.material = reactionType[7];
                emotion.Play();
                //eats the food
                Destroy(animalInventory);
                Fed = true;
                return;

            }
            
            else if (food.getFoodType() == FoodTypes[i])
            {
                //plays feed anim from animal navmesh script
                if (navMeshScript != null)
                {
                    navMeshScript.startFeedAnim();
                }
                //if it matches the type it eats it.
                hunger += food.saturationRestore;
                Destroy(animalInventory);
                Fed= true;
                reactionChange.material = reactionType[1];
                emotion.Play();
                return;
            
            }

        }
        //drops the food if neither matches
        animalInventory.GetComponent<Rigidbody>().velocity = Vector3.zero;
        animalInventory.gameObject.transform.position = animalHand.transform.position;
        animalInventory = null;
        reactionChange.material = reactionType[3];
        emotion.Play();
        
        
    }
   
    public void EatMedicine(mediceneScript medicene)//input what the medicene cures and it increases health
    {
        int healthmodifier = medicene.HealthAdded;
        int hungermodifier = medicene.HungerAdded;
        int moodmodifier = medicene.HappinessAdded;
        animalInventory.GetComponent<Rigidbody>().velocity = Vector3.zero;
        animalInventory.gameObject.transform.position = animalHand.transform.position;

        if (CurrentAliment != healthy)//medicene does nothing if the animal is healthy
        {
            if (medicene.GetAliment() == CurrentAliment)//If correct medicene used cures the aliment and icreases health
            {
                health += healthmodifier;
                //side effects
                hunger+= hungermodifier;
                mood += moodmodifier;
                CurrentAliment = healthy;
                Cured= true;
                virusCloud.SetActive(false);
                reactionChange.material = reactionType[0];
                emotion.Play();
            }
            else//if wrong medicene used it reduces health instead and aliment stays, but still adds side effects.
            {
                health -= healthmodifier;
                //side effects
                hunger += hungermodifier;
                mood += moodmodifier;
                reactionChange.material = reactionType[5];
                emotion.Play();
            }
        }
        else//if wrong medicene used it reduces health instead and aliment stays, but still adds side effects. this allows for stuff like anti-depressents
        {
            health -= healthmodifier;
            //side effects
            hunger += hungermodifier;
            mood += moodmodifier;
            reactionChange.material = reactionType[5];
            emotion.Play();
        }
        Destroy(animalInventory);//eats the medicene for all cases
    }

     protected IEnumerator playingWithToy(float duration)//triggers once when animal recives toy
    {
        //stops duplication
        if (IsPlayingToy==true)
        {
            yield break;
        }
        IsPlayingToy = true;
        toySound.Play();
        //improves mood
        print("add "+mood+" mood");
        mood += 10;
        yield return new WaitForSeconds(duration);
        IsPlayingToy= false;
        animalInventory.GetComponent<Rigidbody>().velocity = Vector3.zero;
        animalInventory = null;
        Played = true;
    }
    virtual protected void PlayToyAnimation()//for the animal's play animation
    {
        if(IsPlayingToy== true)
        {
            //placeholder animation
            transform.Rotate(0, 50 * Time.deltaTime, 0);
            //moves the toy to the animal's position
            animalInventory.gameObject.transform.position = animalHand.transform.position;
            reactionChange.material = reactionType[2];
            emotion.Play();
        }
    }
    public void becomeDirty()//makes the animal dirty on day reset
    {

        if (Clean == true)
        {
            dirtinessValue = Random.Range(0, 6);
            print(dirtinessValue + " dirtness");
            dirtCloud.SetActive(true);
        }
    }
    public void clean()//cleans the animal
    {
        print("clean");
        if (dirtinessValue >= 0)
        {
            print("clean 2");
            reactionChange.material = reactionType[6];
            emotion.Play();
            dirtinessValue -= 1;
        }
    }
    virtual public void Update()//make sure to call override on all child methods and that it has a base.Update()
    {

        //check animal inventory
        
        if (animalInventory != null)
        {

            if (animalInventory.tag == "toy")
            {
                StartCoroutine(playingWithToy(playAimationDuration));
                PlayToyAnimation();
                
            }
            else if (animalInventory.tag == "medicene")
            {
                var mediceneContents = animalInventory.GetComponent<mediceneScript>();
                EatMedicine(mediceneContents);
            }
            else if (animalInventory.tag == "food")
            {
                
                var FoodContents=animalInventory.GetComponent<foodScript>();
                Feed(FoodContents);
            }
        }
        timer-= Time.deltaTime;
        if (timer < 0)
        {
            //AdvanceTimeStatus();
            timer = tickRate;
            //print("tick "+gameObject.name);
        }
        //clean checker 
        if (dirtinessValue <= 0)
        {
            Clean=true;
            dirtCloud.SetActive(false);
            
        }
        else
        {
            Clean= false;
            
        }
    }
    /// <summary>
    /// for generating random stats upon spawning the animal into the game, put it in start method
    /// </summary>
    protected void generateRandomInitalStats()
    {
        health=UnityEngine.Random.Range(lowerBoundGenerationModifier, 100);
        mood = UnityEngine.Random.Range(lowerBoundGenerationModifier, 100);
        //hunger = Random.Range(lowerBoundModifier, 100);
        hunger = maxValue;
    }
    protected void DecayHunger()
    {
        if (hunger > 1)
        {
            hunger = hunger- hungerDecayRate;
            //if not fed previously it would cause it to decay faster exponentially
            if (Fed == false)
            {
                hunger -= hungerDecayRamp;
                hungerDecayRamp = hungerDecayRamp * 2;
            }
            if(Fed==true)//if have been fed before resets hunger decay ramp and not add it.
            {
                hungerDecayRamp = hungerDecayRate;
            }
            statBars.SetHunger(hunger);

            
        }
        if (hunger < 1)
        {
            hunger = 1;
            reactionChange.material = reactionType[3];
            emotion.Play();
        }
    }
    protected void decayHappiness()
    {
        if (mood > 1)
        {
            mood = mood -  moodDecayRate;
            if (Clean == false)//removes more happiness when dirty
            {
                mood -= dirtyIncreasedModifier;
            }
            if (Played == false)
            {
                mood -= moodDecayRamp;
                moodDecayRamp = moodDecayRamp * 2;
            }
            else if (Played == true)//resets it once it has been played
            {
                moodDecayRamp = moodDecayRate;
            }
            statBars.SetHappiness(mood);
        }
        if (mood < 1)
        {
            mood = 1;
            reactionChange.material = reactionType[3];
            emotion.Play();
            
        }
    }
    protected void decayHealth()
    {
        if (health > 0)
        {
            health = health- healthDecayRate;
            statBars.SetHealth(health);
            if (Clean == false)
            {
                health -= dirtyIncreasedModifier;
            }
        }
        if (health <= 0)//animal dies
        {
            isDead= true;
            var main = emotion.main;
            main.loop = true;
            reactionChange.material = reactionType[4];
            emotion.Play();
        }
    }
    protected void BecomeAfflicted()
    {
        if (CurrentAliment == "healthy")
        {
            int length = alimentsList.Length;
            int index=UnityEngine.Random.Range(0, length);
            CurrentAliment= alimentsList[index];
            virusCloud.SetActive(true);
        }
    }
    protected void restoreHealth()
    {
        if (health <= 100)
        {
            health += healthRestoreRate;
        }
    }

    protected void growOlder()
    {
        age += growthRate;
    }
    virtual protected void AdvanceTimeStatus()//logic for the base stats
    {
        //hunger and happiness goes down all the time
        DecayHunger();
        decayHappiness();
        growOlder();

        //the lower the hunger and happiness, the higher the risk of catching an infection, based on maxChanceToGetIll%/2 x hunger + maxChanceToGetIll%/2 x happiness

        //liner decay to 0
        //currentHappinessAfflictedChance = maxChanceToGetAfflicted / 2-((maxChanceToGetAfflicted / 100) / 2) * mood;      
        //currenthungerAfflictedChance = maxChanceToGetAfflicted / 2-((maxChanceToGetAfflicted / 100) / 2) * hunger;

        //exponential decay to 0
        //                                                            steepness     
        currentHappinessAfflictedChance =  ((maxChanceToGetAfflicted / 2 / 100) ) * (   2    /mood)  *100;
        currenthungerAfflictedChance =  ((maxChanceToGetAfflicted / 2 / 100) ) *    (   2    /hunger)  *100;
        //locks the values within lower bounds
        if (currentHappinessAfflictedChance <= 0)
        {
            currentHappinessAfflictedChance = 0;
        }
        if(currenthungerAfflictedChance <= 0)
        {
            currenthungerAfflictedChance = 0;
        }
        int currentAfflictedChance = (int)(currenthungerAfflictedChance + currentHappinessAfflictedChance);
        //locks value within upper bounds
        if (currentAfflictedChance > 100) {
            currentAfflictedChance = 100;
        }
        
        //print(currentIllChance);
        int randomNumber=UnityEngine.Random.Range(0, maxValue);
        //will animal get afflicted
        //requires fizing later
        if (randomNumber <= currentAfflictedChance)
        {
            BecomeAfflicted();
        }
        //decay health if ill
        if (CurrentAliment != healthy)
        {
            decayHealth();
        }
        else if (CurrentAliment == healthy)//restores health if healthy and if hunger and mood is above a certain value
        {
            if (mood >= HealthRegenPercentage && hunger >= HealthRegenPercentage)
            {
                restoreHealth();
            }
        }

    }
}
