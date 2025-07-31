using UnityEngine;

public class Lever : Pullable
{
    bool pulled = false;
    bool hasBeenPulled = false;
    bool wasWrapped = false;

    void Update()
    {
        bool isWrapped = rope.IsFullyWrappedAround(this.transform);

        if (isWrapped && !wasWrapped)
        {
            pulled = !pulled;
        }

        wasWrapped = isWrapped;

        if (pulled)
        {
            if (!hasBeenPulled)
            {
                gate.Open();
                hasBeenPulled = true;
            }
        }
        else
        {
            gate.Close();
            hasBeenPulled = false;
        }
    }
}
