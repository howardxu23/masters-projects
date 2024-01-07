using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager MM;
    public int currentMoney;

    void Awake () {
        MakeThisTheOnlyGameManager ();
    }

    void MakeThisTheOnlyGameManager(){
        if(MM == null){
            DontDestroyOnLoad(gameObject);
            MM = this;
        }
        else{
            if(MM != this){
                Destroy (gameObject);
            }
        }
    }

    public void MoneyIncrease(int reward)
    {
        currentMoney += reward;
    }
}
