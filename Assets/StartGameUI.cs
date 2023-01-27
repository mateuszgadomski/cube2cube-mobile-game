using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameUI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void ClosePanelAnimation()
    {
        animator.SetTrigger("ClosePanel");
    }

    public void StartGamePanelActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = 1.0f;
    }
}