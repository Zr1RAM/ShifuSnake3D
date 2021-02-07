using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField]
    AudioSource BGMAudioSource;
    [SerializeField]
    AudioSource SoundEffectAudioSource;
    [SerializeField]
    AudioClip WallBounceSound;
    [SerializeField]
    AudioClip ScoredSound;
    [SerializeField]
    AudioClip PizzaLostSound;
    public void PlaySoundEffect(AudioClip val)
    {
        SoundEffectAudioSource.clip = val;
        SoundEffectAudioSource.Play();
    }
    public void PlayWallBounceSound()
    {
        PlaySoundEffect(WallBounceSound);
    }
    public void ToggleBGM(bool val)
    {
        if(val)
        {
            BGMAudioSource.Play();
        }
        else
        {
            BGMAudioSource.Stop();
        }
    }  
    public void PlayScoredSound()
    {
        PlaySoundEffect(ScoredSound);
    }
    public void PlayPizzaLostSound()
    {
        PlaySoundEffect(PizzaLostSound);
    }
}
