using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	private Rigidbody2D rb;
	public float speed;
	public float jump;
	private bool grounded;
	private float moveVelocity;
	//public int grenades = 3;
	
	public GameObject Bullet;
	//public GameObject Grenade;
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
	public int direction = 1;
	public Transform ShotSpawn;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		grounded = true;
	}
	
	void Update()
	{
		
		if(Input.GetKey(KeyCode.Space) && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			if(Input.GetKey(KeyCode.S))
			{
				if(direction == 1)
					Instantiate (Bullet, ShotSpawn.position + (Vector3.down / 2), ShotSpawn.rotation);
				else
					Instantiate (Bullet, (ShotSpawn.position + Vector3.left) + (Vector3.down / 2), ShotSpawn.rotation);
			}
			else
			{
				if(direction == 1)
					Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
				else
					Instantiate (Bullet, (ShotSpawn.position + Vector3.left), ShotSpawn.rotation);
			}
		}
		
		//if(Input.GetKey(KeyCode.LeftShift) && grenades > 0 && Time.time > nextFire)
		//{
		//	nextFire = Time.time + fireRate;
		//	if(direction == 1)
		//	{
		//		Instantiate (Grenade, ShotSpawn.position, ShotSpawn.rotation);
		//		grenades--;
		//	}
		//	else
		//	{
		//		Instantiate (Grenade, ShotSpawn.position + Vector3.left, ShotSpawn.rotation);
		//		grenades--;
		//	}
		//}
	}
	
	void FixedUpdate()
	{
		moveVelocity = 0.0f;
		rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		
		if(Input.GetKey(KeyCode.W) && grounded == true)
		{
			rb.velocity = new Vector2(rb.velocity.x, jump);	
		}
		
		else if(Input.GetKey(KeyCode.A))
		{
			direction = 0;
			moveVelocity = -speed;
			rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			direction = 1;
			moveVelocity = speed;
			rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag == "Dirt")
		{
			grounded = true;
		}
    }
	
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Dirt")
		{
			grounded = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
    {
		if(other.gameObject.tag == "Dirt")
		{
			grounded = false;
		}
    }
}
