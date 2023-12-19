using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioClip[] tracks;
    private AudioSource _source;
    public float sfxVolume;
    public float musicVolume;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _source.volume = musicVolume;
    }

    public void PlayMusic(AudioClip clipToPlay)
    {
        _source.Stop();
        _source.PlayOneShot(clipToPlay, musicVolume);
    }

    public void SetVolume(string valueToChange)
    {
        switch (valueToChange)
        {
            case "SFX":
                sfxVolume = UIManager.instance.SFXVolumeBar.value/10;
                break;
            case "Music":
                musicVolume = UIManager.instance.MusicVolumeBar.value/10;
                break;
        }
    }
}
