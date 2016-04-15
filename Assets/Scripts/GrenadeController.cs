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
	
	void onTriggerStay2D(Collider2D other)
	{
		if(Time.time < cook)
		{
			
		}
	}
}
