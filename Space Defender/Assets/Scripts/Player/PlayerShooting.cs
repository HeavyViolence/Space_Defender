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
        PlayerDurability.PlayerDied += PlayerDiedEventHandler;
    }

    private void OnDisable()
    {
        _controls.Disable();
        PlayerDurability.PlayerDied -= PlayerDiedEventHandler;
    }

    protected void Update()
    {
        Fire();
    }

    private void PlayerDiedEventHandler(object sender, System.EventArgs e)
    {
        _controls.Disable();
    }

    protected override void Fire()
    {
        if (FirePressed && FiringCoroutine == null && _timer > Cooldown)
        {
            FiringCoroutine = StartCoroutine(Firing());
            _timer = 0f;
        }

        if (!FirePressed)
        {
            _timer += Time.deltaTime;

            if (FiringCoroutine != null)
            {
                StopCoroutine(FiringCoroutine);
                FiringCoroutine = null;
            }
        }
    }

    protected override void PerformShot(IMuzzlePoint point)
    {
        base.PerformShot(point);

        CameraShaker.Instance.Shake(0.0075f, 2f, 2f, 0.01f);
    }
}
