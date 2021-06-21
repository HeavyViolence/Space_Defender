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

    private int _nextClipIndex = -1;

    private readonly List<int> _selectedClipsIndexers = new List<int>();

    private int RandomClipIndex
    {
        get
        {
            int index = Random.Range(0, _clips.Count);
            RememberSelectedClipIndex(index);

            return index;
        }
    }

    private int RandomClipIndexUnrepeated
    {
        get
        {
            int index = AuxMath.GetRandomInRangeWithExceptions(0, _clips.Count, _selectedClipsIndexers.ToArray());
            RememberSelectedClipIndex(index);

            return index;
        }
    }

    private int NextClipIndex
    {
        get
        {
            if (_nextClipIndex == -1) _nextClipIndex = _clips.Count;

            int index = _nextClipIndex++ % _clips.Count;
            RememberSelectedClipIndex(index);

            return index;
        }
    }

    public AudioMixerGroup Group => _group;

    public float Volume => _volume == 1f ? AuxMath.RandomizeDown(_volume, _volumeRandom)
                                         : AuxMath.Randomize(_volume, _volumeRandom);

    public int Priority => _priority;

    public float SpatialBlend => _spatialBlend;

    public void PlayRandomClip(Vector3 pos)
    {
        AudioData data = new AudioData(GetRandomClip(), Group, Volume, Priority, SpatialBlend, pos);
        AudioPlayer.Instance.PlayAudio(data);
    }

    public void PlayRandomClipUnrepeatedly(Vector3 pos)
    {
        AudioData data = new AudioData(GetRandomClipUnrepeated(), Group, Volume, Priority, SpatialBlend, pos);
        AudioPlayer.Instance.PlayAudio(data);
    }

    public void PlayNextClip(Vector3 pos)
    {
        AudioData data = new AudioData(GetNextClip(), Group, Volume, Priority, SpatialBlend, pos);
        AudioPlayer.Instance.PlayAudio(data);
    }

    private AudioClip GetRandomClip() => _clips[RandomClipIndex];

    private AudioClip GetRandomClipUnrepeated() => _clips[RandomClipIndexUnrepeated];

    private AudioClip GetNextClip() => _clips[NextClipIndex];

    private void RememberSelectedClipIndex(int index)
    {
        if (_selectedClipsIndexers.Count == _clips.Count - 1)
            _selectedClipsIndexers.Clear();

        if (!_selectedClipsIndexers.Contains(index))
            _selectedClipsIndexers.Add(index);
    }
}
