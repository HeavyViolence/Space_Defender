using UnityEngine;

public class ProjectileMovement : Movement
{
    public const float MovementBoundsDistanceFactor = 2f;

    protected Rigidbody2D Body { get; private set; } = null;

    protected override Vector2 Velocity => new Vector2(0f, Speed);

    protected float LeftBound { get; private set; } = 0f;

    protected float RightBound { get; private set; } = 0f;

    protected float LowerBound { get; private set; } = 0f;

    protected float UpperBound { get; private set; } = 0f;

    protected override bool WithinBounds => AuxMath.ValueWithinRange(Pos.x, LeftBound, RightBound) &&
                                            AuxMath.ValueWithinRange(Pos.y, LowerBound, UpperBound);

    private float _speed = 0f;

    public float Speed { get => _speed; set => _speed = Mathf.Abs(value); }

    private void Awake()
    {
        SetupMovementBounds();
        Body = SetupRigidbody2D();
    }

    private void Start()
    {
        Move();
    }

    protected void Update()
    {
        WatchToRemove();
    }

    protected void FixedUpdate()
    {
        WatchfForBounds();
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
        body.interpolation = RigidbodyInterpolation2D.Extrapolate;

        return body;
    }

    protected override void WatchfForBounds()
    {
        if (!WithinBounds)
        {
            float clampedX = Mathf.Clamp(Pos.x, LeftBound, RightBound);
            float clampedY = Mathf.Clamp(Pos.y, LowerBound, UpperBound);

            Body.position = new Vector2(clampedX, clampedY);
        }
    }

    protected override void Move()
    {
        Body.AddRelativeForce(Velocity, ForceMode2D.Impulse);
    }

    private void SetupMovementBounds()
    {
        LeftBound = CameraHolder.Instance.ViewportLeftBound * MovementBoundsDistanceFactor;
        RightBound = CameraHolder.Instance.ViewportRightBound * MovementBoundsDistanceFactor;
        LowerBound = CameraHolder.Instance.ViewportLowerBound * MovementBoundsDistanceFactor;
        UpperBound = CameraHolder.Instance.ViewportUpperBound * MovementBoundsDistanceFactor;
    }
}
