using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	//public GameObject Player;
	
	private Vector3 offset;

	void Start () {
		offset = transform.position - GM.instance.playerPos;
	}
	
	void LateUpdate () {
		transform.position = GM.instance.playerPos + offset;
		if(transform.position.x <= 10)
		{
			transform.position = new Vector3 (10, transform.position.y, transform.position.z);
		}
	}
}
