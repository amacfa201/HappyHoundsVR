using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] soundFXArray;

	// Use this for initialization
	void Awake () {
        foreach (Sound _sound in soundFXArray)
        {
            _sound.audioSource = gameObject.AddComponent<AudioSource>();
            _sound.audioSource.clip = _sound.audioClip;
            _sound.audioSource.loop = _sound.loopSound;
            _sound.audioSource.playOnAwake = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    

    //public void StopSound(string soundName)
    //{
    //    Sound s = Array.Find(soundFXArray, sound => sound.clipName == soundName);
    //    if (s == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        s.audioSource.Stop();
    //    }
    //}

    public void PlayOnce(string soundName)
    {
        Sound currentSound = Array.Find(soundFXArray, mySound => mySound.clipName == soundName);
        if (currentSound == null)
        {
            return;
        }
        else
        {
            if (!currentSound.audioSource.isPlaying)
            {
                currentSound.audioSource.PlayOneShot(currentSound.audioClip);
            }  
        }
    }


    public void PlaySoundFX(string soundName)
    {
        Sound currentSound = Array.Find(soundFXArray, mySound => mySound.clipName == soundName);
        if (currentSound == null)
        {
            print("null");
            return;
        }
        else
        {
            currentSound.audioSource.Play();
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
