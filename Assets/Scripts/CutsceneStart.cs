using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutsceneStart : MonoBehaviour
{
    public float delayAfterEachLine = 3f;
    public GameObject mainCam;
    public GameObject playerCam;

    public CanvasGroup dialogueBox;
    public TMP_Text playerText;
    public TMP_Text wizardText;
    public Animator wizardAnimator;

    public float cameraMoveDuration = 1.5f;
    public float zoomOutDuration = 2f;
    public float textSpeed = 0.05f;

    public Transform playerFocus;
    public Transform wizardFocus;

    public PlayerController player;

    [TextArea]
    public string[] dialogueLines;

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        // yield return new WaitForSeconds(delayAfterEachLine);
        playerCam.SetActive(false);
        mainCam.SetActive(true);
        dialogueBox.alpha = 0;
        playerText.text = "";
        wizardText.text = "";
        player.inCutscene = true;

        yield return new WaitForSeconds(2f);

        // yield return StartCoroutine(PanCameraTo(playerFocus.position));

        dialogueBox.alpha = 1;
        yield return StartCoroutine(TypeText("hmmmphh.....", playerText));
        yield return new WaitForSeconds(delayAfterEachLine);
        playerText.text = "";
        dialogueBox.alpha = 0;

        yield return StartCoroutine(PanCameraTo(wizardFocus.position));

        dialogueBox.alpha = 1;
        foreach (string line in dialogueLines)
        {
            wizardAnimator.SetBool("talking", true);
            yield return StartCoroutine(TypeText(line, wizardText));
            wizardAnimator.SetBool("talking", false);
            yield return new WaitForSeconds(delayAfterEachLine);
            wizardText.text = "";
        }

        dialogueBox.alpha = 0;

        yield return StartCoroutine(PanCameraTo(playerFocus.position));

        dialogueBox.alpha = 1;
        yield return StartCoroutine(TypeText("hmgh....", playerText));
        yield return new WaitForSeconds(delayAfterEachLine);
        playerText.text = "";
        dialogueBox.alpha = 0;

        yield return StartCoroutine(ZoomOutCamera(5, 8, zoomOutDuration));
        mainCam.SetActive(false);
        playerCam.SetActive(true);
        player.inCutscene = false;

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); 
    }

    IEnumerator PanCameraTo(Vector3 targetPosition)
    {
        Vector3 startPos = mainCam.transform.position;
        Vector3 endPos = new Vector3(targetPosition.x, targetPosition.y, startPos.z);

        float elapsed = 0f;

        while (elapsed < cameraMoveDuration)
        {
            mainCam.transform.position = Vector3.Lerp(startPos, endPos, elapsed / cameraMoveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCam.transform.position = endPos;
    }

    IEnumerator TypeText(string line, TMP_Text dialogueText)
    {
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator ZoomOutCamera(float startSize, float endSize, float duration)
    {
        float elapsed = 0f;

        Camera cam = mainCam.GetComponent<Camera>();

        while (elapsed < duration)
        {
            cam.orthographicSize = Mathf.Lerp(startSize, endSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.orthographicSize = endSize;
    }
}
