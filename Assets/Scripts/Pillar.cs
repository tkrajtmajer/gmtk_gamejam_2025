using UnityEngine;

public class Pillar : Pullable
{
    void Update()
    {
        if (rope.IsFullyWrappedAround(this.transform)) {
            // Debug.Log("wrapped around pillar");
            gate.Open();
        }
        else {
            // Debug.Log("not wrapped");
            gate.Close();
        }
    }
}
