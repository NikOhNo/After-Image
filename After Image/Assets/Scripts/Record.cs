using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    // variables relating to clone
    [SerializeField] GameObject clone;
    [SerializeField] GameObject recorder;
    [SerializeField] Color color;
    Queue<ActionReplayRecord> recordQueue = new Queue<ActionReplayRecord>();

    // bools to handle which action to do
    bool recording = false;
    bool recordPlaying = false;

    // references to important components and objects 
    Player player;
    Animator myAnimator;
    GameObject instantiatedClone;
    GameObject instantiatedRecorder;


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
            recordQueue.Enqueue(new ActionReplayRecord { position = instantiatedRecorder.transform.position, 
                rotation = instantiatedRecorder.transform.rotation });
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
                    // destroy recorder, reenable player
                    recording = false;
                    Destroy(instantiatedRecorder);
                    player.enabled = true;
                    FindObjectOfType<RecordMenu>().UnlockMenu();

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
                        FindObjectOfType<RecordMenu>().LockMenu();
                        myAnimator.SetBool("Recording", recording);
                        // Lock player use recorder
                        instantiatedRecorder = Instantiate(recorder, player.transform);
                        player.enabled = false;
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
        instantiatedClone.GetComponentInChildren<SpriteRenderer>().color = color;
        instantiatedClone.GetComponent<Clone>().RecieveQueue(recordQueue);

        recordPlaying = true;
        myAnimator.SetBool("RecordPlaying", recordPlaying);
    }
}
