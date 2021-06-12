using UnityEngine;

public abstract class ShipMovement : Movement
{
    [SerializeField] protected ShipMovementConfig _config = null;

    protected override Vector2 Velocity => new Vector2(_config.XSpeed, _config.YSpeed) * Time.deltaTime;

    protected override bool WithinBounds => AuxMath.ValueWithinRange(Pos.x, _config.LeftBound, _config.RightBound) &&
                                            AuxMath.ValueWithinRange(Pos.y, _config.LowerBound, _config.UpperBound);

    protected override void WatchfForBounds()
    {
        if (!WithinBounds)
        {
            float clampedX = Mathf.Clamp(Pos.x, _config.LeftBound, _config.RightBound);
            float clampedY = Mathf.Clamp(Pos.y, _config.LowerBound, _config.UpperBound);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
