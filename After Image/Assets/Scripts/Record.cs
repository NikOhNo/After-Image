using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    [SerializeField] GameObject clone;
    bool recording = false;
    bool recordPlaying = false;
    Queue<ActionReplayRecord> recordQueue = new Queue<ActionReplayRecord>();
    Player player;
    Animator myAnimator;
    GameObject instantiatedClone;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        myAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (recording)
        {
            Debug.Log("Recording!");
            // Record all info of the player on the frame
            recordQueue.Enqueue(new ActionReplayRecord { position = player.transform.position, rotation = player.transform.rotation });
        }
        if (instantiatedClone == null) // if there is no clone, no actions are being replayed
        {
            recordPlaying = false;
        }
        myAnimator.SetBool("RecordPlaying", recordPlaying);
    }

    // When the recording button is pressed this function will decide the proper action to take
    public void HandleRecording()
    {
        if (!recordPlaying)
        {
            switch (recording)
            {
                case true: // end recording
                    recording = false;
                    myAnimator.SetBool("Recording", recording);
                    myAnimator.SetBool("FullRecord", true);
                    break;
                case false:
                    if (recordQueue.Count > 0) // if there is a recordQueue detonate it and use it
                    {
                        Debug.Log("Detonate Clone!");
                        DetonateClone();
                        myAnimator.SetBool("FullRecord", false);
                    }
                    else // if there is no queue begin a recording
                    {
                        recording = true;
                        myAnimator.SetBool("Recording", recording);
                    }
                    break;
            }
        }
    }

    private void DetonateClone()
    {
        //creates a clone, hands it the queue
        ActionReplayRecord firstRecord = recordQueue.Dequeue();
        instantiatedClone = Instantiate(clone, firstRecord.position, firstRecord.rotation);
        instantiatedClone.GetComponent<Clone>().RecieveQueue(recordQueue);

        recordPlaying = true;
        myAnimator.SetBool("RecordPlaying", recordPlaying);
    }
}
