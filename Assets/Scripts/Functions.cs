using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions
{
    private static float LineExistTime = 3f;
    //private bool LogWriting;
    public static Vector3 RightVector(Vector3 v)
    {
        return new Vector3(v.z, v.y, -1 * v.x);
    }
    public static Vector3 LeftVector(Vector3 v)
    {
        return new Vector3(-1 * v.z, v.y, v.x);
    }
    public static void DrawTemporalLine(Vector3 Start, Vector3 End)
    {
        GameObject LineObj = new GameObject();
        LineObj.name = "Temporal debugging line";
        LineRenderer Line = LineObj.AddComponent<LineRenderer>();
        Line.startWidth = 0.1f;
        Line.endWidth = 0.15f;
        Line.SetPosition(0, Start);
        Line.SetPosition(1, End);
        MonoBehaviour.Destroy(LineObj, LineExistTime);
    }
    public static Vector3 FlatNormalize(Vector3 v)
    {
        Vector2 flatCoordinates = new Vector2(v.x, v.z);
        return new Vector3(flatCoordinates.normalized.x, v.y, flatCoordinates.normalized.y);
    }
    public static Vector3 SetFlatMagnitude(Vector3 v, float magnitude)
    {
        Vector2 flatCoordinates = new Vector2(v.x, v.z).normalized;
        flatCoordinates *= magnitude;
        return new Vector3(flatCoordinates.x, v.y, flatCoordinates.y);
    }
}
