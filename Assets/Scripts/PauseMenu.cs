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
		pauseMenu = pauseMenu.GetComponent<Canvas>();
		
		pauseMenu.enabled = false;
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 0.0f;
			pauseMenu.enabled = true;
		}
	}
	
	public void YesPress()
	{
		SceneManager.LoadScene("Assignment_MainMenu");
	}
	
	public void NoPress()
	{
		Time.timeScale = 1f;
		pauseMenu.enabled = false;
	}
	
	
}