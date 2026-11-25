using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalking : MonoBehaviour
{
    AudioSource audioSource;
    public List<AudioClip> basicFootsteps;
    public float footstepDelayTimer;
    public float runDelayTimer;

    float horizontal;
    float vertical;
    float footstepDelay;
    int clipCounter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        footstepDelay = 0;
        clipCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
                
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(footstepDelay > 0)
            footstepDelay -= Time.deltaTime;

        if (move.magnitude > 0.1f && footstepDelay <= 0.0f) {
            clipCounter = Random.Range(0, basicFootsteps.Count);
            audioSource.PlayOneShot(basicFootsteps[clipCounter],0.05f);
            if (!Input.GetKey(KeyCode.LeftShift))
                footstepDelay = footstepDelayTimer;
            else              
                footstepDelay = runDelayTimer;
        }
    }
}
