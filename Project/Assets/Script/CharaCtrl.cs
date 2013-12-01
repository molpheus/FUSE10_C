using UnityEngine;
using System.Collections;

public class CharaCtrl : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.A))
		{
			Jumps();
			SendMessage("Jump");
		}
    }

    void Jumps()
    {
		gameObject.rigidbody2D.velocity = Vector3.up * 6f;
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
}
