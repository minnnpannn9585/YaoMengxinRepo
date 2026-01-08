using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPuzzle : MonoBehaviour
{
    private Transform[] puzzles;
    public Transform target;
    Transform[] targets;
    public float rotationThreshold = 0.1f;

    public GameObject mainCanvas;
    public GameObject endingCanvas;

    private void Awake()
    {
        puzzles = new Transform[transform.childCount];
        targets = new Transform[transform.childCount];
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            puzzles[i] = transform.GetChild(i);
            targets[i] = target.GetChild(i);
        }
    }

    public void CheckFinish()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!AreRotationsEqual(puzzles[i], targets[i]))
            {
                return;
            }
        }
        mainCanvas.SetActive(false);
        endingCanvas.SetActive(true);
        print("finish");
    }
    
    public bool AreRotationsEqual(Transform obj1, Transform obj2)
    {
        // 计算两个四元数的夹角（0~180°）
        float angle = Quaternion.Angle(obj1.rotation, obj2.rotation);
        // 夹角小于阈值则认为一致
        return angle < rotationThreshold;
    }
}
