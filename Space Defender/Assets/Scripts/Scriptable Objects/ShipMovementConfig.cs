using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Movement Config", menuName = "Configs/Ship Movement Config")]
public class ShipMovementConfig : ScriptableObject
{
    public const float MaxSpeed = 30f;
    public const float MaxSpeedDuration = 15f;
    public const float MaxSpeedTransitionDuration = 10f;

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

    public float HighestXspeed => AuxMath.HighestRandom(_xSpeed, _xSpeedRandom);

    public float LowestXspeed => AuxMath.LowestRandom(_xSpeed, _xSpeedRandom);

    public float XspeedDuration => AuxMath.Randomize(_xSpeedDuration, _xSpeedDurationRandom);

    public float HighestXspeedDuration => AuxMath.HighestRandom(_xSpeedDuration, _xSpeedDurationRandom);

    public float LowestXspeedDuration => AuxMath.LowestRandom(_xSpeedDuration, _xSpeedDurationRandom);

    public float XspeedTransitionDuration => AuxMath.Randomize(_xSpeedTransitionDuration, _xSpeedTransitionDurationRandom);

    public float HighestXspeedTransitionDuration => AuxMath.HighestRandom(_xSpeedTransitionDuration, _xSpeedTransitionDurationRandom);

    public float LowestXspeedTransitionDuration => AuxMath.LowestRandom(_xSpeedTransitionDuration, _xSpeedTransitionDurationRandom);

    public float LeftBound => CameraHolder.Instance.LeftBound * (_horizontalBoundsOffset + 1f);

    public float RightBound => CameraHolder.Instance.RightBound * (_horizontalBoundsOffset + 1f);

    public float Yspeed => AuxMath.Randomize(_ySpeed, _ySpeedRandom);

    public float HighestYspeed => AuxMath.HighestRandom(_ySpeed, _ySpeedRandom);

    public float LowestYspeed => AuxMath.LowestRandom(_ySpeed, _ySpeedRandom);

    public float YspeedDuration => AuxMath.Randomize(_ySpeedDuration, _ySpeedDurationRandom);

    public float HighestYspeedDuration => AuxMath.HighestRandom(_ySpeedDuration, _ySpeedDurationRandom);

    public float LowestYspeedDuration => AuxMath.LowestRandom(_ySpeedDuration, _ySpeedDurationRandom);

    public float YspeedTransitionDuration => AuxMath.Randomize(_ySpeedTransitionDuration, _ySpeedTransitionDurationRandom);

    public float HighestYspeedTransitionDuration => AuxMath.HighestRandom(_ySpeedTransitionDuration, _ySpeedTransitionDurationRandom);

    public float LowestYspeedTransitionDuration => AuxMath.LowestRandom(_ySpeedTransitionDuration, _ySpeedTransitionDurationRandom);

    public float UpperBound => CameraHolder.Instance.UpperBound * (_upperBoundOffset + 1f);

    public float LowerBound => CameraHolder.Instance.LowerBound * (_lowerBoundOffset + 1f);
}
