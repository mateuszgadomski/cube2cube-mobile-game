using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardCube : Cube
{
    public override void ObstacleEvent()
    {
        base.ObstacleEvent();
        Debug.Log("Standard");
    }
}
