using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    [SerializeField] List<AudioClip> footSteps;
    AudioSource footSource;
    bool isPlaying = false;

    void Start()
    {
        footSource = GetComponent<AudioSource>();    
    }

    public void PlayTrigger()
    {
        if(!isPlaying)
            StartCoroutine(PlayFootsteps());
    }
    IEnumerator PlayFootsteps()
    {
        isPlaying = true;
        for (int i = 0; i < footSteps.Count; i++) 
        { 
            footSource.clip = footSteps[i];
            PitchModulator();
            footSource.Play();
            yield return new WaitForSeconds(footSteps[i].length);
        }
        isPlaying = false;
    }

    void PitchModulator()
    {
        footSource.pitch = Random.Range(0.8f, 1.2f);
    }
}
