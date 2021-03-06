﻿using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	
	public float bulletSpeed;
	private Rigidbody2D rb;
	private float travelTime;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>(); 
		//Fires bullets from player
		if(GM.instance.playerFire == true)
		{
			if(GM.instance.playerDirection == 1)
				rb.velocity = new Vector2(bulletSpeed, 0.0f);
			else
				rb.velocity = new Vector2(bulletSpeed * -1, 0.0f);
			GM.instance.playerFire = false;
			travelTime = Time.time;
		}
		//Fires bullets from enemy
		else
		{
			if(GM.instance.enemyDirection == 1)
				rb.velocity = new Vector2(bulletSpeed, 0.0f);
			else
				rb.velocity = new Vector2(bulletSpeed * -1, 0.0f);
			travelTime = Time.time;
		}
	}
	//Destroys bullet if no collision within two seconds
	void Update()
	{
		if(Time.time > (travelTime + 2f))
		{
			Destroy(this.gameObject);
		}
	}
	//Destroys the bullet if it collides with anything
	void OnCollisionEnter2D()
	{
		Destroy(this.gameObject);
	}
}
