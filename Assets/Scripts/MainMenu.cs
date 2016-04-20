using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	
	public Button playGame;
	public Button exitGame;
	public Canvas quitMenu;
	public Button yes;
	public Button no;
	
	void Start () 
	{
		quitMenu = quitMenu.GetComponent<Canvas>();
		playGame = playGame.GetComponent<Button>();
		exitGame = exitGame.GetComponent<Button>();
		
		quitMenu.enabled = false;
	}
	
	public void StartGame()
	{
		SceneManager.LoadScene("Assignment_LevelOne");
	}
	
	public void ExitGame()
	{
		quitMenu.enabled = true;
		playGame.enabled = false;
		exitGame.enabled = false;
	}
	
	public void YesPress()
	{
		Application.Quit();
	}
	
	public void NoPress()
	{
		quitMenu.enabled = false;
		playGame.enabled = true;
		exitGame.enabled = true;
	}
	
	
}
