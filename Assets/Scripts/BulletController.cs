using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	
	public float bulletSpeed;
	private Rigidbody2D rb;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>(); 
		rb.velocity = new Vector2(bulletSpeed, 0.0f);
	}
}
