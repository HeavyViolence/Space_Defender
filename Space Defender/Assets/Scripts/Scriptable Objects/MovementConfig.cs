using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Config", menuName = "Configs/Movement Config")]
public class MovementConfig : ScriptableObject
{
    public const float MaxSpeed = 32f;
    public const float MaxSpeedDuration = 16f;
    public const float MaxSpeedTransitionDuration = 8f;

    [SerializeField] private float _xSpeed = 0f;
    [SerializeField] private float _xSpeedRandom = 0f;

    [SerializeField] private float _xSpeedDuration = 0f;
    [SerializeField] private float _xSpeedDurationRandom = 0f;

    [SerializeField] private float _xSpeedTransitionDuration = 0f;
    [SerializeField] private float _xSpeedTransitionDurationRandom = 0f;

    [SerializeField] private float _horizontalBoundsOffset = 0f;

    [SerializeField] private float _ySpeed = 0f;
    [SerializeField] private float _ySpeedRandom = 0f;

    [SerializeField] private float _ySpeedDuration = 0f;
    [SerializeField] private float _ySpeedDurationRandom = 0f;

    [SerializeField] private float _ySpeedTransitionDuration = 0f;
    [SerializeField] private float _ySpeedTransitionDurationRandom = 0f;

    [SerializeField] private float _upperBoundOffset = 0f;
    [SerializeField] private float _lowerBoundOffset = 0f;

    public float Xspeed => AuxMath.Randomize(_xSpeed, _xSpeedRandom);

    public float XspeedDuration => AuxMath.Randomize(_xSpeedDuration, _xSpeedDurationRandom);

    public float XspeedTransitionDuration => AuxMath.Randomize(_xSpeedTransitionDuration, _xSpeedTransitionDurationRandom);

    public float LeftBound
    {
        get
        {
            float leftBoundPos = CameraHolder.Instance.MainCam.ViewportToWorldPoint(-Vector3.right).x;
            return leftBoundPos + _horizontalBoundsOffset;
        }
    }

    public float RightBound
    {
        get
        {
            float rightBoundPos = CameraHolder.Instance.MainCam.ViewportToWorldPoint(Vector3.right).x;
            return rightBoundPos - _horizontalBoundsOffset;
        }
    }

    public float Yspeed => AuxMath.Randomize(_ySpeed, _ySpeedRandom);

    public float YspeedDuration => AuxMath.Randomize(_ySpeedDuration, _ySpeedDurationRandom);

    public float YspeedTransitionDuration => AuxMath.Randomize(_ySpeedTransitionDuration, _ySpeedTransitionDurationRandom);

    public float UpperBound
    {
        get
        {
            float upperBoundPos = CameraHolder.Instance.MainCam.ViewportToWorldPoint(Vector3.up).y;
            return upperBoundPos + _upperBoundOffset;
        }
    }
    public float LowerBound

    {
        get
        {
            float lowerBoundPos = CameraHolder.Instance.MainCam.ViewportToWorldPoint(-Vector3.up).y;
            return lowerBoundPos + _lowerBoundOffset;
        }
    }
}
