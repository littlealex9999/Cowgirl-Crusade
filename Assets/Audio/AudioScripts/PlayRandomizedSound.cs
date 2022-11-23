using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]


public class PlayRandomizedSound : MonoBehaviour
{
    private AudioSource audioSource;


    [SerializeField] AudioClip[] sounds;


    [Range(0f, 0.5f)]
    [SerializeField] float volumeRandomization = 0f;

    [Range(0f, 0.5f)]
    [SerializeField] float pitchRandomization = 0f;

    private float maxVolume;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxVolume = audioSource.volume;
        volumeRandomization *= maxVolume;

        if(sounds.Length != 0)
        {
            audioSource.clip = sounds[Random.Range(0, sounds.Length)];
            Debug.Log("Not equal to null");
        }
        else
        {
            Debug.Log("null");
        }
        

        if (volumeRandomization != 0)
        {
            RandomizeVolume();
        }

        if (pitchRandomization != 0)
        {
            RandomizePitch();
        }

        if(audioSource.clip != null)
        {
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }
        else
        {
            Debug.Log("Audio object was destroyed, because there was no clip assigned to its Sounds array or audiosource.");
            Destroy(gameObject);
        }
        
    }

    
    private void RandomizeVolume()
    {
        audioSource.volume = Random.Range(maxVolume - volumeRandomization, maxVolume);
    }

    private void RandomizePitch()
    {
        audioSource.pitch = Random.Range(1 - pitchRandomization, 1 + pitchRandomization);
    }

}
