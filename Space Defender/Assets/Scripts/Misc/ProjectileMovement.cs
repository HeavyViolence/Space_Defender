using UnityEngine;

public class ProjectileMovement : Movement
{
    [SerializeField] private ProjectileMovementConfig _config = null;

    private Rigidbody2D _rb = null;

    protected override Vector2 Velocity => new Vector2(0f, _config.Speed);

    protected override bool WithinBounds => AuxMath.ValueWithinRange(Pos.x, _config.LeftBound, _config.RightBound) &&
                                            AuxMath.ValueWithinRange(Pos.y, _config.LowerBound, _config.UpperBound);

    private void Awake()
    {
        _rb = SetupRigidbody2D();
    }

    private void Start()
    {
        Move();
    }

    protected override void Update()
    {
        base.Update();

        WatchToRemove();
    }

    private Rigidbody2D SetupRigidbody2D()
    {
        Rigidbody2D body;

        if (gameObject.TryGetComponent(out Rigidbody2D rb)) body = rb;
        else body = gameObject.AddComponent<Rigidbody2D>();

        body.bodyType = RigidbodyType2D.Dynamic;
        body.useAutoMass = false;
        body.mass = 1f;
        body.drag = 0f;
        body.angularDrag = 0f;
        body.angularVelocity = 0f;
        body.gravityScale = 0f;
        body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        body.sleepMode = RigidbodySleepMode2D.StartAwake;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;

        return body;
    }

    protected override void WatchfForBounds()
    {
        if (!WithinBounds)
        {
            float clampedX = Mathf.Clamp(Pos.x, _config.LeftBound, _config.RightBound);
            float clampedY = Mathf.Clamp(Pos.y, _config.LowerBound, _config.UpperBound);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }

    protected override void Move()
    {
        _rb.AddRelativeForce(Velocity, ForceMode2D.Impulse);
    }
}
