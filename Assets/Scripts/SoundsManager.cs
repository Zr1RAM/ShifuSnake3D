// The GameObject that contains this class should have Two AudioSources added to it. One for BGM and other for Sound effects.
// Some of the sounds and clips will be played from Editor and UI system. 
// This component helps the scripts side to play clips via the sound manager.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField]
    AudioSource BGMAudioSource; //Audio source dedicated to Background music 
    [SerializeField]
    AudioSource SoundEffectAudioSource; //Audio Source dedicated to playing sounds for button, score , game over, etc.
    [SerializeField]
    AudioClip WallBounceSound; // played when snake rebounds from wall
    [SerializeField]
    AudioClip ScoredSound;  //Played when player scores, in this case when snake eats pizza.
    [SerializeField]
    AudioClip PizzaLostSound; //Played when pizza timer is over and pizza is going to respawn.

    // Function for setting audio clip and playing it for Sound effect audio source
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
