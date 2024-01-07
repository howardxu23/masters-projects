using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoRange : MonoBehaviour
{
    public Color GizmosColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);

    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
