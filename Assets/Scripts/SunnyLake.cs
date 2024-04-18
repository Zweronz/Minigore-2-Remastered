using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyLake : Level
{
    protected override AudioClip GetMusic()
    {
        return SoundManager.Load("Music/SunnyLake");
    }

    public SunnyLake() : base("TerrainSunnyLake")
    {
    }
}
