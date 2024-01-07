using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Meerkat> meerkats;

    [Header("UI objects")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject outOfTimeText;
    [SerializeField] private GameObject penguinText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    // Hardcoded variables you may want to tune.
    private float startingTime = 30f;

    // Global variables
    private float timeRemaining;
    private HashSet<Meerkat> currentMeerkats = new HashSet<Meerkat>();
    private int score;
    private bool playing = false;

    // This is public so the play button can see it.
    public void StartGame()
    {
        // Hide/show the UI elements we don't/do want to see.
        playButton.SetActive(false);
        outOfTimeText.SetActive(false);
        penguinText.SetActive(false);
        gameUI.SetActive(true);
        // Hide all the visible meerkats.
        for (int i = 0; i < meerkats.Count; i++)
        {
            meerkats[i].Hide();
            meerkats[i].SetIndex(i);
        }
        // Remove any old game state.
        currentMeerkats.Clear();
        // Start with 30 seconds.
        timeRemaining = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;
    }

    public void GameOver(int type)
    {
        // Show the message.
        if (type == 0)
        {
            outOfTimeText.SetActive(true);
        }
        else
        {
            penguinText.SetActive(true);
        }
        // Hide all meerkats.
        foreach (Meerkat meerkat in meerkats)
        {
            meerkat.StopGame();
        }
        // Stop the game and show the start UI.
        playing = false;
        playButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            // Update time.
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(0);
            }
            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
            // Check if we need to start any more animals
            if (currentMeerkats.Count <= (score / 10))
            {
                // Choose a random Animal
                int index = Random.Range(0, meerkats.Count);
                // Doesn't matter if it's already doing something, we'll just try again next frame.
                if (!currentMeerkats.Contains(meerkats[index]))
                {
                    currentMeerkats.Add(meerkats[index]);
                    meerkats[index].Activate(score / 10);
                }
            }
        }
    }

    public void AddScore(int meerkatIndex)
    {
        // Add and update score.
        score += 1;
        scoreText.text = $"{score}";
        // Increase time by a little bit.
        timeRemaining += 1;
        // Remove from active meerkats.
        currentMeerkats.Remove(meerkats[meerkatIndex]);
    }

    public void Missed(int meerkatIndex, bool isMeerkat)
    {
        if (isMeerkat)
        {
            // Decrease time by a little bit.
            timeRemaining -= 2;
        }
        // Remove from active meerkats
        currentMeerkats.Remove(meerkats[meerkatIndex]);
    }
}
