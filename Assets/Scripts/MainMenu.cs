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
		//Sets up canvas and buttons
		quitMenu = quitMenu.GetComponent<Canvas>();
		playGame = playGame.GetComponent<Button>();
		exitGame = exitGame.GetComponent<Button>();
		//Disables the sub menu
		quitMenu.enabled = false;
	}
	
	public void StartGame()
	{
		//Loads level if play game is clicked
		SceneManager.LoadScene("Assignment_LevelOne");
	}
	
	public void ExitGame()
	{
		//Disables main menu options and enables sub menu
		quitMenu.enabled = true;
		playGame.enabled = false;
		exitGame.enabled = false;
	}
	
	public void YesPress()
	{
		//quits the game if yes is clicked
		Application.Quit();
	}
	
	public void NoPress()
	{
		//disables the sub menu and enables main menu options if no is clicked
		quitMenu.enabled = false;
		playGame.enabled = true;
		exitGame.enabled = true;
	}
	
	
}
