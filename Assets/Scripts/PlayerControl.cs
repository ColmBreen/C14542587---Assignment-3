using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	private Rigidbody2D rb;
	private bool grounded;
	private float moveVelocity;
	private float nextFire = 0.0f;
	private float fireRate = 0.5f;
	private int grenades = 3;
	private Renderer rend;
	private AudioSource source;
	private float volRange = 0.5f;
	private float volHighRange = 1f;
	private float vol;
	private float speed = 7;
	private float jump = 15;
	
	public AudioClip shootSound;
	public Material forward;
	public Material backward;
	public int health;
	public bool fire = false;
	public GameObject Bullet;
	public GameObject Grenade;
	public int direction = 1;
	public Transform ShotSpawn;
	
	void Awake()
	{
		//Sets up the audiosource
		source = GetComponent<AudioSource>();
	}
	
	void Start()
	{
		GM.instance.enemyReset = false;
		//Sets up Renderer to allow for switching sprites
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody2D>();
		grounded = true;
		health = 50;
		GM.instance.pHealth = 50;
	}
	
	void Update()
	{
		//Checks if the player has tried to shoot
		if(Input.GetKey(KeyCode.Space) && Time.time > nextFire)
		{
			GM.instance.playerFire = true;
			//Checks if player has gun pickup
			if(GM.instance.guns[1] == true)
			{
				//Sets the time which the player has to wait before firing again
				nextFire = Time.time + 0.2f;
			}
			else
				nextFire = Time.time + fireRate;
			//Fires bullets low if player is holding down S
			if(Input.GetKey(KeyCode.S))
			{
				if(GM.instance.guns[0] == true)
				{
					//Sets a random volume for gunshots within a small limit
					vol = Random.Range(volRange, volHighRange);
					source.PlayOneShot(shootSound, vol);
					if(direction == 1)
						Instantiate (Bullet, ShotSpawn.position + (Vector3.down / 2), ShotSpawn.rotation);
					else
						Instantiate (Bullet, (ShotSpawn.position + (Vector3.left + Vector3.left)) + (Vector3.down / 2), ShotSpawn.rotation);
				}
			}
			else
			{
				vol = Random.Range(volRange, volHighRange);
				source.PlayOneShot(shootSound, vol);
				if(direction == 1)
					Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
				else
					Instantiate (Bullet, (ShotSpawn.position + (Vector3.left + Vector3.left)), ShotSpawn.rotation);
			}
		}
		//Checks to see if player has thrown a grenade
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
		//All player movement
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
			rend.sharedMaterial = backward;
			rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			direction = 1;
			GM.instance.playerDirection = 1;
			moveVelocity = speed;
			rend.sharedMaterial = forward;
			rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		}
		
		if(rb.position.x <= 0)
		{
			rb.position = new Vector2 (0, rb.transform.position.y);
		}
		if(rb.position.x >= 185)
		{
			rb.position = new Vector2 (185, rb.transform.position.y);
			GM.instance.WinGame();
		}
		
		if(rb.position.y <= -10)
		{
			GM.instance.LoseLife();
		}
		
		if(GM.instance.health == true)
		{
			this.health = 50;
			GM.instance.pHealth = 50;
			GM.instance.health = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		//Checks if player was hit by bullet
		if(other.gameObject.tag == "Bullet")
		{
			health -= 10;
			GM.instance.pHealth -= 10;
		}
		if(health <= 0)
		{
			GM.instance.LoseLife();
		}
	}
	//Checks if player can jump
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
