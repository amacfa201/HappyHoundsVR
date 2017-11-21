using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public Sound[] soundArray;

	// Use this for initialization
	void Awake () {
        foreach (Sound _sound in soundArray)
        {
            _sound.audioSource.gameObject.AddComponent<AudioSource>();
            _sound.audioSource.clip = _sound.clip;
            _sound.audioSource.loop = _sound.loop;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(string soundName)
    {
       Sound s = Array.Find(soundArray, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        else
        {
            s.audioSource.Play();
        }
    }

    public void StopSound(string soundName)
    {
        Sound s = Array.Find(soundArray, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        else
        {
            s.audioSource.Stop();
        }
    }

    public void StopAllSFX()
    {
        foreach (Sound _sound in soundArray)
        {
            _sound.audioSource.gameObject.AddComponent<AudioSource>();
            _sound.audioSource.Stop();   
        }

    }



}
