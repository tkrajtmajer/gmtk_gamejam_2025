using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class InputSystem : MonoBehaviour
{
    public GameObject HUDCanvas;
    public GameObject pauseCanvas;
    public GameObject pauseContents;
    public GameObject mapCanvas;
    public GameObject mapContents; 
    public Animator scrollAnimator;
    public Animator bookAnimator;

    private CanvasGroup cg;
    private CanvasGroup cgPause;

    public float animationDuration = 1f;

    private bool isMapOpen = false;
    private bool isTransitioning = false;

    private bool isPauseOpen = false;
    private bool isTransitioningPause = false;
    public float animationDurationPause = 1.1f;

    void Start()
    {
        HUDCanvas.SetActive(true);

        cg = mapCanvas.GetComponent<CanvasGroup>();
        cgPause = pauseCanvas.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cgPause.alpha = 0;
        mapContents.SetActive(false);
        pauseContents.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isTransitioning && !isPauseOpen)
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

        if (Input.GetKeyDown(KeyCode.P) && !isTransitioningPause && !isMapOpen)
        {
            if (!isPauseOpen)
            {
                Pause();
            }
            else
            {
                Unpause();
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

    System.Collections.IEnumerator ShowContentsAfterDelayPause()
    {
        yield return new WaitForSecondsRealtime(animationDurationPause);
        pauseContents.SetActive(true);
        isTransitioningPause = false;
        Time.timeScale = 0f; // Pause game
    }

    System.Collections.IEnumerator HideCanvasAfterDelayPause()
    {
        yield return new WaitForSecondsRealtime(animationDurationPause);
        cgPause.alpha = 0;
        HUDCanvas.SetActive(true);
        isTransitioningPause = false;
    }

    void Pause() {
        isTransitioningPause = true;
        // mapCanvas.SetActive(true);
        cgPause.alpha = 1;
        HUDCanvas.SetActive(false);
        bookAnimator.SetBool("isOpen", true);

        // Delay showing contents until after the animation
        StartCoroutine(ShowContentsAfterDelayPause());
        isPauseOpen = true;
    }

    public void Unpause() {
        isTransitioningPause = true;
        bookAnimator.SetBool("isOpen", false);
        pauseContents.SetActive(false);

        Time.timeScale = 1f; 
        // Delay hiding canvas & resuming game
        StartCoroutine(HideCanvasAfterDelayPause());
        isPauseOpen = false;
    }

    public void LoadMenu() {
        Debug.Log("load menu");
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame() {
        Debug.Log("quit game");
        Application.Quit();
    }
}
