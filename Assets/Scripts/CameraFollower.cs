using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject Ball;
    private Vector3 flatlyNormalBallCoords
    {
        get
        {
            return Functions.FlatNormalize(Ball.transform.position);
        }
    }
    private Vector3 pointBehindTheBall
    {
        get
        {
            return new Vector3(flatlyNormalBallCoords.x*Settings.CameraDistance,flatlyNormalBallCoords.y,flatlyNormalBallCoords.z*Settings.CameraDistance);
        }
    }
    private Vector3 PointAfterTheBall
    {
        get
        {
            return new Vector3(0, flatlyNormalBallCoords.y, 0);
        }
    }
    private bool WriteLog = false;

    // Update is called once per frame
    void Update()
    {
        if (Ball == null)
            return;
        transform.position = pointBehindTheBall;
        transform.rotation = Quaternion.LookRotation(PointAfterTheBall - transform.position, Vector3.up);
        if(WriteLog) Functions.DrawTemporalLine(transform.position, PointAfterTheBall);
    }
}