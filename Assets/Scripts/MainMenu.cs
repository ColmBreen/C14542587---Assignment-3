using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	
	public Button playGame;
	public Button exitGame;
	
	void Start () 
	{
		playGame = playGame.GetComponent<Button>();
		exitGame = exitGame.GetComponent<Button>();
	}
	
	public void StartGame()
	{
		SceneManager.LoadScene("Assignment_LevelOne");
	}
	
	public void ExitGame()
	{
		Application.Quit();
	}
	
	
}
