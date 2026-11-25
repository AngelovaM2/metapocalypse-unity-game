using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField]
    PartsChecklist partsChecklistScript;
    [SerializeField]
    NotesChecklist notesChecklistScript;

    public GameObject pickuptext;
    bool inItemRange;
    public int puzzlePart = -1;
    public int noteNumber = -1;
    public Animator sidebarAnimator;
    public GameObject pressJText;
    private bool firstNote;

    void Start()
    {
        pickuptext.SetActive(false);
        inItemRange = false;

        firstNote = false;
        pressJText.SetActive(false);
    }

    void Update()
    {
        if (inItemRange && Input.GetKeyDown(KeyCode.E))
        {
            pickuptext.SetActive(false);
            if(puzzlePart != -1 && noteNumber == -1)
            {
                partsChecklistScript.partsImages[puzzlePart].color = Color.white;
                sidebarAnimator.SetTrigger("Sidebar");
            }
            else if(puzzlePart == -1 && noteNumber != -1)
            {
                notesChecklistScript.notesImages[noteNumber].color = Color.white;
                notesChecklistScript.notesPickedUp[noteNumber] = true; 
                
                if(!firstNote)
                {
                    pressJText.SetActive(true);
                    firstNote = true;
                }
            }
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        
        if (controller != null)
        {   
            pickuptext.SetActive(true); 
            inItemRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        
        if (controller != null)
        {   
            pickuptext.SetActive(false); 
            inItemRange = false;
        }
    }
}
