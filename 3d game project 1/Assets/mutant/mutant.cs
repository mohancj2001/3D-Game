using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutant : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Initialize the animator if attached to the same GameObject
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("player"))
        {
            Debug.Log("Game Over");
            animator.SetTrigger("attack");
        }
    }
}