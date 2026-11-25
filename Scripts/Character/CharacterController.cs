using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour//, IDataPersistence
{
    //public int maxHealth = 5;
    public float walk = 3.0f;
    public float run = 5.5f;

    //public HealthBar healthBar;
    //public bool allowCombat;
    //public int currentHealth;


    float speed;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    List<Collider2D> mapCollider = new List<Collider2D>();

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    Collider2D characterCollider;

    public CarryableItem currentlyHeldGate;

    //AudioSource audioSource;
    //public AudioClip death;

    void Awake() {
        /* foreach (GameObject ob in GameObject.FindGameObjectsWithTag("MapCollider")) {
            mapCollider.Add(ob.GetComponent<Collider2D>());
        } */
    }

    void Start() {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<Collider2D>();

        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //exit scene to main menu
        /*if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            // save the game anytime before loading a new scene
            DataPersistenceManager.instance.SaveGame();
            // load the main menu scene
            SceneManager.LoadSceneAsync("Menu");
        }
        */

        if (Input.GetKey(KeyCode.LeftShift)) {
            animator.SetBool("Run", true);
            speed = run;
        } else {
            animator.SetBool("Run", false);
            speed = walk;
        }

    }

    /*public void LoadData(GameData data) 
    {   
        Debug.Log("LoadData " + data.playerPosition);
        
        this.transform.position = data.playerPosition;
        this.currentHealth = data.playerAttributesData.currentHealth;
    }

    public void SaveData(GameData data) 
    {
        //data.playerPosition = 
        Debug.Log("SaveData: " + this.transform.position);

        data.scene = SceneManager.GetActiveScene().name;
        data.playerPosition = this.transform.position;
        data.playerAttributesData.currentHealth = this.currentHealth;
    }
    */

    void FixedUpdate() {
        Vector2 position = rigidbody2d.position;
        Vector2 move = new Vector2(horizontal, vertical);
        if (move.magnitude > 1f) {
            move = move.normalized;
        }
        // Debug.Log("directional" + move);
        position = position + speed * move * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }


    /*
    public void ChangeHealth(int amount) {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            animator.SetTrigger("Hit");
            
            //Vector2 position = GetComponent<Rigidbody2D>().position;
            //Instantiate(hitEffect, position, Quaternion.identity).Play();

            isInvincible = true;
            invincibleTimer = timeInvincible;

            //audioSource.PlayOneShot(hitClip);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        if(currentHealth == 0) {

            audioSource.PlayOneShot(death);
            animator.SetTrigger("Dead");
            // Dead character something.
            Destroy(GetComponent<CharacterController>());
            rigidbody2d.bodyType = RigidbodyType2D.Static;
        }
        
    }
    */

    /*void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "BridgeTrigger") 
            foreach (Collider2D ob in mapCollider)
                Physics2D.IgnoreCollision(characterCollider, ob, false);
    }
    */

    // will need for bridge puzzle and scene transition
    /* void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "BridgeTrigger") {
            foreach (Collider2D ob in mapCollider) {
                Physics2D.IgnoreCollision(characterCollider, ob, true);
            }
        }

        if (other.tag == "TransitionPoint") {
            TransitionPoints nextSpawn = other.GetComponent<TransitionPoints>();

            this.transform.position = new Vector3(nextSpawn.x, nextSpawn.y, 0);

            //DataPersistenceManager.instance.SaveGame();
            Debug.Log("nextSpawn.Scene");
            SceneManager.LoadSceneAsync(nextSpawn.nextScene);
        }
    }
    */
}
