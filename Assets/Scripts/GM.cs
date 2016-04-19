using UnityEngine;
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
	public GameObject terrainInstance;
	public Vector3 playerPos;
	public bool playerFire = false;
	public bool health = false;
	public int playerDirection = 1;
	public int enemyDirection = -1;
	public int pHealth;
	public bool enemyReset = false;
	
	public bool[] guns = new bool[2];
	
	public static GM instance = null;
	
	private int enemyBack;
	private float enemyRate = 10f;
	private float nextEnemy = 0.0f;
	private GameObject clonePlayer;
	private GameObject enemiesObj;
	private GameObject healthPick;
	private GameObject terrain;
	private bool dead = false;
	
	void Awake()
	{
		terrain = Instantiate(terrainInstance, terrainInstance.transform.position, Quaternion.identity) as GameObject;
	}
	
	void Start () 
	{
		Time.timeScale = 1f;
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
	
	void Update()
	{	
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
		if(lives < 1)
		{
			gameOver.SetActive(true);
			Invoke("Reset", resetDelay);
		}
	}
	
	public void WinGame()
	{
		youWon.SetActive(true);
		Time.timeScale = .25f;
		SceneManager.LoadScene("Assignment_MainMenu");
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
		enemyReset = true;
		dead = false;
	}
}
