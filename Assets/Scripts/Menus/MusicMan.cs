using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [System.Serializable]
    public class SceneMusicPair
    {
        public int sceneBuildIndex;
        public AudioClip musicClip;
    }

    public List<SceneMusicPair> musicByScene;
    public float fadeDuration = 1.5f;

    private AudioSource audioSource;
    private int currentSceneIndex = -1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 1f;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneIndex = scene.buildIndex;
        AudioClip newTrack = GetClipForScene(currentSceneIndex);

        if (newTrack != null && newTrack != audioSource.clip)
        {
            StartCoroutine(SwitchTrack(newTrack));
        }
    }

    AudioClip GetClipForScene(int buildIndex)
    {
        foreach (var pair in musicByScene)
        {
            if (pair.sceneBuildIndex == buildIndex)
                return pair.musicClip;
        }
        return null;
    }

    IEnumerator SwitchTrack(AudioClip newClip)
    {
        float startVolume = audioSource.volume;

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume;
    }
}
