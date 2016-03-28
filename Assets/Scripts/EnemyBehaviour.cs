using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	private Rigidbody2D erb;
	private Vector2 startPos;
	private float moveVelocity;
	public float speed;
	private int direction;
	
	void Start()
	{
		erb = GetComponent<Rigidbody2D>();
		startPos = erb.transform.position;
		direction = -1;
	}
	
	void FixedUpdate()
	{
		if(erb.transform.position > startPos - Vector2(10, 0) && direction < 0)
		{
			moveVelocity = -speed;
			erb.velocity = new Vector2(moveVelocity, erb.velocity.y);
		}
	}
}
