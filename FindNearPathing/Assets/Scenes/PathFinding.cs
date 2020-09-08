using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour
{
    private static PathFinding instance;
    public static PathFinding GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    /// <summary>
    /// 获取新点,最后没有最近点之后 NextTransfrom添加了自身
    /// </summary>
    public List<Transform> GetNewList(List<Transform> pathList)
    {
        bool seeking = true;
        List<Transform> newPath1List = new List<Transform>();
        var nextNode = GetSmalldistanceVec(pathList[0], pathList);
        nextNode.GetComponent<Node_zq>().Selected = true;
        pathList[0].GetComponent<Node_zq>().NextTransfrom = nextNode;
        pathList[0].GetComponent<Node_zq>().Selected = true;
        newPath1List.Add(pathList[0]);
        while (seeking)
        {
            Transform nextPoint = null;
            foreach (var point in pathList)
            {
                if (point.GetComponent<Node_zq>().Selected == true && point.GetComponent<Node_zq>().NextTransfrom == null)
                {
                    nextPoint = point;
                    break;
                }
                else
                {
                    nextPoint = null;
                }
            }
            if (nextPoint != null)
            {
                nextPoint.GetComponent<Node_zq>().Selected = true;
                var otherList = new List<Transform>();
                foreach (var item in pathList)
                {
                    if (item.GetComponent<Node_zq>().Selected != true && item.GetComponent<Node_zq>().NextTransfrom == null)
                    {
                        otherList.Add(item);
                    }
                }
                var zuiJinDian = GetSmalldistanceVec(nextPoint, otherList);
                zuiJinDian.GetComponent<Node_zq>().Selected = true;
                nextPoint.GetComponent<Node_zq>().NextTransfrom = zuiJinDian;
                newPath1List.Add(nextPoint);
            }

            var panDuanList = new List<Transform>();
            foreach (var item in pathList)
            {
                if (item.GetComponent<Node_zq>().Selected == true && item.GetComponent<Node_zq>().NextTransfrom != null)
                {
                    panDuanList.Add(item);
                }
            }
            if (panDuanList.Count == pathList.Count)
            {
                seeking = false;
            }

        }
        return newPath1List;

    }

    /// <summary>
    /// 获取当前点的最近距离点,列表为空,返回自身
    /// </summary>
    /// <param name="thisObj"></param>
    /// <param name="otherObjs"></param>
    /// <returns></returns>
    Transform GetSmalldistanceVec(Transform thisObj, List<Transform> otherObjs)
    {
        var distenceDc = new Dictionary<float, Transform>();
        distenceDc.Clear();
        foreach (var item in otherObjs)
        {
            var dis = Vector3.Distance(thisObj.position, item.position);
            if (dis != 0)
            {
                distenceDc.Add(dis, item);
            }
        }
        var shengYuSortDic = (from objDic in distenceDc orderby objDic.Key select objDic).ToDictionary(pair => pair.Key, Pair => Pair.Value);
        if (shengYuSortDic.Count != 0)
        {
            return (shengYuSortDic.First().Value);

        }
        else
        {
            return (thisObj);

        }

    }

}
