using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource, sfxSource;
    [SerializeField] private AudioClip[] music, sfx;

    private Queue<AudioClip> musicQueue;
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
        // shuffle all songs and create a queue to play them
        musicQueue = new Queue<AudioClip>(music);
        ShuffleQueue(musicQueue);
    }

    private void Update()
    {
        if (!musicSource.isPlaying && musicQueue.Count > 0)
        {
            AudioClip nextClip = musicQueue.Dequeue();
            PlayMusic(nextClip);
            musicQueue.Enqueue(nextClip);
        }
    }

    private void ShuffleQueue(Queue<AudioClip> queue)
    {
        AudioClip[] clips = queue.ToArray();
        System.Random rng = new System.Random();
        int n = clips.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            AudioClip temp = clips[n];
            clips[n] = clips[k];
            clips[k] = temp;
        }
        queue.Clear();
        foreach (var clip in clips)
        {
            queue.Enqueue(clip);
        }
    }

    public void PlayMusic(string clipName)
    {
        AudioClip clipToPlay = Array.Find(music, x => x.name == clipName);
        PlayMusic(clipToPlay);
    }

    public void PlayMusic(AudioClip clipToPlay)
    {
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