using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] private AudioClip themeStart;
    [SerializeField] private AudioClip themeLoop;

    private bool started = false;

    private bool running = true;

    private void Update()
    {
        if (!source.isPlaying && running)
        {
            if (!started)
            {
                source.PlayOneShot(themeStart);
                started = true;
            }
            else source.PlayOneShot(themeLoop);
        }
    }

    public void StopMusic()
    {
        source.Stop();
        running = false;
    }

    public void SetPitch(float pitch)
    {
        source.pitch = pitch;
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayRandomSound(List<AudioClip> clips)
    {
        source.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }

}
