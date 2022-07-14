using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordMenu : MonoBehaviour
{
    [SerializeField] GameObject[] records;
    [SerializeField] GameObject recordSelector;

    int currentPosition = 0;


    void Update()
    {
        recordSelector.transform.position = records[currentPosition].GetComponent<Transform>().position;
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

    public void RecordAtPosition()
    {
        records[currentPosition].GetComponent<Record>().HandleRecording();
    }
}
