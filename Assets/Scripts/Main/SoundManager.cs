using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioMixer audioMixer;

    // Set player saved volumes
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
    }

    //creates, plays and destroys audio clips
    public void PlaySoundClip(AudioClip clip, Transform spawntransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundEffect, spawntransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    //Scripts to set different volumes
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("soundFXVolume", volume);
        PlayerPrefs.SetFloat("soundFXVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume"));
        audioMixer.SetFloat("soundFXVolume", PlayerPrefs.GetFloat("soundFXVolume"));
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
    }
}
