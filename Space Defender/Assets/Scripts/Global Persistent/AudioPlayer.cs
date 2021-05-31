using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : GlobalSingleton<AudioPlayer>
{
    public const int MinAudioSources = 16;
    public const int MaxAudioSources = 64;

    public const float MaxVolume = 0f;
    public const float MinVolume = -80f;

    [SerializeField] private AudioMixer _audioMixer = null;

    [Range(MinAudioSources, MaxAudioSources)]
    [SerializeField] private int _audioSourcesAmount = MinAudioSources;

    private AudioSource[] _pool;

    protected override void Awake()
    {
        base.Awake();

        SetupAudioSourcePool();
    }

    public void PlayAudio(AudioData data) => ConfigureAudioSource(FindAvailableAudioSource(), data);

    public void SetMasterVolume(float volume) => _audioMixer.SetFloat("Master Volume", Mathf.Clamp(volume, MinVolume, MaxVolume));

    public void SetShootingVolume(float volume) => _audioMixer.SetFloat("Shooting Volume", Mathf.Clamp(volume, MinVolume, MaxVolume));

    public void SetExplosionsVolume(float volume) => _audioMixer.SetFloat("Explosions Volume", Mathf.Clamp(volume, MinVolume, MaxVolume));

    public void SetBackgroundVolume(float volume) => _audioMixer.SetFloat("Background Volume", Mathf.Clamp(volume, MinVolume, MaxVolume));

    public void SetMusicVolume(float volume) => _audioMixer.SetFloat("Music Volume", Mathf.Clamp(volume, MinVolume, MaxVolume));

    public void SetInterfaceVolume(float volume) => _audioMixer.SetFloat("UI Volume", Mathf.Clamp(volume, MinVolume, MaxVolume));

    private void SetupAudioSourcePool()
    {
        _pool = new AudioSource[_audioSourcesAmount];

        for (int i = 0; i < _audioSourcesAmount; i++)
        {
            GameObject poolItem = new GameObject($"Audio Source #{i}");

            poolItem.transform.parent = Instance.transform;
            _pool[i] = poolItem.AddComponent<AudioSource>();

            DisableAudioSource(_pool[i]);
        }
    }

    private AudioSource FindAvailableAudioSource()
    {
        foreach (AudioSource s in _pool)
        {
            if (!s.gameObject.activeSelf) return s;
        }

        int leastImportantPoolItemIndex = 0;
        int importance = 0;

        for (int i = 0; i < _pool.Length; i++)
        {
            if (_pool[i].priority > importance)
            {
                leastImportantPoolItemIndex = i;
                importance = _pool[i].priority;
            }
        }

        DisableAudioSource(_pool[leastImportantPoolItemIndex]);

        return _pool[leastImportantPoolItemIndex];
    }

    private void DisableAudioSource(AudioSource source)
    {
        source.Stop();

        source.clip = null;
        source.outputAudioMixerGroup = null;
        source.mute = true;
        source.bypassEffects = true;
        source.bypassListenerEffects = true;
        source.loop = false;
        source.priority = 256;
        source.volume = 0f;
        source.spatialBlend = 0f;

        source.transform.position = Vector3.zero;

        source.gameObject.SetActive(false);
    }

    private void ConfigureAudioSource(AudioSource source, AudioData data)
    {
        source.clip = data.Clip;
        source.outputAudioMixerGroup = data.Group;
        source.mute = false;
        source.bypassEffects = false;
        source.bypassListenerEffects = false;
        source.priority = data.Priority;
        source.volume = data.Volume;
        source.spatialBlend = data.SpatialBlend;

        source.transform.position = data.Pos;

        source.gameObject.SetActive(true);

        source.Play();

        StartCoroutine(WatchToDisableAudioSource(source, data.Clip.length));
    }

    private IEnumerator WatchToDisableAudioSource(AudioSource source, float playDuration)
    {
        yield return new WaitForSeconds(playDuration);

        DisableAudioSource(source);
    }
}
