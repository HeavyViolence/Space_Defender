using UnityEngine;
using System;

public class ProjectileHitEventArgs : EventArgs
{
    public Vector3 HitPos { get; }
    public IDamageable Recipient { get; }

    public ProjectileHitEventArgs(Vector3 hitPos, IDamageable recipient)
    {
        HitPos = hitPos;
        Recipient = recipient;
    }
}
