using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HeartCount : MonoBehaviour
{
    public TextMeshProUGUI HeartText;
    public static int health;

    void Start()
    {
        health = 3;
    }

    private void Update()
    {
        if (health > 3)
            health = 3;

        switch (health)
        {
            case 3:
                health = 3;
                break;
            case 2:
                health = 2;
                break;
            case 1:
                health = 1;
                break;
            case 0:
                health = 0;
                break;
        }
    }
}