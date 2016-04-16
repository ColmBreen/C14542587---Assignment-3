using UnityEngine;
using System.Collections;

public class GrenadeController : MonoBehaviour {

	private float grenadeSpeed;
	private float cook;
	private Rigidbody2D rb;
	
	void Start()
	{
		grenadeSpeed = 20;
		rb = GetComponent<Rigidbody2D>();
		if(GM.instance.playerDirection == 1)
		rb.velocity = new Vector2(grenadeSpeed, grenadeSpeed);
		else
			rb.velocity = new Vector2(grenadeSpeed * -1, grenadeSpeed);
		GM.instance.playerFire = false;
		cook = Time.time + 5f;
	}
	
	void Update()
	{
		if(Time.time > cook)
		{
			Explosion(rb.transform.position, 15f);
		}
	}
	
	void Explosion(Vector3 centre, float radius)
	{
		int i = 0;
		Collider[] colliders = Physics.OverlapSphere(centre, radius);
		while(i < colliders.Length)
		{
			colliders[i].grenadeDamage();
			i++;
		}
	}
}
