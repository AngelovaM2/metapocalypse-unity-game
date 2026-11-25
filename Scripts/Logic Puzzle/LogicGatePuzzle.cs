using UnityEngine;
using System.Collections.Generic;

public class LogicGatePuzzle : MonoBehaviour
{
    public static LogicGatePuzzle LogicInstance;
    public List<Slot> slots = new List<Slot>();
    public GameObject puzzleSlots;
    public GameObject ventilationReward;
    AudioSource audioSource;
    public AudioClip Lpuzzlesolvedclip;


    int correctslots = 0;
    private void Awake()
    {
        // Basic singleton pattern
        if (LogicInstance == null) LogicInstance = this;
        else Destroy(gameObject);
        for(int i = 0; i < puzzleSlots.transform.childCount; i++)
        {
            slots.Add(puzzleSlots.transform.GetChild(i).GetComponent<Slot>());
        }
    }

    private void Start()
    {
        ventilationReward.SetActive(false);
        audioSource= GetComponent<AudioSource>();
    }

    public void CheckPuzzleCompletion()
    {
        foreach(Slot slot in slots)
        {
            if (slot.isCorrectlyFilled())
            {
                correctslots++;
            }
        }  
          
        if (correctslots == slots.Count)
        {
            //unlock door or something
            ventilationReward.SetActive(true);
            audioSource.PlayOneShot(Lpuzzlesolvedclip);

        } 
        else
        {
            correctslots = 0;
        }      
    }
}
