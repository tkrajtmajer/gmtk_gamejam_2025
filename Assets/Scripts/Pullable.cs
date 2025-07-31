using UnityEngine;

public abstract class Pullable : MonoBehaviour
{
    public Gate gate;
    public Rope rope;

    void Awake()
    {
        rope = FindFirstObjectByType<Rope>();
    }
}
