using UnityEngine;
using System;

public class ProjectileHitEventArgs : EventArgs
{
    public Vector3 HitPos { get; private set; }
    public IDamageable Recipient { get; private set; }

    public ProjectileHitEventArgs(Vector3 hitPos, IDamageable recipient)
    {
        HitPos = hitPos;
        Recipient = recipient;
    }
}
