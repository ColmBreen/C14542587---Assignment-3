using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	private Rigidbody2D rb;
	public float speed;
	public float jump;
	private bool grounded;
	private float moveVelocity;
	
	public GameObject Bullet;
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
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
			Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
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
		
		if(Input.GetKey(KeyCode.A))
		{
			moveVelocity = -speed;
			rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			moveVelocity = speed;
			rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		}
		
		
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.CompareTag("Dirt"))
		{
			grounded = true;
		}
    }
	
	void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Dirt"))
		{
			grounded = false;
		}
    }
}
