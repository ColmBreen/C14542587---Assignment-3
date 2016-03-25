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
		if(playerControl.direction == 1)
			rb.velocity = new Vector2(bulletSpeed, 0.0f);
		else
			rb.velocity = new Vector2(bulletSpeed * -1, 0.0f);
	}
}
