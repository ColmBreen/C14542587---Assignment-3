using UnityEngine;
using System.Collections;

public class GunPickup : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			//int i = (int)Random.Range(0, 2);
			int i = 1;
			for(int j = 0; j < 4; j++)
			{
				if(j == i && i == 1)
				{
					GM.instance.guns[j - 1] = true;
					GM.instance.guns[j] = true;
				}
				if(j == i)
				{
					GM.instance.guns[j] = true;
				}
				else
				{
					GM.instance.guns[j] = false;
				}
			}
			Destroy(gameObject);
		}
	}
}
