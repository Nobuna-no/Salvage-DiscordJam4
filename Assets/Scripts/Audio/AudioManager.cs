using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;




public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct SchedulingEvents
    {
        public UnityEvent OnStartup;
        public UnityEvent OnDestroy;
    }



    [System.Serializable]
    public struct RandomAudioPack
    {
        public List<AudioClip> AudioClips;
        [SerializeField]
        private float RandomSoundDelayMin;
        [SerializeField]
        private float RandomSoundDelayMax;
        public float SoundDelay { get { return Random.Range(RandomSoundDelayMin, RandomSoundDelayMax); } }
        public bool CanPlaySound { get; set; }

        public void PlayRandomAudio(Vector3 location)
        {
            if (CanPlaySound)
            {
                CanPlaySound = false;
                AudioSource.PlayClipAtPoint(AudioClips[Random.Range(0, AudioClips.Count)], location);
            }
        }
    }


    private static AudioManager instance;
    public static AudioManager Instance
    {
        get 
        { 
            if(instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if(!instance)
                {
                    instance = new AudioManager();
                }
            }
            return instance;
        }
    }


    [Header("AUDIO- Required")]
    [SerializeField]
    private AudioSource PlayerAudioSource;
    [SerializeField]
    private SchedulingEvents Events;

    [Header("AUDIO- Human")]
    [SerializeField]
    private RandomAudioPack DyingHuman;
    [SerializeField]
    private RandomAudioPack YellingHuman;


    public void Start()
    {
        Events.OnStartup?.Invoke();
        if (PlayerAudioSource)
        {
            PlayerAudioSource = FindObjectOfType<IsometricPlayerMovementController>().GetComponent<AudioSource>();
        }


        if (!PlayerAudioSource)
        {
            Debug.LogError("No audio source given to " + this);
            Debug.Break();
        }

        DyingHuman.CanPlaySound = true;
        YellingHuman.CanPlaySound = true;
    }

    public void OnDestroy()
    {
        Events.OnDestroy?.Invoke();
    }

    public void PlayLoopSound(AudioClip sound)
    {
        PlayerAudioSource.clip = sound;
        PlayerAudioSource.loop = true;
        PlayerAudioSource?.Play();
    }

    public void StopLoopSound()
    {
        PlayerAudioSource.loop = false;
        PlayerAudioSource?.Stop();
    }

    public void PlayHumanDyingRandomAudio(Vector3 location)
    {
        if (DyingHuman.CanPlaySound)
        {
            DyingHuman.PlayRandomAudio(location);
            StartCoroutine(DyingPlaySoundDelay_Coroutine());
        }
    }


    public void PlayHumanYellingRandomAudio(Vector3 location)
    {
        if (YellingHuman.CanPlaySound)
        {
            YellingHuman.PlayRandomAudio(location);
            StartCoroutine(YellingPlaySoundDelay_Coroutine());
        }
    }


    IEnumerator DyingPlaySoundDelay_Coroutine()
    {
        yield return new WaitForSeconds(DyingHuman.SoundDelay);
        DyingHuman.CanPlaySound = true;
    }

    IEnumerator YellingPlaySoundDelay_Coroutine()
    {
        yield return new WaitForSeconds(YellingHuman.SoundDelay);
        YellingHuman.CanPlaySound = true;
    }
}
