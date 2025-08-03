using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gate : MonoBehaviour
{

    BoxCollider2D boxCollider;
    // SpriteRenderer spriteRenderer;
    [HideInInspector] public Rope rope;

    public bool startOpen = false;
    public bool isOpen  = false;

    public GameObject wall;

    public SpriteRenderer gateSpriteRenderer;
    public Sprite gateOpenSprite;
    public Sprite gateClosedSprite;

    private PlayerController player;
    private Animator animator;

    public bool isFinalGate = false;

    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        rope = FindFirstObjectByType<Rope>();

        if (startOpen)
            Open();
        else
            Close();

        player = FindFirstObjectByType<PlayerController>();
    }

    public void Open() {
        if (isOpen) return;

        boxCollider.enabled = false;
        isOpen = true;

        if(wall != null) {
            wall.SetActive(false);
        }
        if(gateSpriteRenderer != null) {
            gateSpriteRenderer.sprite = gateOpenSprite;
        }

        if(!isFinalGate) {
            animator.SetTrigger("open");
        }
    }

    public void Close() {
        if (!isOpen) return;

        boxCollider.enabled = true;
        isOpen = false;

        if (rope.IsRopeIntersecting(boxCollider))
        {
            Debug.Log("Gate cut off the rope, the player dies");
            player.Die();
        }

        if(wall != null) {
            wall.SetActive(true);
        }
        if(gateSpriteRenderer != null) {
            gateSpriteRenderer.sprite = gateClosedSprite;
        }

        animator.SetTrigger("close");
    }

    public void Toggle()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            if(isOpen && isFinalGate) {
                StartCoroutine(PlayGateAndTransition());
            }
        }
    }

    private IEnumerator PlayGateAndTransition()
    {
        if (animator != null)
        {
            animator.SetTrigger("open");
        }
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
