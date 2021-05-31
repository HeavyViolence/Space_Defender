using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Movement Config", menuName = "Configs/Projectile Movement Config")]
public class ProjectileMovementConfig : ScriptableObject
{
    public const float MaxSpeed = 30f;

    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _speedRandom = 0f;

    public float Speed => AuxMath.Randomize(_speed, _speedRandom);

    public float LeftBound => CameraHolder.Instance.LeftBound * 2f;

    public float RightBound => CameraHolder.Instance.RightBound * 2f;

    public float LowerBound => CameraHolder.Instance.LowerBound * 2f;

    public float UpperBound => CameraHolder.Instance.UpperBound * 2f;
}
