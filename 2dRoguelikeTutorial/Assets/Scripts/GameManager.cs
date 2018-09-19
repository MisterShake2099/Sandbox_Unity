using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public BoardManager BoardManager { get; private set; }
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn;
    public float turnDelay = 0.1f;

    private List<Enemy> enemies;
    private bool enemiesMoving;
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
        enemies = new List<Enemy>();
        BoardManager = GetComponent<BoardManager>();
        InitGame();
    }


    void InitGame()
    {
        enemies.Clear();
        BoardManager.SetupScene(level);
    }


    private void Update()
    {
        if (playersTurn || enemiesMoving)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }


    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }


    public void GameOver()
    {
        enabled = false;
    }


    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }


}
