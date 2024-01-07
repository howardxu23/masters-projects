using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class EnemyController : MonoBehaviour
////{
////    public enum GhostNodeStatesEnum
////    {
////        respawning,
////        nodeLeft,
////        nodeMiddleLeft,
////        nodeMiddleRight,
////        nodeRight,
////        movingInNodes
////    }

////    public GhostNodeStatesEnum ghostNodeState;

////    public enum GhostColour
////    {
////        red,
////        blue,
////        pink,
////        orange
////    }

////    public GhostColour ghostColour;

////    public GameObject ghostNodeLeft;
////    public GameObject ghostNodeMiddleLeft;
////    public GameObject ghostNodeMiddleRight;
////    public GameObject ghostNodeRight;

////    public MovementController movementController;

////    public GameObject startingNode;

////    // Start is called before the first frame update
////    void Awake()
////    {
////        movementController = GetComponent<MovementController>();
////        if (ghostColour == GhostColour.red)
////        {
////            ghostNodeState = GhostNodeStatesEnum.nodeLeft;
////            startingNode = ghostNodeLeft;
////        }
////        else if (ghostColour == GhostColour.blue)
////        {
////            ghostNodeState = GhostNodeStatesEnum.nodeMiddleLeft;
////            startingNode = ghostNodeMiddleLeft;
////        }
////        else if (ghostColour == GhostColour.pink)
////        {
////            ghostNodeState = GhostNodeStatesEnum.nodeMiddleRight;
////            startingNode = ghostNodeMiddleRight;
////        }
////        else if (ghostColour == GhostColour.orange)
////        {
////            ghostNodeState = GhostNodeStatesEnum.nodeRight;
////            startingNode = ghostNodeRight;
////        }
////        movementController.currentNode = startingNode;
////    }

//    // Update is called once per frame
//    //void Update()
//    //{
        
//    //}
//}
