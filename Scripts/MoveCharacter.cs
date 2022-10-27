using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MoveCharacter : MonoBehaviour
{
    public Transform rayStart;
    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator animator;
    private GameManager gameManager;
    public GameObject crystalEffect;
    public float velocity = 2f;
    public AudioSource takeCyrstal;
    public double velocityTime = 10f;
    private bool firstTouch = false;
    



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.gameStarted)
        {

            return;
        }
        else
        {

            animator.SetTrigger("gameStarted");
        }

        rb.transform.position = transform.position + transform.forward * velocity * Time.deltaTime;

        if (velocityTime <= 0 && velocity <= 4f) {

            velocity += 0.2f;
            velocityTime = 5;
        }
       
    }

    void Start()
    {
      
    }

    void Update()
    {

        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            
            if (touch.phase == TouchPhase.Began)
            {

                gameManager.StartGame();
                if (firstTouch == true)
                {
                    Switch();
                }

                firstTouch = true;
                
            }
        }

        RaycastHit hit;

        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {

            animator.SetTrigger("isFalling");

        }
        else {

            animator.SetTrigger("notFallingAnymore");
        }

        if (transform.position.y < -5) {
            gameManager.EndGame();
            velocity = 2f;
          
        }
        velocityTime -= Time.deltaTime;
    }

    private void Switch()
    {

        walkingRight = !walkingRight;
        if (!gameManager.gameStarted) {
            return;
        }
        if (walkingRight)
        {

            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {

            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal") { 
        
            Destroy(other.gameObject);
            gameManager.IncreaseScore();

            GameObject go = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(go, 2f);

            takeCyrstal.Play();
            
        }

        

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (gameManager.gameStarted)
        {
            Destroy(collision.gameObject, 5f);
        }
    }


}
