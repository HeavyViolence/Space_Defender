using UnityEngine;

public class EnemyShooting : Shooting
{
    [SerializeField] private bool _stopShootingOnLowerBoundReach = false;

    protected bool HasEnteredViewport => transform.position.y < CameraHolder.Instance.ViewportUpperBound &&
                                         transform.position.y > CameraHolder.Instance.ViewportLowerBound;

    protected bool HasEscapedViewport => transform.position.y < CameraHolder.Instance.ViewportLowerBound &&
                                         transform.position.y > CameraHolder.Instance.ViewportUpperBound;

    private void Update()
    {
        Fire();
    }

    protected override void Fire()
    {
        if (FiringCoroutine == null && HasEnteredViewport)
        {
            FiringCoroutine = StartCoroutine(Firing());
        }

        if (HasEscapedViewport &&
            _stopShootingOnLowerBoundReach &&
            FiringCoroutine != null)
        {
            StopCoroutine(FiringCoroutine);
            FiringCoroutine = null;
        }
    }
}
