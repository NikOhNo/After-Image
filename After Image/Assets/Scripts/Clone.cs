using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    Queue<ActionReplayRecord> recordQueue;

    void Update()
    {
        ReplayActions(recordQueue);
    }

    public void ReplayActions(Queue<ActionReplayRecord> recordQueue)
    {
        if(recordQueue.Count == 0) // Destroy clone if it has no more actions left to replay
        {
            Destroy(gameObject);
            return;
        }
        ActionReplayRecord currentRecord = recordQueue.Dequeue();
        transform.position = currentRecord.position;
        transform.rotation = currentRecord.rotation;
    }

    public void RecieveQueue(Queue<ActionReplayRecord> queueToRecieve)
    {
        recordQueue = queueToRecieve;
    }
}
