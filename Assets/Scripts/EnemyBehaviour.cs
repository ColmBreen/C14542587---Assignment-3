using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	private Rigidbody2D erb;
	private float startPos;
	private float moveVelocity;
	public float speed;
	private int direction;
	public int health;
	public int jump;
	private bool wall = false;
	
	void Start()
	{
		erb = GetComponent<Rigidbody2D>();
		startPos = erb.transform.position.x;
		direction = -1;
		health = 50;
	}
	
	void FixedUpdate()
	{
		if(erb.transform.position.x > (startPos - 10) && direction < 0)
		{
			moveVelocity = -speed;
			if(wall == true)
			{
				erb.velocity = new Vector2(moveVelocity, jump);
			}
			else
				erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(erb.transform.position.x < (startPos + 10) && direction > 0)
		{
			moveVelocity = speed;
			if(wall == true)
			{
				erb.velocity = new Vector2(moveVelocity, jump);
			}
			else
				erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else
		{
			direction = direction * (-1);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag == "Dirt")
		{
			wall = true;
		}
    }
	
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Dirt")
		{
			wall = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
    {
		if(other.gameObject.tag == "Dirt")
		{
			wall = false;
		}
    }
}
