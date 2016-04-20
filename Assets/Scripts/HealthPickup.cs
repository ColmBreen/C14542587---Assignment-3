using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour 
{
	//If collision with player is detected players health is full
	//gameObject gets destroyed
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			GM.instance.health = true;
			Destroy(this.gameObject);
		}
	}
}

	
