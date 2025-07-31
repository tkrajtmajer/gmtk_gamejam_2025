using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rope : MonoBehaviour
{
    public Transform player;
    public Transform hook;
    public LayerMask obstacles;

    public Transform pillar;

    public float maxLength = 100f;
    public TMP_Text text;

    public float breakpointThresh = 0.001f;

    private LineRenderer lineRenderer;
    private List<Vector2> ropePositions = new List<Vector2>();

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        ropePositions.Add(hook.position);
        ropePositions.Add(player.position);
    }

    void Update() {
        float ropeLength = GetRopeLength(ropePositions[ropePositions.Count - 1]);
        text.text = (maxLength - ropeLength).ToString();

        UpdateRopePositions();
        CheckWrapping();
        UnwrapIfPossible();

        if (IsFullyWrappedAround(pillar)) {
            Debug.Log("wrapped around pillar");
        }
        else {
            Debug.Log("not wrapped");
        }
    }

    void UpdateRopePositions() {
        // Debug.Log(ropePositions.Count);
        // Debug.Log(lineRenderer.positionCount);
        ropePositions[ropePositions.Count - 1] = player.position;

        lineRenderer.positionCount = ropePositions.Count;
        for (int i = 0; i < ropePositions.Count; i++)
        {
            lineRenderer.SetPosition(i, ropePositions[i]);
        }
    }

    void CheckWrapping() {
        Vector2 lastPoint = ropePositions[ropePositions.Count - 2];
        Vector2 dir = lastPoint - (Vector2)player.position;
        float dist = Vector2.Distance((Vector2)player.position, lastPoint);
        
        // Debug.DrawLine((Vector2)player.position, lastPoint, Color.blue, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)player.position, dir.normalized, dist + 0.05f, obstacles);

        if (hit.collider != null) {
            Vector2 hitPoint = hit.point;
            Vector2 normal = hit.normal;

            float offset = 0.05f;
            Vector2 offsetHitPoint = hitPoint + normal * offset;

            if (Vector2.Distance(hitPoint, lastPoint) > breakpointThresh) {
                ropePositions.Insert(ropePositions.Count - 1, offsetHitPoint);
            }
        }
    }

    void UnwrapIfPossible() {
        while (ropePositions.Count >= 3) {
            Vector2 candidate = ropePositions[ropePositions.Count - 3];

            RaycastHit2D hit = Physics2D.Linecast(player.position, candidate, obstacles);

            if (hit.collider == null) {
                ropePositions.RemoveAt(ropePositions.Count - 2);
            }
            else break;
        }
    }

    public float GetRopeLength(Vector2 playerPos) {
        float length = 0f;

        if (ropePositions.Count < 2) return 0f;

        length += Vector2.Distance(playerPos, ropePositions[ropePositions.Count - 2]);

        for (int i = ropePositions.Count - 2; i > 0; i--) {
            length += Vector2.Distance(ropePositions[i], ropePositions[i - 1]);
        }

        return length;
    }

    public Vector2 GetLastPoint() {
        return ropePositions[ropePositions.Count - 2];
    }


    public bool IsFullyWrappedAround(Transform target) {
        float angleSum = 0f;
        Vector2 center = target.position;

        for (int i = 0; i < ropePositions.Count - 1; i++) {
            Vector2 a = ropePositions[i] - center;
            Vector2 b = ropePositions[i + 1] - center;
            float angle = Vector2.SignedAngle(a, b);
            angleSum += angle;
        }

        return Mathf.Abs(angleSum) > 355f; 
    }
}
