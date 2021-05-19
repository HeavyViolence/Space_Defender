using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected Vector2 Pos => new Vector2(transform.position.x, transform.position.y);

    protected abstract Vector2 Velocity { get; }

    protected abstract bool WithinBounds { get; }

    protected virtual void Update()
    {
        WatchfForBounds();
    }

    protected abstract void WatchfForBounds();

    protected void WatchToRemove()
    {
        if (!WithinBounds) Destroy(gameObject);
    }

    protected abstract void Move();
}
