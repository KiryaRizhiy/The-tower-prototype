using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static float 
        CameraDistance = 7f, 
        //ballJumpIntensivity = 7f, Временно перенесены в BallMove для тюнинга
        //ballSideMoveIntensivity = 4f, 
        //ballInAirSideMoveIntensivity = 1.2f, 
        //ballMoveIntensivity = 6f,
        //ballInAirMoveIntensivity = 1.3f,
        victoryCondotion = 3f,
        levelEnergy = 700f,
        energyMultiplyer = 0.03f;
    public static int levelsCount = 5;
}
