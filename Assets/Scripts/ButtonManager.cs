using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        SingleTouch();
        MultiTouch();
    }

    private void SingleTouch()
    {
        if (Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                ActionsManager.instance.OnGameObjectTouchedCallback(hit.collider.gameObject, touch, "Coin");
            }
        }
    }

    private void MultiTouch()
    {
        Touch[] playerTouches = Input.touches;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(playerTouches[i].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                ActionsManager.instance.OnGameObjectTouchedCallback(hit.collider.gameObject, playerTouches[i], "Enemy");
            }
        }
    }
}
