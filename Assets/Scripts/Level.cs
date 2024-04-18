using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : ModelBase
{
    protected AudioSource musicSource;

    protected virtual AudioClip GetMusic()
    {
        return null;
    }

    public Level(string name) : base(name)
    {
        musicSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.clip = GetMusic();

        musicSource.Play();
    }
}
