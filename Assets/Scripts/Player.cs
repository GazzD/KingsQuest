using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float jumpingForce;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        rigidBody2D= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            isJumping = true;
            animator.SetBool("isJumping", true); // set animator properties to trigger to jumping animation
            rigidBody2D.AddForce(new Vector2(0f, jumpingForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor") {
            animator.SetBool("isJumping", false);
            isJumping = false;
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            GameManager.Instance.gameOver = true;
        }

    }
}
