using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private Vector3 ForwardPoint
    {
        get
        {
            return new Vector3(0, transform.position.y, 0);
        }
    }
    private Vector3 BackwardPoint
    {
        get
        {
            return new Vector3(transform.position.x*2, transform.position.y, transform.position.z*2);
        }
    }
    private Vector3 LeftPoint
    {
        get
        {
            return Functions.RightVector(transform.position);
        }
    }
    private Vector3 RightPoint
    {
        get
        {
            return Functions.LeftVector(transform.position);
        }
    }
    private enum MovingDiractions {Forward,Backward,Left,Right};
    private Rigidbody BallBody
    {
        get
        {
            return GetComponent<Rigidbody>();
        }
    }
    private Collider ballCollider
    {
        get
        {
            return GetComponent<Collider>();
        }
    }
    private bool WriteLog = true, hasCollision = false;
    private bool isInTheAir
    {
        get
        {
            return contactObjectType == "air";
        }
    }
    private Collision currentCollision;
    private string contactObjectType
    {
        get
        {
            if (hasCollision)
                return currentCollision.gameObject.tag;
            else
                return "air";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            if (WriteLog) Debug.Log("Moving " + MovingDiractions.Forward.ToString());
            Move(MovingDiractions.Forward);
        }
        if (Input.GetKey("s"))
        {
            if (WriteLog) Debug.Log("Moving " + MovingDiractions.Backward.ToString());
            Move(MovingDiractions.Backward);
        }
        if (Input.GetKey("a"))
        {
            if (WriteLog) Debug.Log("Moving " + MovingDiractions.Left.ToString());
            Move(MovingDiractions.Left);
        }
        if (Input.GetKey("d"))
        {
            if (WriteLog) Debug.Log("Moving " + MovingDiractions.Right.ToString());
            Move(MovingDiractions.Right);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (WriteLog) Debug.Log("Jump");
            Jump();
        }
        Logger.AddContent(UILogDataTypes.BallState, "Is in the air : " + isInTheAir + ", contact to object : " + contactObjectType , true);
    }
    void OnCollisionEnter(Collision collision)
    {
        hasCollision = true;
        currentCollision = collision;
    }
    void OnCollisionExit(Collision collision)
    {
        hasCollision = false;
        currentCollision = null;
    }
    void OnCollisionStay(Collision collision)
    {
        hasCollision = true;
        currentCollision = collision;
    }
    private Vector3 NormalizedForceVectorByDiraction(MovingDiractions Diraction)
    {
        Vector3 ResultVector;
        switch (Diraction)
        {
            case MovingDiractions.Forward:
                ResultVector = Functions.SetFlatMagnitude(ForwardPoint - transform.position + Vector3.up * transform.position.y, GetIntensivity(Diraction));
                break;
            case MovingDiractions.Backward:
                ResultVector = Functions.SetFlatMagnitude(BackwardPoint - transform.position + Vector3.up * transform.position.y, GetIntensivity(Diraction));
                break;
            case MovingDiractions.Left:
                ResultVector = Functions.SetFlatMagnitude(LeftPoint, GetIntensivity(Diraction));
                break;
            case MovingDiractions.Right:
                ResultVector = Functions.SetFlatMagnitude(RightPoint, GetIntensivity(Diraction));
                break;
            default:
                Debug.LogError("Cant compute vector for unknown diraction" + Diraction.ToString());
                ResultVector = Vector3.zero;
                break;
        }
        Logger.AddContent(UILogDataTypes.PressedButton, "Ball diraction - " + Diraction.ToString(), true);
        return ResultVector;
    }
    private float GetIntensivity(MovingDiractions diraction)
    {
        switch (diraction)
        {
            case MovingDiractions.Forward:
                if (isInTheAir)
                    return Settings.ballInAirMoveIntensivity;
                else
                    return Settings.ballMoveIntensivity;
            case MovingDiractions.Backward:
                if (isInTheAir)
                    return Settings.ballInAirMoveIntensivity;
                else
                    return Settings.ballMoveIntensivity;
            case MovingDiractions.Left:
                if (isInTheAir)
                    return Settings.ballInAirSideMoveIntensivity;
                else
                    return Settings.ballSideMoveIntensivity;
            case MovingDiractions.Right:
                if (isInTheAir)
                    return Settings.ballInAirSideMoveIntensivity;
                else
                    return Settings.ballSideMoveIntensivity;
            default:
                Debug.LogError("Cant get intensivity for unknown diraction " + diraction.ToString());
                return 0f;
        }
    }
    private void Move(MovingDiractions Diraction)
    {
        BallBody.AddForce(NormalizedForceVectorByDiraction(Diraction));
    }
    private void Jump()
    {
        if (isInTheAir) return;
        BallBody.velocity = Vector3.up * Settings.ballJumpIntensivity;
    }
}
