using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] private AudioClip themeStart;
    [SerializeField] private AudioClip themeLoop;

    private bool started = false;

    private void Update()
    {
        if (!source.isPlaying)
        {
            if (!started)
            {
                source.PlayOneShot(themeStart);
                started = true;
            }
            else source.PlayOneShot(themeLoop);
        }
    }

}
