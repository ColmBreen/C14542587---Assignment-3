using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {
	
	public int lives = 3;
	public int enemies = 10;
	public float resetDelay = 1f;
	public Text livesText;
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject player;
	public GameObject enemyPrefab;
	public GameObject bloodParticles;
	public Vector3 playerPos;
	public bool playerFire = false;
	public int playerDirection = 0;
	public int enemyDirection = -1;
	
	public static GM instance = null;
	
	private GameObject clonePlayer;
	private GameObject enemiesObj;
	private bool dead = false;
	
	void Start () 
	{
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
	}
	
	void LateUpdate()
	{
		if(!dead)
			playerPos = clonePlayer.transform.position;
		else
			playerPos = playerPos;
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
		Application.LoadLevel(Application.loadedLevel);
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
