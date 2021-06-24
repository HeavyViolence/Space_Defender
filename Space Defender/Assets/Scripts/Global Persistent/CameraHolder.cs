using UnityEngine;

public class CameraHolder : GlobalSingleton<CameraHolder>
{
    public Camera MainCam { get; private set; } = null;

    public AudioListener Listener { get; private set; } = null;

    public float ViewportUpperBound { get; private set; } = 0f;
    public float ViewportLowerBound { get; private set; } = 0f;
    public float ViewportLeftBound { get; private set; } = 0f;
    public float ViewportRightBound { get; private set; } = 0f;

    protected override void Awake()
    {
        base.Awake();

        MainCam = SetupCamera();
        Listener = SetupAudioListener();
        SetupBounds();
    }

    private Camera SetupCamera()
    {
        Camera cam;

        if (gameObject.TryGetComponent(out Camera c)) cam = c;
        else cam = gameObject.AddComponent<Camera>();

        cam.orthographic = true;
        cam.orthographicSize = 6f;
        cam.nearClipPlane = 1f;
        cam.farClipPlane = 1000f;
        cam.transform.position = new Vector3(0f, 0f, -10f);

        return cam;
    }

    private AudioListener SetupAudioListener()
    {
        AudioListener listener;

        if (gameObject.TryGetComponent(out AudioListener l)) listener = l;
        else listener = gameObject.AddComponent<AudioListener>();

        listener.enabled = false;

        return listener;
    }

    private void SetupBounds()
    {
        ViewportUpperBound = MainCam.ViewportToWorldPoint(Vector3.up).y;
        ViewportLowerBound = MainCam.ViewportToWorldPoint(Vector3.zero).y;
        ViewportLeftBound = MainCam.ViewportToWorldPoint(Vector3.zero).x;
        ViewportRightBound = MainCam.ViewportToWorldPoint(Vector3.right).x;
    }
}
