using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource, sfxSource;
    [SerializeField] private AudioClip[] music, sfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("07. They Might As Well Be Dead");
    }

    public void PlayMusic(string clipName)
    {
        AudioClip clipToPlay = Array.Find(music, x => x.name == clipName);
        if (clipToPlay != null)
        {
            musicSource.clip = clipToPlay;
            musicSource.Play();
            Debug.Log($"Playing Song: {clipToPlay.name}");
        }
    }

    public void PlaySfx(string clipName)
    {
        AudioClip clipToPlay = Array.Find(sfx, x => x.name == clipName);

        if (clipToPlay != null)
        {
            sfxSource.PlayOneShot(clipToPlay);
        }
    }
}