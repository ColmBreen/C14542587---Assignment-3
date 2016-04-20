using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	public float speed;
	public int directions;
	public int health;
	public Transform ShotSpawn;
	public float fireRate = 0.5f;
	public float nextFire = 0.0f;
	public GameObject Bullet;
	public GameObject bloodParticles;
	public GameObject playerC;
	public Material forward;
	public Material backward;
	public AudioClip shootSound;
	public AudioClip hurtSound;
	
	private AudioSource source;
	private AudioSource source2;
	private float volRange = 0.5f;
	private float volHighRange = 1f;
	private float vol;
	private Rigidbody2D erb;
	private float startPos;
	private float moveVelocity;
	private bool wall;
	private bool shootL, shootR;
	private Renderer rend;
	
	Vector3 temp, temp2, temp3, temp4;
	
	void Awake()
	{
		//Sets up Audio sources
		source = GetComponent<AudioSource>();
		source2 = GetComponent<AudioSource>();
	}
	
	void Start()
	{
		//Sets renderer to allow switching sprites
		rend = GetComponent<Renderer>();
		erb = GetComponent<Rigidbody2D>();
		startPos = erb.transform.position.x;
		directions = -1;
		GM.instance.enemyDirection = -1;
		health = 50;
		wall = false;
		shootL = false; 
		shootR = false;
	}
	
	void Update()
	{
		//Creates Vector3s to check if player is within range
		temp = GM.instance.playerPos - new Vector3(10f, 0f, 0f);
		temp2 = GM.instance.playerPos + new Vector3(10f, 0f, 0f);
		temp3 = GM.instance.playerPos + new Vector3(0f, 1.5f, 0f);
		temp4 = GM.instance.playerPos - new Vector3(0f, 1.5f, 0f);
		//Checks if player is within shooting range
		if((temp.x < erb.transform.position.x) && GM.instance.playerPos.x > erb.transform.position.x && erb.transform.position.y < temp3.y &&  erb.transform.position.y > temp4.y)
		{
			shootR = true;
			rend.sharedMaterial = forward;
			this.directions = 1;
			GM.instance.enemyDirection = 1;
			erb.velocity = new Vector2(0, 0);
			if(Time.time > nextFire)
			{
				vol = Random.Range(volRange, volHighRange);
				source.PlayOneShot(shootSound, vol);
				nextFire = Time.time + fireRate;
				Instantiate (Bullet, ShotSpawn.position + Vector3.right, ShotSpawn.rotation);
			}
		}
		else if((temp2.x > erb.transform.position.x) && GM.instance.playerPos.x < erb.transform.position.x && erb.transform.position.y < temp3.y &&  erb.transform.position.y > temp4.y)
		{
			shootL = true;
			rend.sharedMaterial = backward;
			this.directions = -1;
			GM.instance.enemyDirection = -1;
			erb.velocity = new Vector2(0, 0);
			if(Time.time > nextFire)
			{
				vol = Random.Range(volRange, volHighRange);
				source.PlayOneShot(shootSound, vol);
				nextFire = Time.time + fireRate;
				Instantiate (Bullet, (ShotSpawn.position + (Vector3.left + Vector3.left)), ShotSpawn.rotation);
			}
		}
		else
		{
			shootL = false;
			shootR = false;
		}
		//Resets enemies if variable is true after player has died
		if(GM.instance.enemyReset == true)
		{
			Destroy(this.gameObject);
		}
	}
	
	void FixedUpdate()
	{
		//Enemy movement code
		if(erb.transform.position.x > (startPos - 10) && directions < 0 && wall == false && shootR == false && shootL == false)
		{
			rend.sharedMaterial = backward;
			moveVelocity = -speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(erb.transform.position.x < (startPos + 10) && directions > 0 && wall == false && shootR == false && shootL == false)
		{
			rend.sharedMaterial = forward;
			moveVelocity = speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(shootR == false && shootL == false)
		{
			directions = directions * (-1);
			GM.instance.enemyDirection *= -1;
			wall = false;
		}
	}
	//Checks if hit by bullet
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Bullet")
		{
			health -= 10;
		}
		if(health <= 0)
		{
			vol = Random.Range(volRange, volHighRange);
			source2.PlayOneShot(hurtSound, vol);
			Instantiate(bloodParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	//Checks if collided with wall
	void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag == "Dirt")
		{
			wall = true;
		}
    }
}
