using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	
	public float bulletSpeed;
	private Rigidbody2D rb;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>(); 
		
		GameObject player = GameObject.FindWithTag("Player");
		PlayerControl playerControl = player.GetComponent<PlayerControl>();
		if(playerControl.fire == true)
		{
			if(playerControl.direction == 1)
				rb.velocity = new Vector2(bulletSpeed, 0.0f);
			else
				rb.velocity = new Vector2(bulletSpeed * -1, 0.0f);
			playerControl.fire = false;
		}
		else
		{
			GameObject enemy = GameObject.FindWithTag("Enemy");
			EnemyBehaviour enemyControl = enemy.GetComponent<EnemyBehaviour>();
			if(enemyControl.directions == 1)
				rb.velocity = new Vector2(bulletSpeed, 0.0f);
			else
				rb.velocity = new Vector2(bulletSpeed * -1, 0.0f);
		}
	}
	
	void OnCollisionEnter2D()
	{
		Destroy(this.gameObject);
	}
}
