using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Rope rope;

    public Animator animator; 

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("speed", movement.magnitude);

        if (movement.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(movement.x) * 1.5f;
            transform.localScale = scale;
        }
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
