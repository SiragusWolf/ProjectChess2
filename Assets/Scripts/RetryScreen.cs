using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryScreen : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;
    private void Start()
    {
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(GameManager.Instance.Restart);
        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(GameManager.Instance.Quit);
    }
}
