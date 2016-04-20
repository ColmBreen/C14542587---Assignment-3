using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{
	public Canvas pauseMenu;
	public Button yes;
	public Button no;
	
	
	void Start () 
	{
		//Sets up canvas
		pauseMenu = pauseMenu.GetComponent<Canvas>();
		//Initially disables pause menu
		pauseMenu.enabled = false;
	}
	
	void Update()
	{
		//if escape key is pressed, time is stopped and the pause menu is displayed
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 0.0f;
			pauseMenu.enabled = true;
		}
	}
	
	public void YesPress()
	{
		//if yes is clicked the game returns to the main menu
		SceneManager.LoadScene("Assignment_MainMenu");
	}
	
	public void NoPress()
	{
		//If no is pressed time is resumed and the pause menu disappears
		Time.timeScale = 1f;
		pauseMenu.enabled = false;
	}
	
	
}