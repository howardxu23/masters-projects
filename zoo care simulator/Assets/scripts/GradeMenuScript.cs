using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GradeMenuScript : MonoBehaviour
{
    [SerializeField] private Image GradeValue;
    [SerializeField] private TMP_Text GradeComment;
    [SerializeField] private TMP_Text TasksCompleted;
    [SerializeField] private TMP_Text AvgHealth;
    [SerializeField] private Sprite[] PossibleGrades;
    [SerializeField] private AttendenceMachineScript AMScript;
    [SerializeField] private GameObject Wallet;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //string HealthGrade = AMScript.AverageHealthGrade;
        if (AMScript.AverageHealthGrade == "S")
        {
            GradeValue.sprite = PossibleGrades[0];
            GradeComment.text = "Super! Are you sure you're just an intern?";
            
        }
        else if (AMScript.AverageHealthGrade == "A")
        {
            GradeValue.sprite = PossibleGrades[1];
            GradeComment.text = "Amazing! You deserve a raise!";
            
        }
        else if (AMScript.AverageHealthGrade == "B")
        {
            GradeValue.sprite = PossibleGrades[2];
            GradeComment.text = "Bravo! Your love for animals is top-notch.";
            
        }
        else if (AMScript.AverageHealthGrade == "C")
        {
            GradeValue.sprite = PossibleGrades[3];
            GradeComment.text = "Congrats! However there's room for improvement.";
            
        }
        else if (AMScript.AverageHealthGrade == "D")
        {
            GradeValue.sprite = PossibleGrades[4];
            GradeComment.text = "Disappointing! The animals need better care than this.";
            
        }
        else if (AMScript.AverageHealthGrade == "E")
        {
            GradeValue.sprite = PossibleGrades[5];
            GradeComment.text = "Erm... I don't understand how you got hired.";
            
        }
        else if (AMScript.AverageHealthGrade == "F")
        {
            GradeValue.sprite = PossibleGrades[6];
            GradeComment.text = "F#@*$£#?! You're lucky we don't report you!";
        }

        
        TasksCompleted.text = AMScript.taskCount.ToString();
        AvgHealth.text = AMScript.AverageHealthScore.ToString("f1") + "%";
    }

    public void ContinueBtn()
    {
        DontDestroyOnLoad(Wallet);
        SceneManager.LoadScene("ShopUI");
    }
}
