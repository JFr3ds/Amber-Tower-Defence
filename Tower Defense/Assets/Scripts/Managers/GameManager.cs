using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool isPause;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        OnLaunchGame();
    }

    void OnLaunchGame()
    {
        ActionsController.OnLaunchGame?.Invoke();
    }

    void OnGameOver()
    {
        isPause = true;
        
    }

    void OnWinGame()
    {
        
    }

    

}
