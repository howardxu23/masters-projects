//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using static SkillTree;

//public class Skill : MonoBehaviour
//{
//    public int id;

//    public TMP_Text TitleText;
//    public TMP_Text DescriptionText;

//    public int[] ConnectedSkills;

//    public void UpdateUI()
//    {
//        TitleText.text = $"{skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}\n{skillTree.SkillNames[id]}";
//        DescriptionText.text = $"{skillTree.SkillDescriptions[id]}\nCost : {skillTree.SkillPoint}/1 SP";

//        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow : skillTree.SkillPoint >= 1 ? Color.green : Color.White;
//    }

//    public void Buy()
//    {
//        if (skillTree.SkillPoint < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;
//        skillTree.SkillPoint -= 1;
//        skillTree.SkillLevels[id]++;
//    }
//}
