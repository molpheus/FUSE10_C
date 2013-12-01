using UnityEngine;
using System.Collections;

public class CharaCtrl : MonoBehaviour
{
	public bool jump = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.A) && !jump)
		{
			Jumps();
			SendMessage("Jump");
		}
    }

    void Jumps()
    {
		gameObject.rigidbody2D.velocity = Vector3.up * 6f;
		jump = true;
    }
	IEnumerator JumpMove()
	{
		yield return null;
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(0,0,50,50), "<"))
		{
			GameUtility.speed += 1f;
		}
		if (GUI.Button(new Rect(50, 0, 50, 50), ">"))
		{
			GameUtility.speed -= 1f;
		}
		GUI.Label(new Rect(100, 0, 50, 50), GameUtility.speed.ToString());
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("other.gameObject.tag : " + other.gameObject.tag);
		if (other.gameObject.tag == "floor")
		{
			jump = false;
		}

		if (other.gameObject.tag == "ball")
		{
			if (jump)
			{
				if (transform.position.y > other.gameObject.transform.position.y)
				{
					Object.Destroy(other.gameObject);
				}
				else
				{
					gameObject.rigidbody2D.fixedAngle = false;
					gameObject.rigidbody2D.isKinematic = true;
					gameObject.rigidbody2D.velocity = Vector3.up * 10f + Vector3.right * 10f;
				}
			}
			else
			{
				gameObject.rigidbody2D.fixedAngle = false;
				gameObject.rigidbody2D.isKinematic = true;
				gameObject.rigidbody2D.velocity = Vector3.up * 10f + Vector3.right * 10f;
			}
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		jump = true;
	}
}
