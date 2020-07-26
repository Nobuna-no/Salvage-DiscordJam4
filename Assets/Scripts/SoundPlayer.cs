using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> Sounds;
    [SerializeField]
    private float RandomSoundRangeDelayMin = 3f;
    [SerializeField]
    private float RandomSoundRangeDelayMax = 10f;


    [SerializeField]
    AudioSource Source;

    public void PlayMusic()
    {
        Source?.PlayOneShot(Sounds[Random.Range(0, Sounds.Count)]);
    }

    public void PlayOneMusic()
    {
        Source?.PlayOneShot(Sounds[0]);
    }


    public void PlayLoopSound(AudioClip sound)
    {
        Source.clip = sound;
        Source.loop = true;
        Source?.Play();
    }

    public void StopLoopSound()
    {
        Source.loop = false;
        Source?.Stop();
    }
}
