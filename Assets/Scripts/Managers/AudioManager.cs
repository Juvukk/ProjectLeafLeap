using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectsSource;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        effectsSource = GetComponentInChildren<AudioSource>();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Play effects
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySFX(AudioClip clip)
    {
        if (effectsSource.isPlaying)
        {
            return;
            // i've turned this off
            // because with all of the sounds now in the game
            // having them play one after another gets really annoying sorry!
            // StartCoroutine(WaitForEffectsToFinish(clip));
        }
        else if (!effectsSource.isPlaying)
        {
            effectsSource.clip = clip;
            effectsSource.Play();
        }
    }

    private IEnumerator WaitForEffectsToFinish(AudioClip clip)
    {
        // If an existing sound is playing then wait until it's finished
        yield return new WaitWhile(() => effectsSource.isPlaying);

        effectsSource.clip = clip;
        effectsSource.Play();

        yield return null;
    }
}