using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementConfig _config = null;

    private Rigidbody2D _rb = null;
    private PlayerControls _controls;

    private Vector2 MoveDir => _controls.Player.Fly.ReadValue<Vector2>();
    private Vector2 CurrentPos => new Vector2(transform.position.x, transform.position.y);

    private void Awake()
    {
        _rb = SetupRigidbody2D();
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(CurrentPos + MoveDir);
    }

    private Rigidbody2D SetupRigidbody2D()
    {
        Rigidbody2D body;

        if (gameObject.TryGetComponent(out Rigidbody2D e)) body = e;
        else body = gameObject.AddComponent<Rigidbody2D>();

        body.bodyType = RigidbodyType2D.Kinematic;
        body.simulated = true;
        body.useFullKinematicContacts = false;
        body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        body.sleepMode = RigidbodySleepMode2D.StartAwake;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        return body;
    }
}
