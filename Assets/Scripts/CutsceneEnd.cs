using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneEnd : MonoBehaviour
{
    public float delayAfterEachLine = 3f;
    public GameObject mainCam;
    public GameObject playerCam;

    public CanvasGroup dialogueBox;
    public TMP_Text playerText;
    public TMP_Text wizardText;
    public Animator wizardAnimator;
    public Animator playerAnimator;

    public float cameraMoveDuration = 1.5f;
    public float zoomOutDuration = 2f;
    public float textSpeed = 0.05f;

    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;

    public Transform playerFocus;
    public Transform wizardFocus;

    public PlayerController player;

    public Button button1;
    public Button button2;
    public GameObject buttons;

    private int selectedChoice = -1;

    [TextArea]
    public string[] dialogueLines;

    void Start()
    {
        button1.onClick.AddListener(() => OnChoiceSelected(1));
        button2.onClick.AddListener(() => OnChoiceSelected(2));

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
        buttons.SetActive(false);

        yield return new WaitForSeconds(2f);

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

        yield return StartCoroutine(ShowChoices());

        if (selectedChoice == 1)
        {
            yield return StartCoroutine(SpareHim());
        }
        else if (selectedChoice == 2)
        {
            yield return StartCoroutine(KillHim());
        }

        // yield return StartCoroutine(PanCameraTo(playerFocus.position));

        // dialogueBox.alpha = 1;
        // yield return StartCoroutine(TypeText("hmgh....", playerText));
        // yield return new WaitForSeconds(delayAfterEachLine);
        // playerText.text = "";
        // dialogueBox.alpha = 0;

        // yield return StartCoroutine(ZoomOutCamera(5, 8, zoomOutDuration));
        // mainCam.SetActive(false);
        // playerCam.SetActive(true);
        // player.inCutscene = false;

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

    IEnumerator KillHim() {
        dialogueBox.alpha = 0;
        Vector3 offset = mainCam.transform.position - playerFocus.position;

        while (Vector2.Distance(playerFocus.position, wizardFocus.position) > stopDistance)
        {
            Vector2 direction = (wizardFocus.position - playerFocus.position).normalized;
            playerFocus.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            // Move camera to follow
            Vector3 desiredPos = playerFocus.position + offset;
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, desiredPos, Time.deltaTime * cameraMoveDuration);

            playerAnimator.SetFloat("speed", 1f);

            yield return null;
        }

        playerAnimator.SetFloat("speed", 0f);

        yield return new WaitForSeconds(0.3f);
        playerAnimator.SetTrigger("slash");
        yield return new WaitForSeconds(0.8f);
        wizardAnimator.SetTrigger("die");

        yield return new WaitForSeconds(2f);
        
        // wizard final dialogue

        yield return StartCoroutine(PanCameraTo(wizardFocus.position));

        string[] lines = {"But... but... knight-kun I thought we were friends bleh......"};

        dialogueBox.alpha = 1;
        foreach (string line in lines)
        {
            // wizardAnimator.SetBool("talking", true);
            yield return StartCoroutine(TypeText(line, wizardText));
            // wizardAnimator.SetBool("talking", false);
            yield return new WaitForSeconds(delayAfterEachLine);
            wizardText.text = "";
        }

        dialogueBox.alpha = 0;
    }

    IEnumerator SpareHim() {
        // no animation needed, just pan to wizard

        dialogueBox.alpha = 1;
        yield return StartCoroutine(TypeText("Hmmmmph....", playerText));
        yield return new WaitForSeconds(delayAfterEachLine);
        playerText.text = "";
        dialogueBox.alpha = 0;

        yield return StartCoroutine(PanCameraTo(wizardFocus.position));

        string[] lines = {"Ohhh how splendid", "Mother always told me this is no way to make friends", 
        "But this will show her", 
        "Come now I just made us some hot chocolate and I got the whole 3rd season of Monk on DVD"};
        
        dialogueBox.alpha = 1;
        foreach (string line in lines)
        {
            wizardAnimator.SetBool("talking", true);
            yield return StartCoroutine(TypeText(line, wizardText));
            wizardAnimator.SetBool("talking", false);
            yield return new WaitForSeconds(delayAfterEachLine);
            wizardText.text = "";
        }

        dialogueBox.alpha = 0;
    }

    IEnumerator ShowChoices()
    {
        selectedChoice = -1;

        buttons.SetActive(true);

        yield return new WaitUntil(() => selectedChoice != -1);

        buttons.SetActive(false);
    }   

    void OnChoiceSelected(int choice)
    {
        selectedChoice = choice;
    }
}
