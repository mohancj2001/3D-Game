using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject GameOverText; 
    public GameObject GameOverBtn; 

    public float initialMoveSpeed = 10f; 
    public float speedIncreaseRate = 0.5f; 
    public float jumpForce = 1f;

    private bool isDead = false;
    private Rigidbody rb;
    private float currentMoveSpeed; 

    private void Start()
    {
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        Time.timeScale = 1;
        currentMoveSpeed = initialMoveSpeed;
    }

    private void Update()
    {
       
        if (isDead) return;

        
        currentMoveSpeed += speedIncreaseRate * Time.deltaTime;

       
        transform.position += Vector3.forward * Time.deltaTime * currentMoveSpeed;
        animator.SetBool("Run", true); 

     
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * currentMoveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * currentMoveSpeed;
        }

  
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
         
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
  
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jump"); 
        }
    }

    private bool IsGrounded()
    {

        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);

       
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("Game Over");
            animator.SetTrigger("dead"); 
            isDead = true;
            animator.SetBool("dead", true); 

           
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator GameOverRoutine()
    {
     
        yield return new WaitForSeconds(2.19f);
        Time.timeScale = 0; 
        GameOverText.SetActive(true);
        GameOverBtn.SetActive(true);
    }
}