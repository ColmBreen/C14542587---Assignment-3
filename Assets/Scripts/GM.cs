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
	public static GM instance = null;
	
	private GameObject clonePlayer;
	
	// Use this for initialization
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
		Instantiate(enemyPrefab, transform.position, Quaternion.identity);
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
		Destroy(clonePlayer);
		Invoke("SetupPlayer", resetDelay);
		CheckGameOver();
	}
	
	void SetupPlayer()
	{
		clonePlayer = Instantiate(player, player.transform.position, Quaternion.identity) as GameObject;
	}
}
