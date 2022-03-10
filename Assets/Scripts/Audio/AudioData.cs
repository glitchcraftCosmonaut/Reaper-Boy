using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : Singleton<AudioData>
{
    [SerializeField] AudioSource sFXPlayer;

    public void PlaySFX(AudioDatas audioDatas)
    {
        // sFXPlayer.clip = audioClip;
        // sFXPlayer.volume = volume;
        // sFXPlayer.Play();
        sFXPlayer.PlayOneShot(audioDatas.audioClip, audioDatas.volume);
    }
    // public void PlayMusic(AudioDatas audioDatas)
    // {
    //     musicPlayer.PlayOneShot(audioDatas.backgroundMusic, audioDatas.volume);
    // }
    
}

[System.Serializable]
public class AudioDatas
{
    public AudioClip audioClip;
    public float volume;

}

