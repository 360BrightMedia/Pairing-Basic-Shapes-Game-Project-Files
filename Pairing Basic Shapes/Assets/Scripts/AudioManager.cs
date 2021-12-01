using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	// singleton
	public static AudioManager instance;

	public Sound[] sounds;

	private Sound isPlaying;
	private Sound isPlaying2;
	
	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.outputAudioMixerGroup = s.audioMixerGroup;
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
	}
    private void Start()
    {
		Play("BackgroundMusic", 0.5f);
	}

    // Plays the audio with the name in sound variable
    public float Play(string sound, float volume = 0.83f, bool replay = false)
	{
		Debug.Log("Playing " + sound);
		
		isPlaying = Array.Find(sounds, item => item.name == sound);
		if (isPlaying == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return 0;
		}
		if(isPlaying.source.isPlaying && !replay)
        {
			return 0;
        }
		if (isPlaying.source.isPlaying && replay)
		{
			isPlaying.source.Stop();
		}
		isPlaying.source.volume = volume;
		

		isPlaying.source.Play();
		if (sound != "BackgroundMusic")
		{
			isPlaying2 = isPlaying;
		}
		return isPlaying.source.clip.length;

	}

	public float PlayMenuAudio(string sound, float volume = 0.83f)
	{
		Sound soundToPlay;
		soundToPlay = Array.Find(sounds, item => item.name == sound);
		if (soundToPlay == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return 0;
		}
		if (isPlaying.source.isPlaying)
		{
			return 0;
		}
		soundToPlay.source.volume = volume;


		soundToPlay.source.Play();
		
		return soundToPlay.source.clip.length;
	}



	public void DecreaseBackgroundMusicVolume()
    {
		isPlaying = Array.Find(sounds, item => item.name == "BackgroundMusic");
		if (isPlaying == null)
		{
			return;
		}
		isPlaying.source.volume -= 0.30f;
	}

	public void SetBackgroundMusicVolume(float f)
    {
		isPlaying = Array.Find(sounds, item => item.name == "BackgroundMusic");
		if (isPlaying == null)
		{
			return;
		}
		isPlaying.source.volume = f;
	}

	public void IncreaseBackgroundMusicVolume()
	{
		isPlaying = Array.Find(sounds, item => item.name == "BackgroundMusic");
		isPlaying.source.volume += 0.30f;
	}

	public void PauseAudio()
    {
		if (isPlaying2 != null)
		{
			if (isPlaying2.source.isPlaying)
			{
				isPlaying2.source.Pause();
			}
			else
            {
				isPlaying2 = null;
            }
		}
	}

	public void ContinueAudio()
    {
		if (isPlaying2 != null)
		{
			isPlaying2.source.Play();
		}
	}
	public void StopCurrentAudio()
    {
		if (isPlaying2 != null)
		{
			isPlaying2.source.Stop();
			isPlaying2 = null;
		}
	}

	public void StopAllAudioExceptBackground()
    {
		foreach (Sound s in sounds)
		{
			if(s.name != "BackgroundMusic")
            {
				s.source.Stop();
            }
		}
	}

	public void Stop(string sound)
    {
		Sound soundToStop;
		soundToStop = Array.Find(sounds, item => item.name == sound);
		if (soundToStop == null)
		{
			return;
		}
		soundToStop.source.Stop();
    }
}
