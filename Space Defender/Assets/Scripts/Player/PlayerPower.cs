using UnityEngine;

public class PlayerPower : SceneSingleton<PlayerPower>
{
    [SerializeField] FireConfig _fireConfig = null;
    [SerializeField] ShipMovementConfig _movementConfig = null;
    [SerializeField] DurabilityConfig _durabilityConfig = null;

    public float Value { get; private set; } = 0f;

    protected override void Awake()
    {
        base.Awake();
    }
}
