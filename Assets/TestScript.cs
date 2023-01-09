using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private float time = 0;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                time += Time.deltaTime;
                if (time > 1)
                {
                    Debug.Log("NOW");
                    Handheld.Vibrate();
                    time = 0;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("END");
                time = 0;
            }
        }
    }
}
