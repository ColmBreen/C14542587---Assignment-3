using UnityEngine;
using System.Collections;

public class GrenadeController : MonoBehaviour {

	private float grenadeSpeed;
	private float cook;
	private Rigidbody2D rb;
	private bool playerDead;
	private AudioSource source;
	private float volRange = 0.5f;
	private float volHighRange = 1f;
	private float vol;
	
	public AudioClip grenadeSound;
	public GameObject bloodParticles;
	public GameObject dirtParticles;
	
	
	
	void Start()
	{
		source = GetComponent<AudioSource>();
		playerDead = false;
		grenadeSpeed = 10;
		rb = GetComponent<Rigidbody2D>();
		if(GM.instance.playerDirection == 1)
		rb.velocity = new Vector2(grenadeSpeed, grenadeSpeed);
		else
			rb.velocity = new Vector2(grenadeSpeed * -1, grenadeSpeed);
		GM.instance.playerFire = false;
		cook = Time.time + 2f;
	}
	
	void Update()
	{
		if(Time.time > cook)
		{
			vol = Random.Range(volRange, volHighRange);
			source.PlayOneShot(grenadeSound, vol);
			Explosion(rb.transform.position, 2f);
		}
	}
	
	void Explosion(Vector2 centre, float radius)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(centre, radius, 1, -1f, 1f);
		foreach(Collider2D objects in colliders)
		{
			if(objects.gameObject.tag == "Player" && playerDead == false)
			{
				playerDead = true;
				GM.instance.LoseLife();
			}
			if(objects.gameObject.tag == "Enemy")
			{
				Instantiate(bloodParticles, objects.transform.position, Quaternion.identity);
				Destroy(objects.gameObject);
			}
			if(objects.gameObject.tag == "Dirt")
			{
				Instantiate(dirtParticles, objects.transform.position, Quaternion.identity);
				Destroy(objects.gameObject);
			}	
			//colliders[i].gameObject.grenadeDamage();
		}
		Destroy(gameObject);
	}
}
