using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	private Rigidbody2D erb;
	private float startPos;
	private float moveVelocity;
	public float speed;
	private int direction;
	public int health;
	private bool wall = false;
	private bool shootL = false, shootR = false;
	public Transform ShotSpawn;
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
	public GameObject Bullet;
	
	void Start()
	{
		erb = GetComponent<Rigidbody2D>();
		startPos = erb.transform.position.x;
		direction = -1;
		health = 50;
	}
	
	void Update()
	{
		GameObject player = GameObject.FindWithTag("Player");
		PlayerControl playerC = player.GetComponent<PlayerControl>();
		if((playerC.posX + 10) > erb.transform.position.x)
		{
			shootL = true;
		}
		if((playerC.posX - 10) < erb.transform.position.x)
		{
			shootR = true;
		}
	}
	
	void FixedUpdate()
	{
		if(erb.transform.position.x > (startPos - 10) && direction < 0 && wall == false)// && shootR == false && shootL == false)
		{
			moveVelocity = -speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(erb.transform.position.x < (startPos + 10) && direction > 0 && wall == false)// && shootR == false && shootL == false)
		{
			moveVelocity = speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(shootL == true)
		{
			nextFire = Time.time + fireRate;
			if(Time.time > nextFire)
			{
				Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
			}
		}
		else if(shootR == true)
		{
			nextFire = Time.time + fireRate;
			if(Time.time > nextFire)
			{
				Instantiate (Bullet, (ShotSpawn.position + (Vector3.left + (Vector3.left/2))), ShotSpawn.rotation);
			}
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
