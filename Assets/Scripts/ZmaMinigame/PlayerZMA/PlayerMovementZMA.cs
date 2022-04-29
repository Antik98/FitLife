using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMovementZMA : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float curSpeed = 0;
    public Rigidbody2D rb;
    public Animator animator;
    public ParticleSystem Dust;

    Vector2 movement;
    Vector2 movementPrev;

    void Start()
    {
        curSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Input handeled here
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");



        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Physics handeled here
        if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0)
            Dust.Play();
        rb.MovePosition(rb.position + movement * curSpeed * Time.fixedDeltaTime);
        movementPrev = movement;
    }

    public void lockPlayer()
    {
        this.GetComponent<Animator>().enabled = false;
        curSpeed = 0;
    }


    public void unlockPlayer()
    {
        this.GetComponent<Animator>().enabled = true;
        curSpeed = moveSpeed;
    }

}
