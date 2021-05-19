using UnityEngine;

public class MuzzlePoint : MonoBehaviour, IMuzzlePoint
{
    public Vector3 Pos3D => transform.position;
    public float PosX => transform.position.x;
    public float PosY => transform.position.y;

    public Quaternion Rot4D => Quaternion.Euler(Quaternion.LookRotation(transform.forward, transform.up).eulerAngles);
    public float RotX => transform.rotation.x;
    public float RotY => transform.rotation.y;
}
