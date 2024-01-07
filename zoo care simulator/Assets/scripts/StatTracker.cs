using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class StatTracker : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField, Tooltip("Drag in all animal pixel art icon")] private Sprite[] Icons;
    [SerializeField, Tooltip("Drag in all the animal name object from each entry")] private Text[] Names;
    [SerializeField] private Gradient gradient;

    [Header("Penguin Variables")]
    [SerializeField, Tooltip("Drag in image object from first data entry")] private Image PenguinPic;
    [SerializeField, Tooltip("Drag in text object that's underlined from first data entry")] private TMP_Text PenguinName;
    [SerializeField, Tooltip("Drag in text object that's next to the heart from first data entry")] private TMP_Text PenguinHealth;
    [SerializeField, Tooltip("Drag in text object that's next to the stomach from first data entry")] private TMP_Text PenguinHunger;
    [SerializeField, Tooltip("Drag in text object that's next to the smiley from first data entry")] private TMP_Text PenguinHappy;
    [SerializeField, Tooltip("Drag in slider object from first data entry")] private Slider PenguinOverall;
    [SerializeField, Tooltip("Drag in sliderfill object from first data entry to apply gradient")] private Image PenguinFill;
    [SerializeField, Tooltip("Drag in all sliders object from penguin")] private Slider[] PenguinCurrentStats;
    
    [Header("Panda Variables")] 
    [SerializeField, Tooltip("Drag in image object from second data entry")] private Image PandaPic;
    [SerializeField, Tooltip("Drag in text object that's underlined from second data entry")] private TMP_Text PandaName;
    [SerializeField, Tooltip("Drag in text object that's next to the heart from second data entry")] private TMP_Text PandaHealth;
    [SerializeField, Tooltip("Drag in text object that's next to the stomach from second data entry")] private TMP_Text PandaHunger;
    [SerializeField, Tooltip("Drag in text object that's next to the smiley from second data entry")] private TMP_Text PandaHappy;
    [SerializeField, Tooltip("Drag in slider object from second data entry")] private Slider PandaOverall;
    [SerializeField, Tooltip("Drag in sliderfill object from second data entry to apply gradient")] private Image PandaFill;
    [SerializeField, Tooltip("Drag in all sliders object from Panda")] private Slider[] PandaCurrentStats;
    
    [Header("Coati Variables")] 
    [SerializeField, Tooltip("Drag in image object from third data entry")] private Image CoatiPic;
    [SerializeField, Tooltip("Drag in text object that's underlined from third data entry")] private TMP_Text CoatiName;
    [SerializeField, Tooltip("Drag in text object that's next to the heart from third data entry")] private TMP_Text CoatiHealth;
    [SerializeField, Tooltip("Drag in text object that's next to the stomach from third data entry")] private TMP_Text CoatiHunger;
    [SerializeField, Tooltip("Drag in text object that's next to the smiley from third data entry")] private TMP_Text CoatiHappy;
    [SerializeField, Tooltip("Drag in slider object from third data entry")] private Slider CoatiOverall;
    [SerializeField, Tooltip("Drag in sliderfill object from third data entry to apply gradient")] private Image CoatiFill;
    [SerializeField, Tooltip("Drag in all sliders object from Coati")] private Slider[] CoatiCurrentStats;
    
    [Header("Meerkat Variables")] 
    [SerializeField, Tooltip("Drag in image object from fourth data entry")] private Image MeerkatPic;
    [SerializeField, Tooltip("Drag in text object that's underlined from fourth data entry")] private TMP_Text MeerkatName;
    [SerializeField, Tooltip("Drag in text object that's next to the heart from fourth data entry")] private TMP_Text MeerkatHealth;
    [SerializeField, Tooltip("Drag in text object that's next to the stomach from fourth data entry")] private TMP_Text MeerkatHunger;
    [SerializeField, Tooltip("Drag in text object that's next to the smiley from fourth data entry")] private TMP_Text MeerkatHappy;
    [SerializeField, Tooltip("Drag in slider object from fourth data entry")] private Slider MeerkatOverall;
    [SerializeField, Tooltip("Drag in sliderfill object from fourth data entry to apply gradient")] private Image MeerkatFill;
    [SerializeField, Tooltip("Drag in all sliders object from Meerkat")] private Slider[] MeerkatCurrentStats;
    
    [Header("Sloth Variables")] 
    [SerializeField, Tooltip("Drag in image object from first data entry")] private Image SlothPic;
    [SerializeField, Tooltip("Drag in text object that's underlined from fifth data entry")] private TMP_Text SlothName;
    [SerializeField, Tooltip("Drag in text object that's next to the heart from fifth data entry")] private TMP_Text SlothHealth;
    [SerializeField, Tooltip("Drag in text object that's next to the stomach from fifth data entry")] private TMP_Text SlothHunger;
    [SerializeField, Tooltip("Drag in text object that's next to the smiley from fifth data entry")] private TMP_Text SlothHappy;
    [SerializeField, Tooltip("Drag in slider object from fifth data entry")] private Slider SlothOverall;
    [SerializeField, Tooltip("Drag in sliderfill object from fifth data entry to apply gradient")] private Image SlothFill;
    [SerializeField, Tooltip("Drag in all sliders object from Sloth")] private Slider[] SlothCurrentStats;
    // Start is called before the first frame update
    void Start()
    {
        PenguinPic.GetComponent<Image>().sprite = Icons[0];
        PandaPic.GetComponent<Image>().sprite = Icons[1];
        CoatiPic.GetComponent<Image>().sprite = Icons[2];
        MeerkatPic.GetComponent<Image>().sprite = Icons[3];
        SlothPic.GetComponent<Image>().sprite = Icons[4];

        PenguinName.text = Names[0].text;
        PandaName.text = Names[1].text;
        CoatiName.text = Names[2].text;
        MeerkatName.text = Names[3].text;
        SlothName.text = Names[4].text;

    }

    // Update is called once per frame
    void Update()
    {
        PenguinName.text = Names[0].text;
        PandaName.text = Names[1].text;
        CoatiName.text = Names[2].text;
        MeerkatName.text = Names[3].text;
        SlothName.text = Names[4].text;

        gradient.Evaluate(1f);
        PenguinHealth.text = ""+ PenguinCurrentStats[0].value;
        PenguinHunger.text = ""+ PenguinCurrentStats[1].value;
        PenguinHappy.text = ""+ PenguinCurrentStats[2].value;
        PenguinOverall.value =
            (PenguinCurrentStats[0].value + PenguinCurrentStats[1].value + PenguinCurrentStats[2].value)/3;
        PenguinFill.color = gradient.Evaluate(PenguinOverall.normalizedValue);

        PandaHealth.text = ""+ PandaCurrentStats[0].value;
        PandaHunger.text = ""+ PandaCurrentStats[1].value;
        PandaHappy.text = ""+ PandaCurrentStats[2].value;
        PandaOverall.value =
            (PandaCurrentStats[0].value + PandaCurrentStats[1].value + PandaCurrentStats[2].value)/3;
        PandaFill.color = gradient.Evaluate(PandaOverall.normalizedValue);
        
        CoatiHealth.text = ""+ CoatiCurrentStats[0].value;
        CoatiHunger.text = ""+ CoatiCurrentStats[1].value;
        CoatiHappy.text = ""+ CoatiCurrentStats[2].value;
        CoatiOverall.value =
            (CoatiCurrentStats[0].value + CoatiCurrentStats[1].value + CoatiCurrentStats[2].value)/3;
        CoatiFill.color = gradient.Evaluate(CoatiOverall.normalizedValue);
        
        MeerkatHealth.text = ""+ MeerkatCurrentStats[0].value;
        MeerkatHunger.text = ""+ MeerkatCurrentStats[1].value;
        MeerkatHappy.text = ""+ MeerkatCurrentStats[2].value;
        MeerkatOverall.value =
            (MeerkatCurrentStats[0].value + MeerkatCurrentStats[1].value + MeerkatCurrentStats[2].value)/3;
        MeerkatFill.color = gradient.Evaluate(MeerkatOverall.normalizedValue);
        
        SlothHealth.text = ""+ SlothCurrentStats[0].value;
        SlothHunger.text = ""+ SlothCurrentStats[1].value;
        SlothHappy.text = ""+ SlothCurrentStats[2].value;
        SlothOverall.value =
            (SlothCurrentStats[0].value + SlothCurrentStats[1].value + SlothCurrentStats[2].value)/3;
        SlothFill.color = gradient.Evaluate(SlothOverall.normalizedValue);

        /*for (int i = 0; i < animalAverage.Count; i++)
        {
            for (int j = i+1; j < animalAverage.Count; j++) 
            {
                if (animalAverage[j] < animalAverage[i]) 
                { 
                    (animalAverage[i], animalAverage[j]) = (animalAverage[j], animalAverage[i]);
                }
            }
        }*/
    }
    
    /*IEnumerator ScrollLoop(Scrollbar vertScroll)
    {
        while (vertScroll.value>(-1.6f))
        {
            yield return new WaitForSeconds(0.1f);
            vertScroll.value -= 0.025f;
            if (vertScroll.value <= (-1.555))
            {
                vertScroll.value = 2.55f;
            }
        }
    }*/

    /*public void ScrollReset()
    {
        if (scroller.GetComponentInChildren<Scrollbar>().value < 0.01)
        {
            scroller.GetComponentInChildren<Scrollbar>().value = 1;
        }
    }*/
}
