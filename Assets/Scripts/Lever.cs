using UnityEngine;

public class Lever : Pullable
{
    bool wasWrapped = false;
    
    void Update()
    {
        bool isWrapped = rope.IsFullyWrappedAround(this.transform);

        if (isWrapped && !wasWrapped)
        {
            ToggleGates();
        }

        wasWrapped = isWrapped;
    }
}
