using UnityEngine;
using System.Collections;

public class Lever : Pullable
{
    bool wasWrapped = false;

    private PlayerController player;
    private Animator animator;

    void Start() {
        player = FindFirstObjectByType<PlayerController>();
        animator = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        bool isWrapped = rope.IsFullyWrappedAround(this.transform);

        if (isWrapped && !wasWrapped)
        {
            StartCoroutine(PlayPullSequence()); 
            player.Pull();
            ToggleGates();
        }

        wasWrapped = isWrapped;
    }

    private IEnumerator PlayPullSequence()
    {
        animator.SetTrigger("pull");

        yield return new WaitForSeconds(1.3f);

        animator.SetTrigger("unpull");
    }
}
