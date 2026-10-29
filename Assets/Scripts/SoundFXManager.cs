using System;
using Unity.Mathematics;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFX(AudioClip audioClip, Transform objectTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, objectTransform.position, quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        
        Destroy(audioSource.gameObject, clipLength);
    }
    
}
