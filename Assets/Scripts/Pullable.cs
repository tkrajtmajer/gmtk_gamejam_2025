using UnityEngine;

public abstract class Pullable : MonoBehaviour
{
    public Gate[] gates;
    [HideInInspector] public Rope rope;

    void Awake()
    {
        rope = FindFirstObjectByType<Rope>();
    }

    protected void ToggleGates()
    {
        foreach (Gate gate in gates)
        {
            gate.Toggle();
        }
    }
}
