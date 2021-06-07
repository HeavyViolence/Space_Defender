using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public const float MinBlinkRate = 1f;
    public const float MaxBlinkRate = 10f;

    public const float MinBlinkColorFalloff = 0.9f;
    public const float MaxBlinkColorFalloff = 0.75f;

    private Renderer _renderer = null;

    private void Awake()
    {
        _renderer = FindRendererIfExists();
    }

    private Renderer FindRendererIfExists()
    {
        Renderer renderer = null;

        if (gameObject.TryGetComponent(out Renderer r)) renderer = r;

        return renderer;
    }

    public void SetValue(float currentValue, float maxValue)
    {
        if (_renderer != null)
            _renderer.material.SetFloat("_NormalizedHealth", currentValue / maxValue);
    }

    public void EnableBlinking()
    {
        if (_renderer != null)
            _renderer.material.SetFloat("_Blink", 1f);
    }

    public void DisableBlinking()
    {
        if (_renderer != null)
            _renderer.material.SetFloat("_Blink", 0f);
    }

    public bool BlinkingEnabled
    {
        get
        {
            if (_renderer != null)
                return _renderer.material.GetFloat("_Blink") == 1f ? true : false;

            else return false;
        }
    }

    public void SetBlinkRate(float rate)
    {
        if (_renderer != null)
        {
            float clampedRate = Mathf.Clamp(rate, MinBlinkRate, MaxBlinkRate);
            _renderer.material.SetFloat("_BlinkRate", clampedRate);
        }
    }

    public void SetBlinkColorFalloff(float falloff)
    {
        if (_renderer != null)
        {
            float clampedFalloff = Mathf.Clamp(falloff, MinBlinkColorFalloff, MaxBlinkColorFalloff);
            _renderer.material.SetFloat("_BlinkColorFalloff", clampedFalloff);
        }
    }

    public void Hide()
    {
        if (_renderer != null) _renderer.enabled = false;
    }

    public void Show()
    {
        if (_renderer != null) _renderer.enabled = true;
    }
}
