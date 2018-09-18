﻿using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager BoardManager { get; private set; }

    private readonly int level = 3;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        BoardManager = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        BoardManager.SetupScene(level);
    }

}
