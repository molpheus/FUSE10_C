using UnityEngine;
using System.Collections;

public class SendMessageScript : MonoBehaviour {

	public GameObject target = null;
	public string name = "";

	public void SendMessage()
	{
		target.SendMessage(name);
	}
}
