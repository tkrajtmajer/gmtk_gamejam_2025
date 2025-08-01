using UnityEngine;

public abstract class Pullable : MonoBehaviour
{
    public Gate gate;
    [HideInInspector] public Rope rope;

    void Awake()
    {
        rope = FindFirstObjectByType<Rope>();
    }
}
