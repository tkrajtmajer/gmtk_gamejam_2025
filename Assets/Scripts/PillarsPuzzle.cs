using System.Collections.Generic;
using UnityEngine;

public class PillarsPuzzle : MonoBehaviour
{
    [SerializeField] private List<Transform> correctOrder; 
    [SerializeField] private List<Transform> allPillars;  
    [SerializeField] private Rope rope;

    [SerializeField] private Gate gate;

    private int currentIndex = 0;
    private HashSet<Transform> wrappedPillars = new HashSet<Transform>();

    void Start() {
        rope = FindFirstObjectByType<Rope>();

        gate = FindFirstObjectByType<Gate>();
    }

    void Update()
    {
        if (currentIndex >= correctOrder.Count) return;

        foreach (Transform pillar in allPillars)
        {
            if (rope.IsFullyWrappedAround(pillar))
                wrappedPillars.Add(pillar);
            else
                wrappedPillars.Remove(pillar);
        }

        foreach (Transform pillar in wrappedPillars)
        {
            if (!correctOrder.Contains(pillar)) return; 
            int index = correctOrder.IndexOf(pillar);
            if (index > currentIndex) return; 
        }

        Transform expected = correctOrder[currentIndex];
        if (rope.IsFullyWrappedAround(expected))
        {
            Debug.Log("Correct pillar wrapped: " + expected.name);
            currentIndex++;

            if (currentIndex >= correctOrder.Count)
                OnSequenceComplete();
        }
    }

    void OnSequenceComplete()
    {
        gate.Open();
    }
}
