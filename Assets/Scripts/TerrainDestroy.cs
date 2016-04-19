using UnityEngine;
using System.Collections;

public class TerrainDestroy : MonoBehaviour {

	public int health;
	public GameObject dirtParticles;
	
	void Start()
	{
		health = 20;
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Bullet")
		{
			health -= 10;
		}
		if(health <= 0)
		{
			Instantiate(dirtParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
