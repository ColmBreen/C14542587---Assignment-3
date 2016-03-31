using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	private Rigidbody2D erb;
	private float startPos;
	private float moveVelocity;
	public float speed;
	private int direction;
	public int health;
	private bool wall;
	private bool shootL, shootR;
	public Transform ShotSpawn;
	public float fireRate = 0.5f;
	public float nextFire = 0.0f;
	public GameObject Bullet;
	
	void Start()
	{
		erb = GetComponent<Rigidbody2D>();
		startPos = erb.transform.position.x;
		direction = -1;
		health = 50;
		wall = false;
		shootL = false; 
		shootR = false;
	}
	
	void Update()
	{
		GameObject player = GameObject.FindWithTag("Player");
		PlayerControl playerC = player.GetComponent<PlayerControl>();
		if((playerC.posX - 10) < erb.transform.position.x && playerC.posX > erb.transform.position.x)
		{
			shootR = true;
			erb.velocity = new Vector2(0, 0);
			if(Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
			}
		}
		else if((playerC.posX + 10) > erb.transform.position.x && playerC.posX < erb.transform.position.x)
		{
			shootL = true;
			erb.velocity = new Vector2(0, 0);
			if(Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate (Bullet, (ShotSpawn.position + (Vector3.left + Vector3.left + Vector3.left)), ShotSpawn.rotation);
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
		if(erb.transform.position.x > (startPos - 10) && direction < 0 && wall == false && shootR == false && shootL == false)
		{
			moveVelocity = -speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(erb.transform.position.x < (startPos + 10) && direction > 0 && wall == false && shootR == false && shootL == false)
		{
			moveVelocity = speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else
		{
			direction = direction * (-1);
			wall = false;
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
