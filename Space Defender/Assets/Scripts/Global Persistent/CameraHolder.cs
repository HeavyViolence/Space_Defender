using UnityEngine;

public class CameraHolder : GlobalSingleton<CameraHolder>
{
    public Camera MainCam { get; private set; } = null;

    public float UpperBound { get; private set; } = 0f;
    public float LowerBound { get; private set; } = 0f;
    public float LeftBound { get; private set; } = 0f;
    public float RightBound { get; private set; } = 0f;

    protected override void Awake()
    {
        base.Awake();

        MainCam = SetupCamera();
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

        return cam;
    }

    private void SetupBounds()
    {
        UpperBound = MainCam.ViewportToWorldPoint(Vector3.up).y;
        LowerBound = MainCam.ViewportToWorldPoint(Vector3.zero).y;
        LeftBound = MainCam.ViewportToWorldPoint(Vector3.zero).x;
        RightBound = MainCam.ViewportToWorldPoint(Vector3.right).x;
    }
}
