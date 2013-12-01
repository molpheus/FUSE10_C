using UnityEngine;
using System.Collections;

public class LeftBall : MonoBehaviour
{
	Vector3 startpos = new Vector3(10f, 1.5f, 0);
	Vector3 endpos = new Vector3(30f, 1.5f, 0);

	float speed = 0.3f;

	// Use this for initialization
	void Start()
	{
		transform.position = startpos;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += new Vector3(speed * GameUtility.speed, 0, 0);
		if(transform.position.x > endpos.x)
		{
			Object.Destroy(this.gameObject);
		}
	}
}
