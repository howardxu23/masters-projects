using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AttendenceMachineScript : MonoBehaviour
{
    //script to caculate the grades of how many tasks are completed, the average health of animals,
    //and the clock out which would reset the tasks for the day
    [SerializeField]
    [Tooltip("put the chalkboards that are assigned to each enclosure here")]
    private GameObject[] TaskBoards;
    [SerializeField]
    public Transform NewDayPos;
    [SerializeField] private GameObject GradeMenu;
    [SerializeField] private MoneyManager Wallet;
    [SerializeField] private GradeMenuScript GMScript;
    public string TaskCompletionGrade;
    public string AverageHealthGrade;
    [SerializeField] private ZoologistControl Player;
    [SerializeField] private TMP_Text TimeTaken;
    [SerializeField] private AudioSource ClockOutSong;
    [SerializeField] private AudioSource ClockOutSongBad;

    [Header("DEBUG VALUES")]
    [SerializeField]
    public float AverageHealthScore;
    [SerializeField]
    private int taskScore;
    [SerializeField]
    string[] GradeList = { "F", "E", "D", "C", "B", "A", "S"};
    [SerializeField] private GameObject[] Disable;
    [SerializeField] private basicTasks[] AnimalTasks;
    [SerializeField] private GameObject player;
    public int taskCount;
    public int MoneyEarned = 0;

    private void Start()
    {
        GradeMenu.SetActive(false);
    }

    void Update()
    {
        
    }
    public void caculateGradeAndClockOut()
    {
        float totalHealthScore = 0;
        float totalTaskScore = 0;
        int activeEnclosures = 0;

        for (int i = 0; i < AnimalTasks.Length; i++)
        {
            taskCount += AnimalTasks[i].tasksCompleted;
        }

        Debug.Log(taskCount);
        
        foreach (var TaskBoard in TaskBoards)//get basicTask script in each task board
        {
            var taskManager = TaskBoard.GetComponent<basicTasks>();
            //checks if the enclosure is active/purchased, otherwise it is skipped int the final score tally
            if (taskManager.EnclosureActive == true)
            {
                activeEnclosures++;
                //gets the total health score
                totalHealthScore += taskManager.averageAnimalState;

                //gets the total task score
                totalTaskScore += taskManager.tasksCompleted;

                //resets the taskboards for the day
                taskManager.dayReset();
            }
            
        }
        //gets the overall average
        AverageHealthScore = totalHealthScore / activeEnclosures;
        taskScore = (int)Mathf.Round(totalTaskScore / activeEnclosures);
        //gets the grade values
        TaskCompletionGrade = GradeList[taskScore];
        if (AverageHealthScore < 14)
        {
            AverageHealthGrade = GradeList[0];
        }
        else if (AverageHealthScore >= 14 && AverageHealthScore < 28)
        {
            AverageHealthGrade = GradeList[1];
            MoneyEarned = 5;
        }
        else if (AverageHealthScore >= 28 && AverageHealthScore < 42)
        {
            AverageHealthGrade = GradeList[2];
            MoneyEarned = 10;
        }
        else if (AverageHealthScore >= 42 && AverageHealthScore < 56)
        {
            AverageHealthGrade = GradeList[3];
            MoneyEarned = 15;
        }
        else if (AverageHealthScore >= 56 && AverageHealthScore < 70)
        {
            AverageHealthGrade = GradeList[4];
            MoneyEarned = 20;
        }
        else if (AverageHealthScore >= 70 && AverageHealthScore < 84)
        {
            AverageHealthGrade = GradeList[5];
            MoneyEarned = 30;
        }
        else if (AverageHealthScore >= 84)
        {
            AverageHealthGrade = GradeList[6];
            MoneyEarned = 40;
            
        }
        
        float timer = Player.timeStart;
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
        if (minutes == "00")
        {
            TimeTaken.text = $"{seconds}s";
        }
        else
        {
            TimeTaken.text = $"{minutes}m, {seconds}s";
        }
        //DEBUG
        print("average status of all animals: " + AverageHealthGrade);
        print("average task completion of all animals: " + GradeList[taskScore]);
        float debugScore = AverageHealthScore;
        Debug.Log(debugScore);
        for (int i = 0; i < Disable.Length; i++)
        {
            Disable[i].SetActive(false);
        }
        if(AverageHealthGrade == GradeList[0] || AverageHealthGrade == GradeList[1] || AverageHealthGrade == GradeList[2])
        {
            ClockOutSongBad.Play();
        }
        if (AverageHealthGrade == GradeList[3] || AverageHealthGrade == GradeList[4] || AverageHealthGrade == GradeList[5] || AverageHealthGrade == GradeList[6])
        {
            ClockOutSong.Play();
        }
        GradeMenu.SetActive(true);
        
        player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
        Wallet.MoneyIncrease(MoneyEarned);
    }
}
