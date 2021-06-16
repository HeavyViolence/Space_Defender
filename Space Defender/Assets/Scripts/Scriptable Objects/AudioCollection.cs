using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Audio Collection", menuName = "Configs/Audio Collection")]
public class AudioCollection : ScriptableObject
{
    public const int MaxPriority = 256; 

    [SerializeField] private List<AudioClip> _clips = null;

    [SerializeField] private AudioMixerGroup _group = null;

    [SerializeField] private float _volume = 0f;
    [SerializeField] private float _volumeRandom = 0f;

    [SerializeField] private int _priority = 0;

    [SerializeField] private float _spatialBlend = 0f;

    private int _selectedClipIndex = -1;

    public AudioMixerGroup Group => _group;

    public float Volume => _volume == 1f ? AuxMath.RandomizeDown(_volume, _volumeRandom) :
                                           AuxMath.Randomize(_volume, _volumeRandom);

    public int Priority => _priority;

    public float SpatialBlend => _spatialBlend;

    public void PlayRandomClip(Vector3 pos)
    {
        AudioData data = new AudioData(GetRandomClip(), Group, Volume, Priority, SpatialBlend, pos);
        AudioPlayer.Instance.PlayAudio(data);
    }

    public void PlayNextClip(Vector3 pos)
    {
        AudioData data = new AudioData(GetNextClip(), Group, Volume, Priority, SpatialBlend, pos);
        AudioPlayer.Instance.PlayAudio(data);
    }

    private AudioClip GetRandomClip() => _clips[Random.Range(0, _clips.Count)];

    private AudioClip GetNextClip()
    {
        if (_selectedClipIndex == -1) _selectedClipIndex = Random.Range(0, _clips.Count);

        return _clips[(int)Mathf.Repeat(_selectedClipIndex++, _clips.Count - 1f)];
    }
}
