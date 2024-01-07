using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    public static GameManager2 instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 150;
    public int scorePerGreatNote = 200;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public TMP_Text scoreText;
    public TMP_Text multiplierText;

    public float totalNotes;
    public float averageNoteHits;
    public float goodNoteHits;
    public float greatNoteHits;
    public float missedNoteHits;

    public GameObject resultsScreen;
    public TMP_Text percentHitText, averageHitsText, goodHitsText, greatHitsText, missedHitsText, finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
            else
            {
                if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy) {
                    resultsScreen.SetActive(true);

                    averageHitsText.text = "" + averageNoteHits;
                    goodHitsText.text = goodNoteHits.ToString();
                    greatHitsText.text = greatNoteHits.ToString();
                    missedHitsText.text = "" + missedNoteHits;

                    float totalHit = averageNoteHits + goodNoteHits + greatNoteHits;
                    float percentHit = averageNoteHits + goodNoteHits + greatNoteHits;

                    percentHitText.text = percentHit.ToString("F1") + "%";

                    finalScoreText.text = currentScore.ToString();
                }
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time!");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiplierText.text = "Multiplier: x" + currentMultiplier;

        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        averageNoteHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodNoteHits++;
    }

    public void GreatHit()
    {
        currentScore += scorePerGreatNote * currentMultiplier;
        NoteHit();
        greatNoteHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed note...");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiplierText.text = "Multiplier: x" + currentMultiplier;
        missedNoteHits++;
    }
}
