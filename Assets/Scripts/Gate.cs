using UnityEngine;

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

    void Start() {
        this.boxCollider = GetComponent<BoxCollider2D>();
        // this.spriteRenderer = GetComponent<SpriteRenderer>();

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
    }

    public void Toggle()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

}
