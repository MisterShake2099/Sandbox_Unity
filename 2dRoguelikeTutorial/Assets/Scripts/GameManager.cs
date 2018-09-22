﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	public int playerFoodPoints = 100;
	public float levelStartDelay = 2f;
	public float turnDelay = 0.1f;
	[HideInInspector] public bool playersTurn = true;

	private List<Enemy> enemies;
	private bool doingSetup;
	private bool enemiesMoving;
	private int level = 0;
	private GameObject levelImage;
	private Text levelText;
	private BoardManager boardScript;


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
		boardScript = GetComponent<BoardManager>();
	}

	// This is called each time a scene is loaded.
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		level++;
		InitGame();
	}

	void OnEnable()
	{
		// Tell ‘OnLevelFinishedLoading’ to start listening for a
		// scene change event as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
	void OnDisable()
	{
		// Tell ‘OnLevelFinishedLoading’ to stop listening for a
		// scene change event as soon as this script is disabled.
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void InitGame()
	{
		doingSetup = true;

		levelImage = GameObject.Find("LevelImage");
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		levelText.text = "Day " + level;
		levelImage.SetActive(true);
		Invoke("HideLevelImage", levelStartDelay);

		enemies.Clear();
		boardScript.SetupScene(level);
	}

	private void HideLevelImage()
	{
		levelImage.SetActive(false);
		doingSetup = false;
	}

	private void Update()
	{
		if (playersTurn || enemiesMoving || doingSetup)
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
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive(true);
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
