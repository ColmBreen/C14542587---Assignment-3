using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	private Rigidbody2D erb;
	private float startPos;
	private float moveVelocity;
	public float speed;
	private int direction;
	
	void Start()
	{
		erb = GetComponent<Rigidbody2D>();
		startPos = erb.transform.position.x;
		direction = -1;
	}
	
	void FixedUpdate()
	{
		if(erb.transform.position.x > (startPos - 10) && direction < 0)
		{
			moveVelocity = -speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else if(erb.transform.position.x < (startPos + 10) && direction > 0)
		{
			moveVelocity = speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
		else
		{
			direction = direction * (-1);
		}
	}
}
