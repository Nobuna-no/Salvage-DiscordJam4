using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> Sounds;

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
}
