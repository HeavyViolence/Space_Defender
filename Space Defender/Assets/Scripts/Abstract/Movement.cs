using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected MovementConfig _config = null;

    public Vector2 CurrentPos => new Vector2(transform.position.x, transform.position.y);

    public abstract Vector2 Velocity { get; }

    public bool WithinBounds => AuxMath.ValueWithinRange(CurrentPos.x, _config.LeftBound, _config.RightBound) &&
                                AuxMath.ValueWithinRange(CurrentPos.y, _config.LowerBound, _config.UpperBound);

    protected virtual void Update()
    {
        WatchfForBounds();
    }

    protected abstract void Move();

    private void WatchfForBounds()
    {
        if (!WithinBounds)
        {
            float clampedX = Mathf.Clamp(CurrentPos.x, _config.LeftBound, _config.RightBound);
            float clampedY = Mathf.Clamp(CurrentPos.y, _config.LowerBound, _config.UpperBound);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
