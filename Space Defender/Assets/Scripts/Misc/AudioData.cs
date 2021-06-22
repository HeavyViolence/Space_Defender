using UnityEngine;
using UnityEngine.Audio;

public class AudioData
{
    public AudioClip Clip { get; private set; } = null;
    public AudioMixerGroup Group { get; private set; } = null;
    public float Volume { get; private set; } = 0f;
    public int Priority { get; private set; } = 0;
    public float SpatialBlend { get; private set; } = 0f;
    public Vector3 PlayPos { get; private set; } = Vector3.zero;

    public AudioData(AudioClip clip,
                     AudioMixerGroup group,
                     float volume,
                     int priority,
                     float spatialBlend,
                     Vector3 playPos)
    {
        Clip = clip;
        Group = group;
        Volume = volume;
        Priority = priority;
        SpatialBlend = spatialBlend;
        PlayPos = playPos;
    }
}
