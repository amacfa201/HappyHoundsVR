using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public Sound[] soundArray;

	// Use this for initialization
	void Awake () {
        foreach (Sound _sound in soundArray)
        {
            _sound.source = gameObject.AddComponent<AudioSource>();
            _sound.source.clip = _sound.clip;
            _sound.source.loop = _sound.loop;
            _sound.source.playOnAwake = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(string soundName)
    {
       Sound s = Array.Find(soundArray, sound => sound.name == soundName);
        if (s == null)
        {
            print("null");
            return;
        }
        else
        {
            print("sound should play");
            s.source.Play();
        }
    }

    public void StopSound(string soundName)
    {
        Sound s = Array.Find(soundArray, sound => sound.name == soundName);
        if (s == null)
        {
            return;
        }
        else
        {
            s.source.Stop();
        }
    }

    public void PlayOnce(string soundName)
    {
        Sound s = Array.Find(soundArray, sound => sound.name == soundName);
        if (s == null)
        {
            print("null");
            return;
        }
        else
        {
            print("sound should play");
            if (!s.source.isPlaying)
            {
                s.source.PlayOneShot(s.clip);
            }
           
        }
    }

    //public void StopAllSFX()
    //{
    //    foreach (Sound _sound in soundArray)
    //    {
    //        _sound.audioSource.gameObject.AddComponent<AudioSource>();
    //        _sound.audioSource.Stop();   
    //    }

    //}



}
