using UnityEngine;

public class ProjectileSensor : MonoBehaviour
{
    public Gate gate;
    public float closeDelay = 6f;
    private float timeSinceLastHit = 0f;
    private bool gateIsOpen = false;

    void Update()
    {
        timeSinceLastHit += Time.deltaTime;

        if (timeSinceLastHit >= closeDelay && gateIsOpen)
        {
            gate.Close();
            gateIsOpen = false;
        }
    }

    public void RegisterBulletHit()
    {
        timeSinceLastHit = 0f;

        if (!gateIsOpen)
        {
            gate.Open();
            gateIsOpen = true;
        }
    }

}
