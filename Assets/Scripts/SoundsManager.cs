using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField]
    AudioSource BGMAudioSource;
    [SerializeField]
    AudioSource SoundEffectAudioSource;
    public void PlaySoundEffect(AudioClip val)
    {
        SoundEffectAudioSource.clip = val;
        SoundEffectAudioSource.Play();
    }
}
