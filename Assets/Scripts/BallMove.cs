using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public float
        ballJumpIntensivity = 7f,
        ballSideMoveIntensivity = 4f,
        ballInAirSideMoveIntensivity = 1.2f,
        ballMoveIntensivity = 6f,
        ballInAirMoveIntensivity = 1.3f;

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
    private bool writeLog = false, hasCollision = false;
    private bool victoryZoneReached
    {
        get
        {
            if (currentCollider == null)
                return false;
            else
                return currentCollider.tag == "victoryZone";
        }
    }
    private bool defeatureZoneReached
    {
        get
        {
            if (currentCollider == null)
                return false;
            else
            return currentCollider.tag == "defeatureZone";
        }
    }
    private bool isInTheAir
    {
        get
        {
            return contactObjectType == "air";
        }
    }
    private Collision currentCollision;
    private Collider currentCollider;
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
    private float victoryZoneReachTime;
    private float timeToVictory
    {
        get
        {
            if (victoryZoneReached)
                return Settings.victoryCondotion - (Time.time - victoryZoneReachTime);
            else return 1f;
        }
    }
    private Vector2 moveControlTouchCoordinates
    {
        get
        {
            foreach (Touch _t in Input.touches)
                if ((_t.position - GameMenu.MoveControlCoordinates).magnitude <= GameMenu.MoveControlRadius * 2)
                    return _t.position;
            return Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            Move(MovingDiractions.Forward);
        }
        if (Input.GetKey("s"))
        {
            Move(MovingDiractions.Backward);
        }
        if (Input.GetKey("a"))
        {
            Move(MovingDiractions.Left);
        }
        if (Input.GetKey("d"))
        {
            Move(MovingDiractions.Right);
        }
        if (Input.GetKeyDown(KeyCode.Space)||GameMenu.jump)
        {
            Jump();
        }
        if (GameMenu.joystickDiraction != Vector2.zero)
            Logger.AddContent(UILogDataTypes.PressedButton, "Joystick diraction: " + GameMenu.joystickDiraction, true);
        else
            Logger.AddContent(UILogDataTypes.PressedButton, "Joystick not used now", true);
        Move(GameMenu.joystickDiraction);
        //if (GameMenu.joystickDiraction != Vector2.zero)
        //{
        //    Move(GameMenu.joystickDiraction);
        //}
        Logger.AddContent(UILogDataTypes.BallState, "Is in the air : " + isInTheAir + ", contact to object : " + contactObjectType , true);
        if (victoryZoneReached) Logger.AddContent(UILogDataTypes.GameEvents, "Time to victory: " + (Settings.victoryCondotion - (Time.time - victoryZoneReachTime)), true);
        if (timeToVictory <= 0) GameMenu.Victory();
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
    void OnTriggerEnter(Collider collider)
    {
        currentCollider = collider;//Musthave!! victoryZoneReached property computing is based in cuurentCollider
        if (victoryZoneReached) victoryZoneReachTime = Time.time;
        if (defeatureZoneReached) Defeat();
    }
    //void OnTriggerStay(Collider collider)
    //{
    //    currentCollider = collider;
    //    if (collider.tag == "victoryZone")
    //    {
    //        Logger.AddContent(UILogDataTypes.GameEvents, "Seconds to victory", true);
    //    }
    //}
    void OnTriggerExit(Collider collider)
    {
        currentCollider = null;
    }
    private Vector3 ForceVectorByDiraction(MovingDiractions Diraction)
    {
        Vector3 ResultVector;
        switch (Diraction)
        {
            case MovingDiractions.Forward:
                ResultVector = Functions.SetFlatMagnitude(ForwardPoint - transform.position, GetIntensivity(Diraction));
                break;
            case MovingDiractions.Backward:
                ResultVector = Functions.SetFlatMagnitude(BackwardPoint - transform.position, GetIntensivity(Diraction));
                break;
            case MovingDiractions.Left:
                ResultVector = Functions.SetFlatMagnitude(LeftPoint - transform.position, GetIntensivity(Diraction));
                break;
            case MovingDiractions.Right:
                ResultVector = Functions.SetFlatMagnitude(RightPoint - transform.position, GetIntensivity(Diraction));
                break;
            default:
                Debug.LogError("Cant compute vector for unknown diraction" + Diraction.ToString());
                ResultVector = Vector3.zero;
                break;
        }
        Logger.AddContent(UILogDataTypes.PressedButton, "Ball diraction - " + Diraction.ToString(), true);
        if(writeLog) Functions.DrawTemporalLine(transform.position, transform.position + ResultVector);
        return ResultVector;
    }
    private Vector3 ForceVectorByDiraction(Vector2 Diraction)
    {
        Vector3 result = Vector3.zero;
        if (Diraction.y > 0)
            result += Functions.SetFlatMagnitude(ForwardPoint - transform.position, GetIntensivity(MovingDiractions.Forward) * Diraction.y);
        if (Diraction.y < 0)
            result += Functions.SetFlatMagnitude(BackwardPoint - transform.position, GetIntensivity(MovingDiractions.Backward) * Diraction.y * -1);
        if (Diraction.x > 0)
            result += Functions.SetFlatMagnitude(RightPoint - transform.position, GetIntensivity(MovingDiractions.Right) * Diraction.x);
        if (Diraction.x < 0)
            result += Functions.SetFlatMagnitude(LeftPoint - transform.position, GetIntensivity(MovingDiractions.Left) * Diraction.x * -1);
        return result;
    }
    private float GetIntensivity(MovingDiractions diraction)
    {
        switch (diraction)
        {
            case MovingDiractions.Forward:
                if (isInTheAir)
                    return ballInAirMoveIntensivity;
                else
                    return ballMoveIntensivity;
            case MovingDiractions.Backward:
                if (isInTheAir)
                    return ballInAirMoveIntensivity;
                else
                    return ballMoveIntensivity;
            case MovingDiractions.Left:
                if (isInTheAir)
                    return ballInAirSideMoveIntensivity;
                else
                    return ballSideMoveIntensivity;
            case MovingDiractions.Right:
                if (isInTheAir)
                    return ballInAirSideMoveIntensivity;
                else
                    return ballSideMoveIntensivity;
            default:
                Debug.LogError("Cant get intensivity for unknown diraction " + diraction.ToString());
                return 0f;
        }
    }
    private void Move(MovingDiractions Diraction)
    {
        BallBody.AddForce(ForceVectorByDiraction(Diraction));
    }
    private void Move(Vector2 Diraction)
    {
        BallBody.AddForce(ForceVectorByDiraction(Diraction));
        //Functions.DrawTemporalLine(transform.position, transform.position + ForceVectorByDiraction(Diraction));
    }
    public void Jump()
    {
        if (isInTheAir) return;
        Logger.AddContent(UILogDataTypes.PressedButton, "Jump", true);
        BallBody.velocity += Vector3.up * ballJumpIntensivity;
    }
    private void Defeat()
    {
        GameMenu.Defeture();
        Destroy(gameObject);
    }
}
