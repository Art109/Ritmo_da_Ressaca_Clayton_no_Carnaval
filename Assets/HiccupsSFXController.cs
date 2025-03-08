using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiccupsSFXController : MonoBehaviour
{
    AudioSource hiccups_AudioSource;
    [SerializeField] List<AudioClip> hiccups_AudioClipList;

    float timer;

    private void Start()
    {
        hiccups_AudioSource = GetComponent<AudioSource>();
        timer = Random.Range(2, 4);
    }
    void Update()
    {
        Hiccups();
    }

    void Hiccups()
    {
        timer -= Time.deltaTime;
        if (timer < 0) 
        { 
            PlayClip();
            timer = Random.Range(2, 4);
        }
    }

    void PlayClip()
    {
        PitchModulator();
        int indexClip = Random.Range(0, hiccups_AudioClipList.Count);
        hiccups_AudioSource.clip = hiccups_AudioClipList[indexClip];
        hiccups_AudioSource.Play();
    }

    void PitchModulator()
    {
        hiccups_AudioSource.pitch = Random.Range(0.8f, 1.2f);
    }
}
