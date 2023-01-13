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
                AddCoin(touch, hit.collider.gameObject, "Coin");
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
                DestroyCubes(playerTouches[i], hit.collider.gameObject, "Enemy");
            }
        }
    }

    private void DestroyCubes(Touch touch, GameObject collider, string enemyTag)
    {
        if (collider.CompareTag(enemyTag))
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Cube cube = collider.GetComponent<Cube>();

                gameManager.Timer(ref cube.playerAttackDelay, cube.Damage);
            }
        }
    }

    private void AddCoin(Touch touch, GameObject collider, string coinTag)
    {
        if (collider.CompareTag(coinTag))
        {
            if (touch.phase == TouchPhase.Began)
            {
                Coin coin = collider.GetComponent<Coin>();
                coin.AddCoin();
            }
        }
    }
}
