using UnityEngine;

public class LogicGatePuzzleTutorial : MonoBehaviour
{
    public static LogicGatePuzzleTutorial TutorialLogicInstance;
    // Current tutorial step: 1 = AND puzzle, 2 = AND+OR puzzle, 3 = AND+OR+NOT puzzle
    [SerializeField] private int currentTutorial = 1;
    public Slot slotAndT;
    public Slot slotOrT;
    public Slot slotNotT;
    public GameObject Slots;
    public GameObject Gates;

    public GameObject puzzlePanel;
    public CarryableItem andGate;
    public GameObject orGate;
    public Animator orGateCabinetAnim;
    public Animator notGateCabinetAnim;
    Animator tutorialPanelDoorAnim;
    public Animator puzzlePanelDoorAnim;
    public GameObject notGate;
    public bool logicTutorialCompleted;
    private int andGatepickedup = 0;
    private SpriteRenderer spriteRendererOR;
    private SpriteRenderer spriteRendererNOT;

    private CharacterController player;
    [SerializeField] GameObject playerTutorial;
    AudioSource audioSource;

    public AudioClip tutorialsolvedclip;
    public GameObject sensorReward;

    private void Awake()
    {
        // Basic singleton pattern
        if (TutorialLogicInstance == null) TutorialLogicInstance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = playerTutorial.GetComponent<CharacterController>();
        logicTutorialCompleted = false;
        orGate.SetActive(false);
        notGate.SetActive(false);
        Slots.SetActive(false);
        Gates.SetActive(false);
        puzzlePanel.SetActive(false);
        tutorialPanelDoorAnim = gameObject.GetComponent<Animator>();
        spriteRendererOR = orGate.GetComponent<SpriteRenderer>();
        spriteRendererNOT = notGate.GetComponent<SpriteRenderer>();
        audioSource= GetComponent<AudioSource>();
        sensorReward.SetActive(false);
    }

    private void Update()
    {
        if (player.currentlyHeldGate == andGate && andGatepickedup == 0)
        {
            Debug.Log("heldgate = 1");        
            tutorialPanelDoorAnim.SetTrigger("OpenTpanel");
            andGatepickedup = 1;
        }

        if (player.currentlyHeldGate != null && player.currentlyHeldGate.gateType == GateType.OR)
        {
            Debug.Log("layerOr");
            spriteRendererOR.sortingOrder = 5;
        }

        if (player.currentlyHeldGate != null && player.currentlyHeldGate.gateType == GateType.NOT)
        {
            spriteRendererNOT.sortingOrder = 5;
        }
    }

    public void CheckTutorialPuzzleCompletion()
    {
        // Check if the current tutorial is solved
        switch (currentTutorial)
        {
            case 1:
                // Tutorial 1 

                if (slotAndT.isCorrectlyFilled())
                {
                    Debug.Log("Tutorial 1 Solved!");
                    AdvanceToNextTutorial();
                }
                break;

            case 2:
                // Tutorial 2 
                if (slotAndT.isCorrectlyFilled() && slotOrT.isCorrectlyFilled())
                {
                    Debug.Log("Tutorial 2 Solved!");
                    AdvanceToNextTutorial();
                }
                break;

            case 3:
                // Tutorial 3
                if (slotAndT.isCorrectlyFilled() && slotOrT.isCorrectlyFilled() && slotNotT.isCorrectlyFilled())
                {
                    Debug.Log("Tutorial 3 Solved!");
                    AdvanceToNextTutorial();
                }
                break;
        }
    }

    private void AdvanceToNextTutorial()
    {
        currentTutorial++;

        if (currentTutorial == 2)
        {
            // Unlock the OR gate
            // animation for the drawer opening
            orGateCabinetAnim.SetTrigger("OpenCabinet");

            orGate.SetActive(true);
        }
        else if(currentTutorial == 3)
        {
            // Unlock the NOT gate
            // animation for the drawer opening
            notGateCabinetAnim.SetTrigger("OpenCabinet");
            notGate.SetActive(true);
        }
        else if (currentTutorial > 3)
        {
            Debug.Log("All tutorials completed!");
            logicTutorialCompleted = true;
            // Unlock the next puzzle
            puzzlePanel.SetActive(true);
            Slots.SetActive(true);
            Gates.SetActive(true);
            puzzlePanelDoorAnim.SetTrigger("OpenPpanel");
            sensorReward.SetActive(true);
            audioSource.PlayOneShot(tutorialsolvedclip);

            return;
        }
    }

}
