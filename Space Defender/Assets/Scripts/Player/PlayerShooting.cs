using UnityEngine;

public class PlayerShooting : Shooting
{
    private PlayerControls _controls = null;

    private float _timer = Mathf.Infinity;

    private bool FirePressed => _controls.Player.Fire.ReadValue<float>() > 0f;

    protected override void Awake()
    {
        _controls = new PlayerControls();

        base.Awake();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    protected void Update()
    {
        Fire();
    }

    protected override void Fire()
    {
        if (FirePressed && _firingCoroutine == null && _timer > Cooldown)
        {
            _firingCoroutine = StartCoroutine(Firing());
            _timer = 0f;
        }

        if (!FirePressed)
        {
            _timer += Time.deltaTime;

            if (_firingCoroutine != null)
            {
                StopCoroutine(_firingCoroutine);
                _firingCoroutine = null;
            }
        }
    }

    protected override void PerformShot(IMuzzlePoint point)
    {
        base.PerformShot(point);

        CameraShaker.Instance.Shake(0.0075f, 2f, 2f, 0.01f);
    }
}
