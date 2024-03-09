using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    float direction = 0f;

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;

    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        direction = v.x;
    }

    private void Move(float dir)
    {
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }

    void OnJump()
    {
        if (isGrounded)
            Jump();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (Vector2.Angle(collision.GetContact(0).normal, Vector2.up) < 45f)
                isGrounded = true;
        }
        else
            isGrounded = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
