using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	//public GameObject Player;
	
	private Vector3 offset;
	//Sets camera to  track player
	void Start () {
		offset = transform.position - GM.instance.playerPos;
	}
	
	void LateUpdate () {
		transform.position = GM.instance.playerPos + offset;
		//Stops moving left, right and down if values are met
		if(transform.position.x <= 10)
		{
			transform.position = new Vector3 (10, transform.position.y, transform.position.z);
		}
		if(transform.position.x >= 173)
		{
			transform.position = new Vector3 (173, transform.position.y, transform.position.z);
		}
		if(transform.position.y <= -5)
		{
			transform.position = new Vector3 (transform.position.x, -5, transform.position.z);
		}
	}
}
