using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    [SerializeField] GameObject clone;
    bool recording = false;
    Queue<ActionReplayRecord> recordQueue = new Queue<ActionReplayRecord>();
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (recording)
        {
            Debug.Log("Recording!");
            // Record all info of the player on the frame
            recordQueue.Enqueue(new ActionReplayRecord { position = player.transform.position , rotation = player.transform.rotation });
        }
    }

    public void HandleRecording()
    {
        switch (recording)
        {
            case true: // end recording
                recording = false;
                break;
            case false:
                if(recordQueue.Count > 0) // if there is a recordQueue detonate it and use it
                {
                    Debug.Log("Detonate Clone!");
                    DetonateClone();
                }
                else // if there is no queue begin a recording
                {
                    recording = true;
                }
                break;
        }
    }

    private void DetonateClone()
    {
        ActionReplayRecord firstRecord = recordQueue.Dequeue();
        GameObject newClone = Instantiate(clone, firstRecord.position, firstRecord.rotation);
        newClone.GetComponent<Clone>().RecieveQueue(recordQueue);
    }
}
