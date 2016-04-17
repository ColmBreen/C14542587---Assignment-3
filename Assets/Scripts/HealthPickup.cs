using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			GM.instance.health = true;
			Destroy(this.gameObject);
		}
	}
}

	
