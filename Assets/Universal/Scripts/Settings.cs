using System.Collections.Generic;
using System;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public AudioSettings audioSettings;
    public VideoSettings videoSettings;

    public void AdjustVolume(float _newVolume) => audioSettings.AdjustVolume(_newVolume);
}

[System.Serializable]
public class AudioSettings
{
    public float volume;
    public bool mute;

    public void AdjustVolume(float _newVolume) => volume = _newVolume;
}

[System.Serializable]
public class VideoSettings
{
    public float brightness;
    public float contrast;
    public bool performanceMode;
}