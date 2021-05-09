using UnityEngine;

public class CameraHolder : PersistentSingleton<CameraHolder>
{
    public Camera MainCam { get; private set; } = null;

    protected override void Awake()
    {
        base.Awake();

        MainCam = SetupCamera();
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

        return cam;
    }
}
