using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	bool touchOK = false;
	public Camera uiCamera = null;
	// Use this for initialization
	void Start()
	{
		if(uiCamera == null)
		{
			uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
		}
		touchOK = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (touchOK) { return; }
		if (Input.GetMouseButtonDown(0))
		{
			if (Chack2DHit("play"))
			{
				Application.LoadLevel("GameScene");
				touchOK = true;
			}
		}

		if (Input.touchCount != 0)
		{
			foreach (Touch t in Input.touches)
			{
				if(t.phase == TouchPhase.Began)
				{
					if (Chack2DHit("play"))
					{
						Application.LoadLevel("GameScene");
						touchOK = true;
					}
				}
			}
		}
	}

	bool Chack2DHit(string name)
	{
		Vector2 tapPoint = uiCamera.ScreenToWorldPoint(Input.mousePosition);
		Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
		RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);
		if (hitObject)
		{
			Debug.Log("hit object is " + hitObject.collider.gameObject.name);
			if (name == hitObject.collider.gameObject.name) { return true; }
		}
		return false;
	}
}
