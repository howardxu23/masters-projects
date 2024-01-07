using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPost : MonoBehaviour
{
    public GameObject winText;
    int winTime = 0;
    
    void OnTriggerEnter2D(Collider2D colInfo)
    {
        if (colInfo.CompareTag("Penguin_Base"))
        {
            Debug.Log("You win!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // Set text on
            winTime = 170;
        }
    }

    void Update()
    {
        if (winTime >= 1)
        {
            winTime--;
            winText.SetActive(true);
        }
        // else
        else if (winTime <= 0)
        {
            winText.SetActive(false);
        }
    }
}
