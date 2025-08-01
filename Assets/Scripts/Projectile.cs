using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float moveSpeed = 6f;
    [HideInInspector] public LayerMask collisionLayer;
    
    void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        Vector2 nextPos = currentPos + direction.normalized * moveSpeed * Time.fixedDeltaTime;

        // RaycastHit2D hit = Physics2D.Raycast(currentPos, direction, moveSpeed * Time.fixedDeltaTime, collisionLayer);

        // if (hit.collider != null)
        // {
        //     Destroy(gameObject);
        // }
        // else
        // {
        //     transform.position = nextPos;
        // }

        transform.position = nextPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy(other.gameObject);
            Destroy(this.gameObject);
            Debug.Log("player dies");
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
