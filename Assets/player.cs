using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private float speed = 5f;
    private float jumpPower = 14f;

    private Rigidbody2D rb;
    private bool onGround = false;
    private bool finish = false;

    private Vector2[] direction = new Vector2[2];

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        direction[0] = Vector2.left;
        direction[1] = Vector2.right;
    }

    void Update()
    {
        Vector2 move = Vector2.zero;

        if (finish == false)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                move = direction[0];
            }

            if (Keyboard.current.dKey.isPressed)
            {
                move = direction[1];
            }

            rb.linearVelocity = new Vector2(
                move.x * speed,
                rb.linearVelocity.y
            );

            if (Keyboard.current.spaceKey.wasPressedThisFrame && onGround)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    jumpPower
                );

                onGround = false;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            finish = true;
            rb.linearVelocity = Vector2.zero;

            Debug.Log("到達終點！");
        }
    }
}