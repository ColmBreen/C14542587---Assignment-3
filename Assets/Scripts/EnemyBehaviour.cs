using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	private Rigidbody2D erb;
	private float startPos;
	private float moveVelocity;
	public float speed;
	public int directions;
	public int health;
	private bool wall;
	private bool shootL, shootR;
	public Transform ShotSpawn;
	public float fireRate = 0.5f;
	public float nextFire = 0.0f;
	public GameObject Bullet;
	public GameObject bloodParticles;
	public GameObject playerC;
	Vector3 temp, temp2;
	
	void Start()
	{
		erb = GetComponent<Rigidbody2D>();
		startPos = erb.transform.position.x;
		directions = -1;
		health = 50;
		wall = false;
		shootL = false; 
		shootR = false;
	}
	
	void Update()
	{
		temp = playerC.transform.position - new Vector3(10f, 0f, 0f);
		temp2 = playerC.transform.position + new Vector3(10f, 0f, 0f);
		//if((playerC.transform.position - new Vector3(10f, 0f, 0f)) < erb.transform.position && playerC.transform.position > erb.transform.position.x)
		if((temp.x < erb.transform.position.x) && playerC.transform.position.x > erb.transform.position.x)
		{
			shootR = true;
			this.directions = 1;
			erb.velocity = new Vector2(0, 0);
			if(Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
			}
		}
		//else if((playerC.transform.position + 10) > erb.transform.position.x && playerC.transform.position < erb.transform.position.x)
		else if((temp2.x > erb.transform.position.x) && playerC.transform.position.x < erb.transform.position.x)
		{
			shootL = true;
			this.directions = -1;
			erb.velocity = new Vector2(0, 0);
			if(Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate (Bullet, (ShotSpawn.position + (Vector3.left + Vector3.left)), ShotSpawn.rotation);
			}
		}
		else
		{
			shootL = false;
			shootR = false;
		}
	}
	
	void FixedUpdate()
	{
		if(erb.transform.position.x > (startPos - 10) && directions < 0 && wall == false && shootR == false && shootL == false)
		{
			moveVelocity = -speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(erb.transform.position.x < (startPos + 10) && directions > 0 && wall == false && shootR == false && shootL == false)
		{
			moveVelocity = speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(shootR == false && shootL == false)
		{
			directions = directions * (-1);
			wall = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Bullet")
		{
			health -= 10;
		}
		if(health <= 0)
		{
			Instantiate(bloodParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag == "Dirt")
		{
			wall = true;
		}
    }
}
