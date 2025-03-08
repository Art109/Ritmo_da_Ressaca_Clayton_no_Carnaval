using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    public AudioSource music;

    public void Skip()
    {
        Time.timeScale = 1;

        if (music != null)
            music.Play();
    }
}
