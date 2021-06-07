using System.Collections;
using UnityEngine;

public class CameraShaker : GlobalSingleton<CameraShaker>
{
    public const float MaxAmplitude = 1f;
    public const float MaxAttenuation = 2f;
    public const float MaxFrequency = 10f;

    private Rigidbody2D _rb = null;

    private Vector2 _homePos = Vector2.zero;

    public Camera MainCam { get; private set; } = null;

    public bool ShakeEnabled { get; set; } = true;

    protected override void Awake()
    {
        base.Awake();

        _rb = SetupRigidbody2D();
        MainCam = SetupMainCam();
    }

    private Camera SetupMainCam()
    {
        Camera cam;

        if (gameObject.TryGetComponent(out Camera c)) cam = c;
        else cam = gameObject.AddComponent<Camera>();

        cam.orthographic = true;
        cam.orthographicSize = 6f;

        _homePos = cam.transform.position;

        return cam;
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

    public void StopActiveShaking()
    {
        StopAllCoroutines();
        _rb.MovePosition(_homePos);
    }

    public void Shake(float amplitude, float attenuation, float frequency, float cutoff)
    {
        if (ShakeEnabled) StartCoroutine(Shaker(amplitude, attenuation, frequency, cutoff));
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
            float deltaX = delta * AuxMath.RandomSign;
            float deltaY = delta * AuxMath.RandomSign;

            Vector2 deltaPos = new Vector2(deltaX, deltaY);

            _rb.MovePosition(_homePos + deltaPos);

            yield return new WaitForFixedUpdate();
        }

        _rb.MovePosition(_homePos);
    }
}
