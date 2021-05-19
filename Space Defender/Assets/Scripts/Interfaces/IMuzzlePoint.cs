using UnityEngine;

public interface IMuzzlePoint
{
    Vector3 Pos3D { get; }
    float PosX { get; }
    float PosY { get; }

    Quaternion Rot4D { get; }
    float RotX { get; }
    float RotY { get; }
}
