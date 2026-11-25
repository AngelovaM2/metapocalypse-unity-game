using UnityEngine;

public class CarryableItem : MonoBehaviour
{
    public GateType gateType;
    public GameObject carrytext;
    public GameObject dropText;
    private CharacterController player;
    [SerializeField] GameObject playerCarryableItem;
    bool inItemRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        dropText.SetActive(false);
    }
    void Start()
    {
        player = playerCarryableItem.GetComponent<CharacterController>();
        carrytext.SetActive(false);
        inItemRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inItemRange && player.currentlyHeldGate == null && Input.GetKeyDown(KeyCode.E))
        {
            carrytext.SetActive(false);
            player.currentlyHeldGate = this;
            player.currentlyHeldGate.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (player.currentlyHeldGate != null)
        {   
            player.currentlyHeldGate.transform.position = player.transform.position + new Vector3(0, 1.56f, 0);
            dropText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                dropText.SetActive(false);

                player.currentlyHeldGate.transform.position = player.transform.position;
                player.currentlyHeldGate.GetComponent<BoxCollider2D>().enabled = true;
                player.currentlyHeldGate = null;
            }
        }

        if (player.currentlyHeldGate == null)
        {
            dropText.SetActive(false);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {   
            carrytext.SetActive(true); 
            inItemRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {   
            carrytext.SetActive(false); 
            inItemRange = false;
        }
    }
}
