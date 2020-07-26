using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> RandomSound;
    [SerializeField]
    private float RandomSoundRangeDelayMin = 3f;
    [SerializeField]
    private float RandomSoundRangeDelayMax = 10f;
    private bool canPlayRandomSound = true;
    
    [SerializeField]
    AudioSource Source;
    [SerializeField]
    private bool PlayInfiniteRandomSound = false;


    private void Start()
    {
        if(PlayInfiniteRandomSound)
        {
            StartInfiniteRandomPlaySound();
        }
    }

    public void PlayRandomSound()
    {
        if(canPlayRandomSound)
        {
            canPlayRandomSound = false;
            Source?.PlayOneShot(RandomSound[Random.Range(0, RandomSound.Count)]);
            StartCoroutine(RandomPlaySoundDelay_Coroutine());
        }
    }

    public void PlayOneShot(AudioClip sound)
    {
        Source?.PlayOneShot(sound);
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


    IEnumerator RandomPlaySoundDelay_Coroutine()
    {
        yield return new WaitForSeconds(Random.Range(RandomSoundRangeDelayMin, RandomSoundRangeDelayMax));
        canPlayRandomSound = true;
    }



    public void StartInfiniteRandomPlaySound()
    {
        PlayInfiniteRandomSound = true;
        StartCoroutine(InfinitePlaySoundDelay_Coroutine());
    }
    public void StopInfiniteRandomPlaySound()
    {
        PlayInfiniteRandomSound = false;
    }
   
    IEnumerator InfinitePlaySoundDelay_Coroutine()
    {
        while(PlayInfiniteRandomSound)
        {
            yield return new WaitForSeconds(Random.Range(RandomSoundRangeDelayMin, RandomSoundRangeDelayMax));
            Source?.PlayOneShot(RandomSound[Random.Range(0, RandomSound.Count)]);
        }
    }
}
