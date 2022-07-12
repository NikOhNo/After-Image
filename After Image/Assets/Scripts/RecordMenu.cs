using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordMenu : MonoBehaviour
{
    [SerializeField] GameObject[] records;
    [SerializeField] GameObject recordingDot;

    int currentPosition = 0;

    // Update is called once per frame
    void Update()
    {
        recordingDot.transform.position = records[currentPosition].GetComponent<Transform>().position;
    }

    public void MovePositionRight()
    {
        if(currentPosition + 1 < records.Length)
        {
            currentPosition += 1;
        }
    }

    public void MovePositionLeft()
    {
        if(currentPosition - 1 >= 0)
        {
            currentPosition -= 1;
        }
    }
}