﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {
	
	public int lives = 3;
	public int enemies = 10;
	public float resetDelay = 1f;
	public Text livesText;
	public Text healthText;
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject player;
	public GameObject enemyPrefab;
	public GameObject bloodParticles;
	public GameObject healthPickup;
	public Vector3 playerPos;
	public bool playerFire = false;
	public bool health = false;
	public int playerDirection = 0;
	public int enemyDirection = -1;
	public int pHealth;
	
	public bool[] guns = new bool[4];
	
	public static GM instance = null;
	
	private GameObject clonePlayer;
	private GameObject enemiesObj;
	private GameObject healthPick;
	private bool dead = false;
	
	
	void Start () 
	{
		guns[0] = true;
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		
		Setup();
	}
	
	public void Setup()
	{
		clonePlayer = Instantiate(player, player.transform.position, Quaternion.identity) as GameObject;
		enemiesObj = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
		healthPick = Instantiate(healthPickup, healthPickup.transform.position, Quaternion.identity) as GameObject;
	}
	
	void LateUpdate()
	{
		if(!dead)
			playerPos = clonePlayer.transform.position;
		else
			playerPos = playerPos;
		
		healthText.text = "Health: " + pHealth;
	}
	
	void CheckGameOver()
	{
		if(lives < 1)
		{
			gameOver.SetActive(true);
			Invoke("Reset", resetDelay);
		}
	}
	
	void Reset()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Assignment_LevelOne");
	}
	
	public void LoseLife()
	{
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate(bloodParticles, player.transform.position, Quaternion.identity);
		dead = true;
		Destroy(clonePlayer);
		Destroy(enemiesObj);
		Invoke("SetupPlayer", resetDelay);
		CheckGameOver();
	}
	
	void SetupPlayer()
	{
		clonePlayer = Instantiate(player, player.transform.position, Quaternion.identity) as GameObject;
		enemiesObj = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
		dead = false;
	}
}
