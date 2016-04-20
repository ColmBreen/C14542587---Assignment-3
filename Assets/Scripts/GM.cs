using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {
	
	public int lives;
	public float resetDelay;
	public Text livesText;
	public Text healthText;
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject player;
	public GameObject enemyPrefab;
	public GameObject bloodParticles;
	public GameObject healthPickup;
	public GameObject gunPickup;
	public GameObject terrainInstance;
	public Vector3 playerPos;
	public bool playerFire = false;
	public bool health = false;
	public int playerDirection;
	public int enemyDirection;
	public int pHealth;
	public bool enemyReset = false;
	
	public bool[] guns = new bool[2];
	
	public static GM instance = null;
	
	private int enemyBack;
	private float enemyRate;
	private float nextEnemy;
	private GameObject clonePlayer;
	private GameObject enemiesObj;
	private GameObject healthPick;
	private GameObject gunPick;
	private GameObject terrain;
	private bool dead = false;
	
	void Awake()
	{
		//Adds the terrain into the game
		terrain = Instantiate(terrainInstance, terrainInstance.transform.position, Quaternion.identity) as GameObject;
	}
	
	void Start () 
	{
		//Sets the speed of time to the default speed
		Time.timeScale = 1f;
		//Equips the player with the default gun type
		guns[0] = true;
		//If another instance of te GM exists, destroy it and set this one as the main
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		//Sets variables
		playerDirection = 1;
		lives = 3;
		resetDelay = 1.5f;
		enemyDirection = -1;
		
		enemyRate = 7f;
		nextEnemy = 0.0f;
		
		Setup();
	}
	
	public void Setup()
	{
		//Creates instance of the player, healthpickup and gunPickup
		clonePlayer = Instantiate(player, player.transform.position, Quaternion.identity) as GameObject;
		healthPick = Instantiate(healthPickup, healthPickup.transform.position, Quaternion.identity) as GameObject;
		gunPick = Instantiate(gunPickup, gunPickup.transform.position, Quaternion.identity) as GameObject;
	}
	
	void LateUpdate()
	{
		//Keeps track of playerposition
		if(!dead)
			playerPos = clonePlayer.transform.position;
		else
			playerPos = playerPos;
		//Sets the on screen text for health
		healthText.text = "Health: " + pHealth;
	}
	
	void Update()
	{	
		//Spawns enemies based on time and player position 
		if(Time.time > nextEnemy)
		{
			enemyBack = Random.Range(0, 2);
			if(playerPos.x > 20 && enemyBack == 1)
			{
				enemyPrefab.transform.position = playerPos + new Vector3(-5f, 10f, 0);
			}
			else if(enemyBack == 0)
			{
				enemyPrefab.transform.position = playerPos + new Vector3(20f, 10f, 0);
			}
			enemiesObj = Instantiate(enemyPrefab, enemyPrefab.transform.position, Quaternion.identity) as GameObject;
			nextEnemy = Time.time + enemyRate;
		}
	}
	
	void CheckGameOver()
	{
		//Game over condition
		if(lives < 1)
		{
			gameOver.SetActive(true);
			Invoke("MainMenu", resetDelay);
		}
	}
	
	public void WinGame()
	{
		//Win Game condition
		youWon.SetActive(true);
		Invoke("MainMenu", resetDelay);
	}
	
	void MainMenu()
	{
		//Returns to the main menu when called
		SceneManager.LoadScene("Assignment_MainMenu");
	}
	
	public void LoseLife()
	{
		//Steps taken when the player dies
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate(bloodParticles, player.transform.position, Quaternion.identity);
		dead = true;
		Destroy(clonePlayer);
		Invoke("SetupPlayer", resetDelay);
		CheckGameOver();
	}
	
	void SetupPlayer()
	{
		//Resets the level after the player has died
		terrain = Instantiate(terrainInstance, terrainInstance.transform.position, Quaternion.identity) as GameObject;
		clonePlayer = Instantiate(player, player.transform.position, Quaternion.identity) as GameObject;
		enemyReset = true;
		dead = false;
	}
}
