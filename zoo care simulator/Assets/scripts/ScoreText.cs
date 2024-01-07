using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI PointText;
    public static int points;
    public GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PointText.text = points.ToString();
        if (points == 6375)
        {
            Debug.Log("You Win!");
            winScreen.SetActive(true);
        }
    }
}
