using UnityEngine;

public class Gate : MonoBehaviour
{

    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    void Start() {
        this.boxCollider = GetComponent<BoxCollider2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Open() {
        boxCollider.enabled = false;
        Color clr = spriteRenderer.color;
        spriteRenderer.color = new Color(clr.r, clr.g, clr.b, 0.5f);
    }

    public void Close() {
        boxCollider.enabled = true;
        Color clr = spriteRenderer.color;
        spriteRenderer.color = new Color(clr.r, clr.g, clr.b, 1.0f);
    }

}
