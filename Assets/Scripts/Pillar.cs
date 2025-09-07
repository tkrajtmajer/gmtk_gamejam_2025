using UnityEngine;

public class Pillar : Pullable
{

    private bool gatesAreOpen = false;

    void Update()
    {
        bool isWrapped = rope.IsFullyWrappedAround(this.transform);

        if (isWrapped && !gatesAreOpen)
        {
            foreach (var gate in gates)
            {
                gate.Open();
            }
            gatesAreOpen = true;
        }
        else if (!isWrapped && gatesAreOpen)
        {
            foreach (var gate in gates)
            {
                gate.Close();
            }
            gatesAreOpen = false;
        }
    }
}
