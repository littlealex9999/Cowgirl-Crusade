using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]


public class PlayRandomizedSound : MonoBehaviour
{
    private AudioSource audioSource;


    [SerializeField] AudioClip[] soundVariations;


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

        if(soundVariations.Length != 0)
        {
            audioSource.clip = soundVariations[Random.Range(0, soundVariations.Length - 1)];
            
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
           // Debug.Log("There were no audio clips assigned to " + gameObject.name + "'s SoundVariations array or audiosource.");
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
