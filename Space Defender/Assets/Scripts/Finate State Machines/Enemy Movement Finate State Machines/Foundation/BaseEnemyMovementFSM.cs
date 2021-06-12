using UnityEngine;

public abstract class BaseEnemyMovementFSM : BaseFSM
{
    [SerializeField] protected ShipMovementConfig _config = null;

    protected Rigidbody2D _rBody = null;

    public float LastXSpeed { get; set; } = 0f;
    public float LastYSpeed { get; set; } = 0f;
    public Rigidbody2D Body => _rBody;
    public ShipMovementConfig Config => _config;

    protected override void OnSetup()
    {
        _rBody = SetupRigidbody2D();
    }

    private Rigidbody2D SetupRigidbody2D()
    {
        Rigidbody2D body;

        if (gameObject.TryGetComponent(out Rigidbody2D b)) body = b;
        else body = gameObject.AddComponent<Rigidbody2D>();

        body.bodyType = RigidbodyType2D.Kinematic;
        body.simulated = true;
        body.useFullKinematicContacts = true;
        body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        body.sleepMode = RigidbodySleepMode2D.StartAwake;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        return body;
    }
}
