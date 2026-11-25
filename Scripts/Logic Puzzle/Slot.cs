using UnityEngine;

public class Slot : MonoBehaviour
{
    public GateType correctGateType;
    public CarryableItem currentGate;
    public GameObject slotPickUpText;
    public GameObject slotPlaceText;
    public bool slotInRange;

    private CharacterController player;

    void Start()
    {
        slotInRange = false;
        slotPickUpText.SetActive(false); 
        slotPlaceText.SetActive(false);
    }

    public bool isCorrectlyFilled()
    {
        // Slot is correctly filled if there's a gate and its type matches the correct required type
        return (currentGate != null && currentGate.gateType == correctGateType);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<CharacterController>();
        
        if (other.CompareTag("Player"))
        {   
            slotInRange = true;
            if (player.currentlyHeldGate != null && currentGate == null)
            {
                slotPlaceText.SetActive(true);
                slotPickUpText.SetActive(false);
            }
            else if (player.currentlyHeldGate == null && currentGate != null)
            {
                slotPickUpText.SetActive(true);
                slotPlaceText.SetActive(false);
            }
            else
            {
                slotPickUpText.SetActive(false);
                slotPlaceText.SetActive(false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            slotPickUpText.SetActive(false); 
            slotPlaceText.SetActive(false);

            slotInRange = false;
            player = null;
        }
    }

    void Update()
    {
        // Only respond to input if the player is in range
        if (slotInRange && Input.GetKeyDown(KeyCode.E))
        {
            // If the player is holding a gate and our slot is empty, place the gate
            if (player.currentlyHeldGate != null && currentGate == null)
            {
                currentGate = player.currentlyHeldGate;
                // Change the position of the gate to the slot's position
                player.currentlyHeldGate.transform.position = transform.position + new Vector3(0, 0.15f, 0);

                // call the tutorial class to check if the puzzle is solved correctly
                if(!LogicGatePuzzleTutorial.TutorialLogicInstance.logicTutorialCompleted)
                {
                    LogicGatePuzzleTutorial.TutorialLogicInstance.CheckTutorialPuzzleCompletion();
                }
                else
                {
                    LogicGatePuzzle.LogicInstance.CheckPuzzleCompletion();  
                }

                player.currentlyHeldGate = null; // Player no longer holds the gate
            }
            // If the player is NOT holding a gate and our slot has a gate, pick it up
            else if (player.currentlyHeldGate == null && currentGate != null)
            {
                // Give the slot's gate to the player
                player.currentlyHeldGate = currentGate;

                // Place it at the player's position plus some offset above the head.
                currentGate.transform.position = player.transform.position + new Vector3(0, 0.85f, 0);

                // Clear the reference from the slot
                currentGate = null;
            }
        }
    }

}
