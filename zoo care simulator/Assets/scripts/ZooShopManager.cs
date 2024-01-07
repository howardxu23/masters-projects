using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZooShopManager : MonoBehaviour
{
    [SerializeField] private GameObject Desktop;
    [SerializeField] private GameObject ZooShop;
    [SerializeField] private TMP_InputField MoneyDisplay;
    [SerializeField] private AudioSource PaySound;
    [SerializeField] private AudioSource QuitSound;
    private int moneyAmount;
    private int testMoney = 100;
    private Dictionary<string, Boolean> ShopItems = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        Desktop.SetActive(true);
        ZooShop.SetActive(false);
        moneyAmount = MoneyManager.MM.currentMoney;
        MoneyDisplay.text = moneyAmount.ToString();
        
        ShopItems.Add("WindUp Fish Purchased", false);
        ShopItems.Add("Snowman Purchased", false);
        ShopItems.Add("Pool Slide Purchased", false);
        ShopItems.Add("Bamboo Plant Purchased", false);
        ShopItems.Add("Wooden Obstacles(Panda) Purchased", false);
        ShopItems.Add("Rocking Horse Purchased", false);
        ShopItems.Add("Giant Teddy Purchased", false);
        ShopItems.Add("Tyre Swing Purchased", false);
        ShopItems.Add("Wooden Obstacles(Coati) Purchased", false);
        ShopItems.Add("WindUp Mouse Purchased", false);
        ShopItems.Add("Tunnel Purchased", false);
        ShopItems.Add("Ball Pit Purchased", false);
        ShopItems.Add("Hanging Baskets Purchased", false);
        ShopItems.Add("Rope Bridges Purchased", false);
        ShopItems.Add("Warm Blanket Purchased", false);


    }

    public void Subtract20()
    {
        if (moneyAmount - 20 >= 0)
        {
            moneyAmount -= 20;
            MoneyDisplay.text = moneyAmount.ToString();
            GameObject CurrentBtn = EventSystem.current.currentSelectedGameObject;
            CurrentBtn.GetComponentInChildren<TMP_Text>().text = "Purchased";
            CurrentBtn.GetComponent<Button>().interactable = false;
            PaySound.Play();
        }
        else
        {
            Debug.Log("Insufficient Funds");
        }
    }
    
    public void Subtract50()
    {
        if (moneyAmount - 50 >= 0)
        {
            moneyAmount -= 50;
            MoneyDisplay.text = moneyAmount.ToString();
            GameObject CurrentBtn = EventSystem.current.currentSelectedGameObject;
            CurrentBtn.GetComponentInChildren<TMP_Text>().text = "Purchased";
            CurrentBtn.GetComponent<Button>().interactable = false;
            PaySound.Play();
        }
        else
        {
            Debug.Log("Insufficient Funds");
        }
    }
    
    public void Subtract80()
    {
        if (moneyAmount - 80 >= 0)
        {
            moneyAmount -= 80;
            MoneyDisplay.text = moneyAmount.ToString();
            GameObject CurrentBtn = EventSystem.current.currentSelectedGameObject;
            CurrentBtn.GetComponentInChildren<TMP_Text>().text = "Purchased";
            CurrentBtn.GetComponent<Button>().interactable = false;
            PaySound.Play();
        }
        else
        {
            Debug.Log("Insufficient Funds");
        }
    }

    public void PenguinItem1()
    {
        if (moneyAmount-20 >= 0)
        {
            ShopItems["WindUp Fish Purchased"] = true; 
        }
        
    }
    public void PenguinItem2()
    {
        if (moneyAmount - 50 >= 0)
        {
            ShopItems["Snowman Purchased"] = true;
        }
    }
    public void PenguinItem3()
    {
        if (moneyAmount - 80 >= 0)
        {
            ShopItems["Pool Slide Purchased"] = true;
        }
    }
    public void PandaItem1()
    {

        if (moneyAmount - 20 >= 0)
        {
            ShopItems["Bamboo Plant Purchased"] = true;
        }
    }
    public void PandaItem2()
    {
        if (moneyAmount - 50 >= 0)
        {
            ShopItems["Wooden Obstacles(Panda) Purchased"] = true;
        }
    }
    public void PandaItem3()
    {
        if (moneyAmount - 80 >= 0)
        {
            ShopItems["Rocking Horse Purchased"] = true;
        }
    }
    public void CoatiItem1()
    {
        if (moneyAmount - 20 >= 0)
        {
            ShopItems["Giant Teddy Purchased"] = true;
        }
    }
    public void CoatiItem2()
    {
        if (moneyAmount - 50 >= 0)
        {
            ShopItems["Tyre Swing Purchased"] = true;
        }
    }
    public void CoatiItem3()
    {
        if (moneyAmount - 80 >= 0)
        {
            ShopItems["Wooden Obstacles(Coati) Purchased"] = true;
        }
    }
    public void MeerkatItem1()
    {
        if (moneyAmount - 20 >= 0)
        {
            ShopItems["WindUp Mouse Purchased"] = true;
        }
    }
    public void MeerkatItem2()
    {
        if (moneyAmount - 50 >= 0)
        {
            ShopItems["Tunnel Purchased"] = true;
        }
    }
    public void MeerkatItem3()
    {
        if (moneyAmount - 80 >= 0)
        {
            ShopItems["Ball Pit Purchased"] = true;
        }
    }
    public void SlothItem1()
    {
        if (moneyAmount - 20 >= 0)
        {
            ShopItems["Hanging Baskets Purchased"] = true;
        }
    }
    public void SlothItem2()
    {
        if (moneyAmount - 50 >= 0)
        {
            ShopItems["Rope Bridges Purchased"] = true;
        }
    }
    public void SlothItem3()
    {
        if (moneyAmount - 80 >= 0)
        {
            ShopItems["Warm Blanket Purchased"] = true;
        }
    }

    public void Continue()
    {
        QuitSound.Play();
        string str = "";
        Debug.Log("Shopping Reciept: ");
        foreach (KeyValuePair<string, bool> ShopItems in ShopItems)
        {
            if (ShopItems.Value == true)
            {
                str += ShopItems.Key + ", ";
            }
        }
        Debug.Log(str);
        SceneManager.LoadScene("EndSplashScreen");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
