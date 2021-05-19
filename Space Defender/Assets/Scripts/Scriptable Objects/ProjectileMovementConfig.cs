using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Movement Config", menuName = "Configs/Projectile Movement Config")]
public class ProjectileMovementConfig : ScriptableObject
{
    public const float MaxSpeed = 30f;

    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _speedRandom = 0f;

    public float Speed => AuxMath.Randomize(_speed, _speedRandom);

    public float LeftBound => CameraHolder.Instance.MainCam.ViewportToWorldPoint(Vector3.left).x;

    public float RightBound => CameraHolder.Instance.MainCam.ViewportToWorldPoint(Vector3.right * 2f).x;

    public float LowerBound => CameraHolder.Instance.MainCam.ViewportToWorldPoint(Vector3.down).y;

    public float UpperBound => CameraHolder.Instance.MainCam.ViewportToWorldPoint(Vector3.up * 2f).y;
}
