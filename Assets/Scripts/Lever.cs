using UnityEngine;

public class Lever : Pullable
{
    bool pulled = false;
    bool wasWrapped = false;
    bool gateIsOpen = false;

    void Update()
    {
        bool isWrapped = rope.IsFullyWrappedAround(this.transform);

        if (isWrapped && !wasWrapped)
        {
            pulled = !pulled;
        }

        wasWrapped = isWrapped;

        if (pulled && !gateIsOpen)
        {
            gate.Open();
            gateIsOpen = true;
        }
        else if (!pulled && gateIsOpen)
        {
            gate.Close();
            gateIsOpen = false;
        }
    }
}
