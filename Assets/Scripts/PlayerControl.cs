using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	private Rigidbody2D rb;
	public float speed;
	public float jump;
	private bool grounded;
	private float moveVelocity;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		grounded = true;
	}
	
	void Update()
	{
	}
	
	void FixedUpdate()
	{
		moveVelocity = 0.0f;
		rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
		
		if(Input.GetKeyDown(KeyCode.Space) && grounded == true)
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
