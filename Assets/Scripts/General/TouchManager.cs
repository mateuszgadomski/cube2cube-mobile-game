using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private readonly string _coinTag = "Coin";
    private readonly string _enemyTag = "Enemy";

    private void Update()
    {
        SingleTouch();
        MultiTouch();
    }

    private void SingleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                EventManager.PlayerEvents.CallOnGameObjectTouched(hit.collider.gameObject, touch, _coinTag);
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
                EventManager.PlayerEvents.CallOnGameObjectTouched(hit.collider.gameObject, playerTouches[i], _enemyTag);
            }
        }
    }
}