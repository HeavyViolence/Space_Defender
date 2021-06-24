using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDurability : Durability
{
    [SerializeField] private Collider2D _collider = null;

    private List<Renderer> _renderers = null;

    private AudioListener _listener = null;

    public static event EventHandler PlayerDied;

    protected override void Awake()
    {
        base.Awake();

        _renderers = FindAllRenderers();
        _listener = SetupAudioListener();
    }

    private List<Renderer> FindAllRenderers()
    {
        List<Renderer> renderers = new List<Renderer>();
        renderers.AddRange(gameObject.GetComponentsInChildren<Renderer>());

        return renderers;
    }

    private AudioListener SetupAudioListener()
    {
        AudioListener listener;

        if (gameObject.TryGetComponent(out AudioListener l)) listener = l;
        else listener = gameObject.AddComponent<AudioListener>();

        listener.enabled = true;

        return listener;
    }

    protected override void PerformDestruction()
    {
        base.PerformDestruction();

        DisableAllRenderers();
        OnPlayerDied();

        _listener.enabled = false;
        CameraHolder.Instance.Listener.enabled = true;
        _collider.enabled = false;
    }

    private void OnPlayerDied()
    {
        PlayerDied?.Invoke(this, EventArgs.Empty);
    }

    private void DisableAllRenderers()
    {
        foreach (var e in _renderers)
            e.enabled = false;
    }
}
