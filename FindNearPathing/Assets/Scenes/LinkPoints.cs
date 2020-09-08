using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class LinkPoints : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    List<Transform> newPoints = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            points.Add(transform.GetChild(i));
        }
        newPoints = PathFinding.GetInstance().GetNewList(points);
        var newPointVec = new List<Vector3>();
        for (int i = 0; i < newPoints.Count; i++)
        {
            newPoints[i].GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
        }
        foreach (var item in newPoints)
        {
            newPointVec.Add(item.position);
        }
        
    }

   
}
