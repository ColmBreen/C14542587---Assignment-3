using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	private Rigidbody2D rb;
	private bool grounded;
	private float moveVelocity;
	private float nextFire = 0.0f;
	private float fireRate = 0.5f;
	private int grenades = 3;
	
	public float speed;
	public float jump;
	public int health;
	public bool fire = false;
	public GameObject Bullet;
	public GameObject Grenade;
	public int direction = 1;
	public Transform ShotSpawn;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		grounded = true;
		health = 50;
	}
	
	void Update()
	{
		if(Input.GetKey(KeyCode.Space) && Time.time > nextFire)
		{
			GM.instance.playerFire = true;
			nextFire = Time.time + fireRate;
			if(Input.GetKey(KeyCode.S))
			{
				if(direction == 1)
					Instantiate (Bullet, ShotSpawn.position + (Vector3.down / 2), ShotSpawn.rotation);
				else
					Instantiate (Bullet, (ShotSpawn.position + (Vector3.left + Vector3.left)) + (Vector3.down / 2), ShotSpawn.rotation);
			}
			else
			{
				if(direction == 1)
					Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
				else
					Instantiate (Bullet, (ShotSpawn.position + (Vector3.left + Vector3.left)), ShotSpawn.rotation);
			}
		}
		
		if(Input.GetKeyDown(KeyCode.LeftShift) && grenades > 0 && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			if(direction == 1)
			{
				Instantiate (Grenade, ShotSpawn.position, ShotSpawn.rotation);
				grenades--;
			}
			else
			{
				Instantiate (Grenade, ShotSpawn.position + Vector3.left, ShotSpawn.rotation);
				grenades--;
			}
		}
	}
	
	void FixedUpdate()
	{
		moveVelocity = 0.0f;
		rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		
		if(Input.GetKeyDown(KeyCode.W) && grounded == true)
		{
			rb.velocity = new Vector2(rb.velocity.x, jump);	
		}
		
		else if(Input.GetKey(KeyCode.A))
		{
			direction = 0;
			GM.instance.playerDirection = 0;
			moveVelocity = -speed;
			rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			direction = 1;
			GM.instance.playerDirection = 1;
			moveVelocity = speed;
			rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		}
	}
	
	void grenadeDamage()
	{
		GM.instance.LoseLife();
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Bullet")
		{
			health -= 10;
		}
		if(health <= 0)
		{
			GM.instance.LoseLife();
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
