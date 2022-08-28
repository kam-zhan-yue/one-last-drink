using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds = Array.Empty<Sound>();

    private void Awake()
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.pitch = sounds[i].pitch;
        }
    }

    public void Play(string _name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].source.Play();
            }
        }
    }
}
