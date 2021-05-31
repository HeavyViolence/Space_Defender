using System.Collections;
using UnityEngine;

public class CameraShaker : GlobalSingleton<CameraShaker>
{
    public const float MaxAmplitude = 1f;
    public const float MaxAttenuation = 2f;
    public const float MaxFrequency = 1f;

    private Rigidbody2D _rb = null;

    private Vector2 _homePos = Vector2.zero;
    private Vector2 _deltaPos = Vector2.zero;

    private Coroutine _coroutine = null;

    public Camera MainCam { get; private set; } = null;

    public bool ShakeEnabled { get; set; } = true;

    private void Start()
    {
        SetupCamProperties();
        _rb = SetupRigidbody2D();
    }

    private void FixedUpdate()
    {
        ShakeEngine();
    }

    private void SetupCamProperties()
    {
        MainCam = CameraHolder.Instance.MainCam;
        _homePos = transform.position;
    }

    private Rigidbody2D SetupRigidbody2D()
    {
        Rigidbody2D body;

        if (gameObject.TryGetComponent(out Rigidbody2D rb)) body = rb;
        else body = gameObject.AddComponent<Rigidbody2D>();

        body.bodyType = RigidbodyType2D.Kinematic;
        body.simulated = true;
        body.useFullKinematicContacts = false;
        body.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        body.sleepMode = RigidbodySleepMode2D.StartAwake;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        return body;
    }

    private void ShakeEngine()
    {
        if (ShakeEnabled && _deltaPos != Vector2.zero) _rb.MovePosition(_homePos + _deltaPos);
    }

    private IEnumerator Shaker(float amplitude, float attenuation, float frequency, float cutoff)
    {
        amplitude = Mathf.Clamp(amplitude, 0f, MaxAmplitude);
        attenuation = Mathf.Clamp(attenuation, 0f, MaxAttenuation);
        frequency = Mathf.Clamp(frequency, 0f, MaxFrequency);
        cutoff = Mathf.Clamp01(cutoff);

        float timer = 0f;
        float duration = -Mathf.Log(cutoff, AuxMath.E) / attenuation;

        while (timer < duration)
        {
            timer += Time.fixedDeltaTime;

            float delta = amplitude * Mathf.Exp(-attenuation * timer) * Mathf.Sin(2f * Mathf.PI * frequency * timer);
            float xDelta = delta * AuxMath.RandomSign;
            float yDelta = delta * AuxMath.RandomSign;

            _deltaPos = new Vector2(xDelta, yDelta);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        _deltaPos = Vector2.zero;
        _coroutine = null;
    }

    public void Shake(float amplitude, float attenuation, float frequency, float cutoff)
    {
        if (ShakeEnabled) _coroutine = StartCoroutine(Shaker(amplitude, attenuation, frequency, cutoff));
    }

    public void StopActiveShaking()
    {
        if (_coroutine != null)
        {
            StopAllCoroutines();
            _coroutine = null;
            transform.position = _homePos;
        }
    }
}
