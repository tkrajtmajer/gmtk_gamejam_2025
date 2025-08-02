using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputSystem : MonoBehaviour
{
    public GameObject HUDCanvas;
    public GameObject pauseCanvas;
    public GameObject mapCanvas;
    public GameObject mapContents; 
    public Animator scrollAnimator;

    private CanvasGroup cg;

    public float animationDuration = 1f;

    private bool isMapOpen = false;
    private bool isTransitioning = false;

    void Start()
    {
        HUDCanvas.SetActive(true);

        cg = mapCanvas.GetComponent<CanvasGroup>();
        // mapCanvas.SetActive(false);
        cg.alpha = 0;
        mapContents.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isTransitioning)
        {
            if (!isMapOpen)
            {
                OpenMap();
            }
            else
            {
                CloseMap();
            }
        }
    }

    void OpenMap()
    {
        isTransitioning = true;
        // mapCanvas.SetActive(true);
        cg.alpha = 1;
        HUDCanvas.SetActive(false);
        scrollAnimator.SetBool("isOpen", true);

        // Delay showing contents until after the animation
        StartCoroutine(ShowContentsAfterDelay());
        isMapOpen = true;
    }

    void CloseMap()
    {
        isTransitioning = true;
        scrollAnimator.SetBool("isOpen", false);
        mapContents.SetActive(false);

        Time.timeScale = 1f; 
        // Delay hiding canvas & resuming game
        StartCoroutine(HideCanvasAfterDelay());
        isMapOpen = false;
    }

    System.Collections.IEnumerator ShowContentsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(animationDuration);
        mapContents.SetActive(true);
        isTransitioning = false;
        Time.timeScale = 0f; // Pause game
    }

    System.Collections.IEnumerator HideCanvasAfterDelay()
    {
        yield return new WaitForSecondsRealtime(animationDuration);
        // mapCanvas.SetActive(false);
        cg.alpha = 0;
        HUDCanvas.SetActive(true);
        isTransitioning = false;
    }
}
