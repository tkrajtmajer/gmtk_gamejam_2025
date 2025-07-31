using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Rope rope;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() 
    {
        Vector2 moveDirection = movement * moveSpeed * Time.fixedDeltaTime;
        Vector2 moveAmount = rb.position + moveDirection;

        float currentRopeLength = rope.GetRopeLength(rb.position);
        float newRopeLength = rope.GetRopeLength(moveAmount);

        if (newRopeLength <= rope.maxLength || newRopeLength < currentRopeLength)
        {
            rb.MovePosition(moveAmount);
        }
    }
}
