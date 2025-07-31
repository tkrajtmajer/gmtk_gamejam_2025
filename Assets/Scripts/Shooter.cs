using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPoint;
    public Vector2 projDirection;
    public float projMoveSpeed = 6f;
    public LayerMask projCollisionLayer;

    public Transform projectilesParent;

    public float spawnDelay = 5f; 
    private float timer = 0f;

    void Start() {
        spawnPoint = this.gameObject.transform.GetChild(0);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay) {
            SpawnProjectile();
            timer = 0f; 
        }
    }

    void SpawnProjectile() {
        GameObject proj = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().direction = projDirection;
        proj.GetComponent<Projectile>().moveSpeed = projMoveSpeed;
        proj.GetComponent<Projectile>().collisionLayer = projCollisionLayer;

        proj.transform.SetParent(projectilesParent);
    }
}
