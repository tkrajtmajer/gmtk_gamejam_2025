using UnityEngine;

public class Pillar : Pullable
{
    private bool gateIsOpen = false;

    void Update()
    {
        bool isWrapped = rope.IsFullyWrappedAround(this.transform);

        if (isWrapped && !gateIsOpen)
        {
            gate.Open();
            gateIsOpen = true;
        }
        else if (!isWrapped && gateIsOpen)
        {
            gate.Close();
            gateIsOpen = false;
        }
    }
}
