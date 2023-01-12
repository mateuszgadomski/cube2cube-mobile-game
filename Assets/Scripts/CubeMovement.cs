using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    Cube cube;
    Vector3 direction;

    private void Start()
    {
        cube = GetComponent<Cube>();
        direction = Vector3.up;
    }

    private void Update()
    {
        CubeMove(direction, cube.cubeSpeed);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            if (direction != Vector3.down)
            {
                direction = Vector3.down;
            }
            else
            {
                direction = Vector3.up;
            }
        }
    }

    private void CubeMove(Vector3 direction, float cubeSpeed)
    {
        transform.Translate(direction * cubeSpeed * Time.deltaTime, Space.World);
    }

}
