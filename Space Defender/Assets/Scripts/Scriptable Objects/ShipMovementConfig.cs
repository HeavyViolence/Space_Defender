using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Movement Config", menuName = "Configs/Ship Movement Config")]
public class ShipMovementConfig : ScriptableObject
{
    public const float MaxSpeed = 20f;
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

    public float XSpeed => AuxMath.Randomize(_xSpeed, _xSpeedRandom);

    public float FastetsXSpeed => AuxMath.GetHighestRandom(_xSpeed, _xSpeedRandom);

    public float SlowestXSpeed => AuxMath.GetLowestRandom(_xSpeed, _xSpeedRandom);

    public float XSpeedDuration => AuxMath.Randomize(_xSpeedDuration, _xSpeedDurationRandom);

    public float LongestXSpeedDuration => AuxMath.GetHighestRandom(_xSpeedDuration, _xSpeedDurationRandom);

    public float ShortestXSpeedDuration => AuxMath.GetLowestRandom(_xSpeedDuration, _xSpeedDurationRandom);

    public float XSpeedTransitionDuration => AuxMath.Randomize(_xSpeedTransitionDuration, _xSpeedTransitionDurationRandom);

    public float LongestXSpeedTransitionDuration => AuxMath.GetHighestRandom(_xSpeedTransitionDuration, _xSpeedTransitionDurationRandom);

    public float ShortestXSpeedTransitionDuration => AuxMath.GetLowestRandom(_xSpeedTransitionDuration, _xSpeedTransitionDurationRandom);

    public float LeftBound => CameraHolder.Instance.ViewportLeftBound * (_horizontalBoundsOffset + 1f);

    public float RightBound => CameraHolder.Instance.ViewportRightBound * (_horizontalBoundsOffset + 1f);

    public float YSpeed => AuxMath.Randomize(_ySpeed, _ySpeedRandom);

    public float FastestYSpeed => AuxMath.GetHighestRandom(_ySpeed, _ySpeedRandom);

    public float SlowestYSpeed => AuxMath.GetLowestRandom(_ySpeed, _ySpeedRandom);

    public float YSpeedDuration => AuxMath.Randomize(_ySpeedDuration, _ySpeedDurationRandom);

    public float LongestYSpeedDuration => AuxMath.GetHighestRandom(_ySpeedDuration, _ySpeedDurationRandom);

    public float ShortestYSpeedDuration => AuxMath.GetLowestRandom(_ySpeedDuration, _ySpeedDurationRandom);

    public float YSpeedTransitionDuration => AuxMath.Randomize(_ySpeedTransitionDuration, _ySpeedTransitionDurationRandom);

    public float LongestYSpeedTransitionDuration => AuxMath.GetHighestRandom(_ySpeedTransitionDuration, _ySpeedTransitionDurationRandom);

    public float ShortestYSpeedTransitionDuration => AuxMath.GetLowestRandom(_ySpeedTransitionDuration, _ySpeedTransitionDurationRandom);

    public float UpperBound => CameraHolder.Instance.ViewportUpperBound * (_upperBoundOffset + 1f);

    public float LowerBound => CameraHolder.Instance.ViewportLowerBound * (_lowerBoundOffset + 1f);
}
