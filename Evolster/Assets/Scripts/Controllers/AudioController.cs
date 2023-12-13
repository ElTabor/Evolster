using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioClip[] tracks;
    AudioSource source;
    public float sfxVolume;
    public float musicVolume;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clipToPlay)
    {
        source.Stop();
        source.PlayOneShot(clipToPlay, musicVolume);
    }

    public void SetVolume(string valueToChange)
    {
        if (valueToChange == "SFX") sfxVolume = UIManager.instance.SFXVolumeBar.value/10;
        if (valueToChange == "Music") musicVolume = UIManager.instance.MusicVolumeBar.value/10;
    }
}
